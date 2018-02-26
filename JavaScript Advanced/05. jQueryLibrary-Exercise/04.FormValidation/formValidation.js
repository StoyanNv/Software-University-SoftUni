function validate() {
    $('#company').on('click', show);
    let isHidden = true;
    $('#submit').on('click', submitData);

    function submitData() {
        event.preventDefault();
        let username = $('#username').val();
        let email = $('#email').val();
        let password = $('#password').val();
        let confirmPassword = $('#confirm-password').val();
        let areAllValid = true;

        let usernamePattern = /^([a-zA-Z0-9]{3,20})$/g;
        let emailPattern = /@.*\./g;
        let passwordPattern =  /^([a-zA-Z0-9_]{5,15})$/g;

        let isValidUsername = username.match(usernamePattern);
        let isValidEmail = email.match(emailPattern);
        let isValidPassword = password.match(passwordPattern);

        if (!isValidUsername) {
            $('#username').css('border-color', 'red');
            areAllValid = false;
        } else {
            $('#username').css('border', 'none');
        }

        if (!isValidEmail) {
            $('#email').css('border-color', 'red');
            areAllValid = false;
        } else {
            $('#email').css('border', 'none');
        }

        if (!isValidPassword) {
            $('#password').css('border-color', 'red');
            areAllValid = false;
        } else {
            $('#password').css('border', 'none');
        }

        if (confirmPassword.match(passwordPattern) && confirmPassword.localeCompare(password) === 0) {
            $('#confirm-password').css('border', 'none');
        } else {
            $('#confirm-password').css('border-color', 'red');
            $('#password').css('border-color', 'red');
            areAllValid = false;
        }

        if (!isHidden) {
            let companyNumber = $('#companyNumber').val();

            if (Number(companyNumber) >= 1000 && Number(companyNumber) <= 9999) {
                $('#companyNumber').css('border', 'none');
            } else {
                $('#companyNumber').css('border-color', 'red');
                areAllValid = false;
            }
        }

        if (areAllValid) {
            $('#valid').css('display', 'block');
        } else {
            $('#valid').css('display', 'none');
        }
    }

    function show() {
        if (isHidden) {
            $('#companyInfo').css('display', 'block');

            isHidden = false;
        } else {
            $('#companyInfo').css('display', 'none');
            isHidden = true;
        }
    }
}