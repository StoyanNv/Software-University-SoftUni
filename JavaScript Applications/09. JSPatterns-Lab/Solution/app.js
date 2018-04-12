$(() => {
    const app = Sammy('#main', function () {
        this.use('Handlebars', 'hbs');

        this.get('#/index.html', (ctx)=>{
            ctx.isAuth = auth.isAuth();

            $.ajax('data.json')
                .then((contacts) => {
                    ctx.contacts = contacts;

                    ctx.loadPartials({
                        header: './templates/common/header.hbs',
                        navigation: './templates/common/navigation.hbs',
                        footer: './templates/common/footer.hbs',
                        contactPage: './templates/contacts/contactPage.hbs',
                        contact: './templates/contacts/contact.hbs',
                        contactDetails: './templates/contacts/contactDetails.hbs',
                        contactsList: './templates/contacts/contactsList.hbs',
                        loginForm: './templates/forms/loginForm.hbs'
                    }).then(function () {
                        ctx.partials = this.partials;

                        render();
                    });
                })
                .catch(console.error);

            function render () {
                ctx.partial('./templates/welcome.hbs')
                    .then(attachEvents)
            }

            function attachEvents() {
                $('.contact').click((e) => {
                    let index = $(e.target).closest('.contact').attr('data-id');
                    ctx.selected = ctx.contacts[index];
                    render();
                });
            }
        });

        this.get('#/register', (ctx) => {
            ctx.loadPartials({
                header: './templates/common/header.hbs',
                navigation: './templates/common/navigation.hbs',
                footer: './templates/common/footer.hbs',
            }).then(function () {
                this.partial('./templates/forms/registerForm.hbs')
            });
        });

        this.post('#/register', (ctx) => {
            let username = ctx.params.username;
            let password = ctx.params.pass;
            let repeatPass = ctx.params.repeatPass;

            if (password !== repeatPass) {
                alert('Passwords do not match');
            } else {
                auth.register(username, password);
                ctx.redirect('#/index.html');
            }
        });

        this.post('#/login', (ctx) => {
            let username = ctx.params.username;
            let password = ctx.params.pass;

            auth.login(username, password)
                .then((userData) => {
                    auth.saveSession(userData);
                    ctx.redirect('#/index.html');
                })
                .catch(console.error);
        });
        this.get('#/logout', (ctx) => {
            (function () {

               auth.logout(ctx)

            }())

        });
    });
    app.run();
});