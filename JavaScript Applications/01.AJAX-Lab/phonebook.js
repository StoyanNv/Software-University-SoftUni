const URL = `https://phonebook-c8f83.firebaseio.com/phonebook`;

$('#btnLoad').on('click', loadData);
$('#btnCreate').on('click', postData);

function postData() {
    let person = $('#person').val();
    let phone = $('#phone').val();
    let postData = JSON.stringify({person, phone});
    let personVal = person;
    let phoneVal = phone;
    $.ajax({
        method: 'POST',
        url: URL + '.json',
        data: postData,
        success: appendElement,
        error: handleError
    });
    console.log(person, phone);

    function appendElement(res) {
        generateLi(personVal, phoneVal, res.name)
    }

    person = $('#person').val('');
    phone = $('#phone').val('');
}

function loadData() {
    $('#phonebook').empty();
    $.ajax({
        method: 'GET',
        url: URL + '.json',
    }).then(handleSuccess)
        .catch(handleError);

    function handleSuccess(res) {
        for (let key in res) {
            generateLi(res[key].person, res[key].phone, key)
        }
    }
}

function generateLi(person, phone, key) {
    let li = $(`<li>${person}: ${phone} </li>`)
        .append($('<a href="#">[Delete]</a>')
            .click(function () {
                $.ajax({
                    method: 'DELETE',
                    url: URL + '/' + key + '.json'
                }).then($(li).remove())
                    .catch(handleError)
            }));
    $('#phonebook').append(li);
}

function handleError(err) {
    console.log(err)
}