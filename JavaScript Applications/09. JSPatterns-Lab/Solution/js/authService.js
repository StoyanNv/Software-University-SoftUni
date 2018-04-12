let auth = (() => {
    function isAuth() {
        return localStorage.getItem('authtoken') !== null;
    }

    function saveSession(userData) {
        localStorage.setItem('authtoken', userData._kmd.authtoken);
        localStorage.setItem('username', userData.username);
        localStorage.setItem('userId', userData._id);
    }


    function register (username, password) {
        let obj = { username, password };
        remote.post('user', '', 'basic', obj)
            .then(saveSession)
            .catch(console.error);
    }

    function login(username, password) {
        let obj = { username, password };
        return remote.post('user', 'login', 'basic', obj)
    }
    
    function logout(ctx) {
        remote.post('user', '_logout', 'kinvey')
            .then(() => {
                localStorage.clear();
                ctx.redirect('#/index.html');
            })
            .catch(console.error);
    }

    return {
        isAuth,
        login,
        logout,
        register,
        saveSession
    }
})();