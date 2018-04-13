$(() => {
    const app = Sammy('#main', function () {
        this.use('Handlebars', 'hbs');

        this.get('index.html', (ctx) => {
            ctx.loadPartials({
                header: './templates/common/header.hbs',
                footer: './templates/common/footer.hbs',
            }).then(function () {
                ctx.teamId = sessionStorage.getItem('teamId');
                ctx.hasTeam = ctx.teamId !== null;
                ctx.username = sessionStorage.getItem('username');
                ctx.partials = this.partials;
                ctx.loggedIn = sessionStorage.getItem('authtoken') !== null;
                ctx.partial('./templates/home/home.hbs');
            });
        });
        this.get('#/home', (ctx) => {
            ctx.redirect('index.html');
        });
        this.get('#/register', (ctx) => {
            ctx.loadPartials({
                header: './templates/common/header.hbs',
                registerForm: './templates/register/registerForm.hbs',
                footer: './templates/common/footer.hbs',
            }).then(function () {
                ctx.partials = this.partials;
                ctx.partial('./templates/register/registerPage.hbs')
            });
        });
        this.post('#/register', (ctx) => {
            let username = ctx.params.username;
            let password = ctx.params.password;
            let repeatPass = ctx.params.repeatPassword;
            if (password !== repeatPass) {
                alert('Passwords do not match');
            } else {
                auth.register(username, password).then(function handleSucces() {
                    auth.showInfo('Registration successful');
                    ctx.redirect('index.html');
                }).catch((error) => {
                    auth.handleError(error)
                })
            }
        });

        this.get('#/login', (ctx) => {
            ctx.loadPartials({
                header: './templates/common/header.hbs',
                loginForm: './templates/login/loginForm.hbs',
                footer: './templates/common/footer.hbs',
            }).then(function () {
                ctx.partials = this.partials;
                ctx.partial('./templates/login/loginPage.hbs')
            });
        });
        this.post('#/login', (ctx) => {
            let username = ctx.params.username;
            let password = ctx.params.password;

            auth.login(username, password)
                .then((userData) => {
                    auth.saveSession(userData);
                    auth.showInfo('Login successful');

                    ctx.redirect('index.html');
                })
                .catch((err) => {
                        auth.handleError(err)
                    }
                );
        });
        this.get('#/about', (ctx) => {
            ctx.loadPartials({
                header: './templates/common/header.hbs',
                footer: './templates/common/footer.hbs',
            }).then(function () {
                ctx.username = sessionStorage.getItem('username');
                ctx.loggedIn = sessionStorage.getItem('authtoken') !== null;
                ctx.partial('./templates/about/about.hbs');
                ctx.partials = this.partials;
            });
        });
        this.get('#/logout', (ctx) => {
            (function () {
                auth.logout(ctx);
                ctx.redirect('index.html');
                auth.showInfo('Logout successful');
                sessionStorage.clear();
            }())
        });
        this.get('#/catalog', displayCatalog);

        this.get('#/create', function (ctx) {
            ctx.loggedIn = sessionStorage.getItem('authtoken') !== null;
            ctx.username = sessionStorage.getItem('username');
            this.loadPartials({
                header: './templates/common/header.hbs',
                footer: './templates/common/footer.hbs',
                createForm: './templates/create/createForm.hbs'
            }).then(function () {
                this.partial('./templates/create/createPage.hbs')
            });
        });

        this.post('#/create', function (ctx) {
            let name = ctx.params.name;
            let comment = ctx.params.comment;
            teamsService.createTeam(name, comment)
                .then(function (data) {
                    teamsService.joinTeam(data._id)
                        .then((newData) => {
                            auth.saveSession(newData);
                            auth.showInfo('TEAM HAS BEEN CREATED!');
                            displayCatalog(ctx);
                        });
                });
        });

        // TEAM DETAILS
        this.get('#/catalog/:id', function (ctx) {
            let teamId = ctx.params.id.substr(1);
            teamsService.loadTeamDetails(teamId)
                .then(function (teamInfo) {
                    ctx.loggedIn = sessionStorage.getItem('authtoken') !== null;
                    ctx.username = sessionStorage.getItem('username');
                    ctx.name = teamInfo.name;
                    ctx.comment = teamInfo.comment;
                    ctx.members = teamInfo.members;
                    ctx.teamId = teamInfo._id;
                    ctx.isOnTeam = teamInfo._id === sessionStorage.getItem('teamId');
                    ctx.isAuthor = teamInfo._acl.creator === sessionStorage.getItem('userId');
                    ctx.loadPartials({
                        header: './templates/common/header.hbs',
                        footer: './templates/common/footer.hbs',
                        teamMember: './templates/catalog/teamMember.hbs',
                        teamControls: './templates/catalog/teamControls.hbs'
                    }).then(function () {
                        this.partial('./templates/catalog/details.hbs');
                    })
                });

        });

        // LEAVE TEAM
        this.get('#/leave', function (ctx) {
            teamsService.leaveTeam()
                .then(function (response) {
                    auth.saveSession(response);
                    auth.showInfo('TEAM HAS BEEN LEFT!');
                    displayCatalog(ctx);
                });
        });

        // JOIN TEAM
        this.get('#/join/:id', function (ctx) {
            let teamId = this.params.id.substr(1);
            teamsService.joinTeam(teamId)
                .then((data) => {
                    auth.saveSession(data);
                    auth.showInfo('TEAM HAS BEEN JOINED!');
                    displayCatalog(ctx);
                });
        });

        // EDIT TEAM
        this.get('#/edit/:id', function (ctx) {
            ctx.loggedIn = sessionStorage.getItem('authtoken') !== null;
            ctx.username = sessionStorage.getItem('username');
            ctx.teamId = this.params.id.substr(1);
            teamsService.loadTeamDetails(ctx.teamId)
                .then((teamInfo) => {
                    ctx.name = teamInfo.name;
                    ctx.comment = teamInfo.comment;
                    this.loadPartials({
                        header: './templates/common/header.hbs',
                        footer: './templates/common/footer.hbs',
                        editForm: './templates/edit/editForm.hbs'
                    }).then(function () {
                        this.partial('./templates/edit/editPage.hbs');
                    })
                })
        });

        this.post('#/edit/:id', function (ctx) {
            let teamId = ctx.params.id.substr(1);
            let teamName = ctx.params.name;
            let teamComment = ctx.params.comment;

            teamsService.edit(teamId, teamName, teamComment)
                .then(function () {
                    auth.showInfo(`TEAM ${teamName} EDITED!`);
                    displayCatalog(ctx);
                })
        });



        function displayCatalog(ctx) {
            teamsService.loadTeams()
                .then(function (data) {
                    ctx.loggedIn = sessionStorage.getItem('authtoken') !== null;
                    ctx.username = sessionStorage.getItem('username');
                    ctx.hasTeam = sessionStorage.getItem('teamId') !== null;
                    ctx.hasNoTeam = sessionStorage.getItem('teamId') === null
                        || sessionStorage.getItem('teamId') === "undefined";
                    ctx.teams = data;
                    ctx.loadPartials({
                        header: './templates/common/header.hbs',
                        footer: './templates/common/footer.hbs',
                        team: './templates/catalog/team.hbs'
                    }).then(function () {
                        this.partial('./templates/catalog/teamCatalog.hbs');
                    });
                });
        }
    });
    app.run();
});