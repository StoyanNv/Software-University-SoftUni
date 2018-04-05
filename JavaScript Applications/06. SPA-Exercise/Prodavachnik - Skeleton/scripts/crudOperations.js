const BASE_URL = 'https://baas.kinvey.com/';
const APP_KEY = 'kid_SyVxhCMsz';
const APP_SECRET = 'f555ecb289c044fcbb4e7254cfabdc37';
const AUTH_HEADERS = {'Authorization': "Basic " + btoa(APP_KEY + ":" + APP_SECRET)};

function loginUser() {
    let username = $('#formLogin').find('input[name=username]').val();
    let password = $('#formLogin').find('input[name=passwd]').val();
    $.ajax({
        method: 'POST',
        url: BASE_URL + 'user/' + APP_KEY + '/login',
        headers: AUTH_HEADERS,
        data: {username, password}
    }).then(function (res) {
        signInUser(res, 'Login successful.')
    }).catch(handleAjaxError)
}

function registerUser() {
    let username = $('#formRegister').find('input[name=username]').val();
    let password = $('#formRegister').find('input[name=passwd]').val();
    try {
        if (username === '' || password === '') {
            throw  new Error('The request body is either missing or incomplete');
        }
        $.ajax({
            method: 'POST',
            url: BASE_URL + 'user/' + APP_KEY + '/',
            headers: AUTH_HEADERS,
            data: {username, password}
        }).then(function (res) {
            signInUser(res, 'Registration successful.')
        }).catch(handleAjaxError)
    }
    catch (err) {
        handleAjaxError(err)
    }
}

function logoutUser() {
    sessionStorage.clear();
    showHomeView();
    showHideMenuLinks();
    showInfo('Logout successful.')
}

function signInUser(res, message) {
    sessionStorage.setItem('username', res.username);
    sessionStorage.setItem('authToken', res._kmd.authtoken);
    sessionStorage.setItem('userId', res._id);
    showHomeView();
    showHideMenuLinks();
    showInfo(message);
}

function createAd() {
    let title = $('#formCreateAd').find('input[name=title]').val();
    let publisher = sessionStorage.getItem('username');
    let description = $('#formCreateAd').find('textarea[name=description]').val();
    let datePublished = $('#formCreateAd').find('input[name=datePublished]').val();
    let price = $('#formCreateAd').find('input[name=price]').val();
    let obj = {
        'title': title,
        'publisher': publisher,
        'description': description,
        'date of publishing': datePublished,
        'price': price
    };

    $.ajax({
        method: 'POST',
        url: BASE_URL + 'appdata/' + APP_KEY + '/advertisements ',
        data: obj,
        headers: {'Authorization': 'Kinvey ' + sessionStorage.getItem('authToken')}
    }).then(function (res) {
        showInfo('Ad created.');
        listAds();
    }).catch(handleAjaxError)
}

function deleteAd(ad) {
    $.ajax({
        method: 'DELETE',
        url: BASE_URL + 'appdata/' + APP_KEY + '/advertisements/' + ad._id,
        headers: {'Authorization': 'Kinvey ' + sessionStorage.getItem('authToken')}
    }).then(function () {
        listAds();
        showInfo('Ad deleted.');
    }).catch(handleAjaxError)
}

function loadAdForEdit(ad) {
    showView('viewEditAd');
    $('#formEditAd').find('input[name=id]').val(ad._id);
    $('#formEditAd').find('input[name=title]').val(ad.title);
    $('#formEditAd').find('input[name=publisher]').val(ad.publisher);
    $('#formEditAd').find('textarea[name=description]').val(ad.description);
    $('#formEditAd').find('input[name=datePublished]').val(ad['date of publishing']);
    $('#formEditAd').find('input[name=price]').val(ad.price);
}

function editAd() {
    let id = $('#formEditAd').find('input[name=id]').val();
    let title = $('#formEditAd').find('input[name=title]').val();
    let publisher = sessionStorage.getItem('username');
    let description = $('#formEditAd').find('textarea[name=description]').val();
    let datePublished = $('#formEditAd').find('input[name=datePublished]').val();
    let price = $('#formEditAd').find('input[name=price]').val();
    let obj = {
        'title': title,
        'publisher': publisher,
        'description': description,
        'date of publishing': datePublished,
        'price': price
    };
    $.ajax({
        method: 'PUT',
        url: BASE_URL + 'appdata/' + APP_KEY + '/advertisements/' + id,
        headers: {'Authorization': 'Kinvey ' + sessionStorage.getItem('authToken')},
        data: obj
    }).then(function (res) {
        listAds();
        showView('viewAds');
        showInfo('Ad edited.');
    })
}

function listAds() {
    $.ajax({
        url: BASE_URL + 'appdata/' + APP_KEY + '/advertisements ',
        headers: {'Authorization': 'Kinvey ' + sessionStorage.getItem('authToken')}
    }).then(function (res) {
        showView('viewAds');
        let table = $('#ads').find('table');
        table.find('tr').each((index, el) => {
            if (index > 0) {
                $(el).remove()
            }
        });

        for (let ad of res.reverse()) {
            let tr = $('<tr>');
            table.append(
                $(tr).append($(`<td>${ad.title}</td>`))
                    .append($(`<td>${ad.publisher}</td>`))
                    .append($(`<td>${ad.description}</td>`))
                    .append($(`<td>${ad.price}</td>`))
                    .append($(`<td>${ad['date of publishing']}</td>`))
            );
            if (ad._acl.creator === sessionStorage.getItem('userId')) {
                $(tr).append(
                    $(`<td>`).append(
                        $(`<a href="#">[Edit]</a>`).on('click', function () {
                            loadAdForEdit(ad)
                        })
                    ).append(
                        $(`<a href="#">[Delete]</a>`).on('click', function () {
                            deleteAd(ad)
                        })
                    )
                )
            }
        }
    }).catch(handleAjaxError)
}

function handleAjaxError(response) {
    let errorMsg = JSON.stringify(response);
    if (response.readyState === 0)
        errorMsg = "Cannot connect due to network error.";
    if (response.responseJSON && response.responseJSON.description)
        errorMsg = response.responseJSON.description;
    if (response.message === 'The request body is either missing or incomplete') {
        errorMsg = response.message
    }
    showError(errorMsg)
}
