function attachEvents() {
    const URL = 'https://baas.kinvey.com/appdata/kid_BkT7qNT9M/';
    const USERNAME = 'peter';
    const PASSWORD = 'p';
    const BASE64 = btoa(`${USERNAME}:${PASSWORD}`);
    const AUTH = {"Authorization": "Basic " + BASE64};
    let posts = {};

    $('#btnLoadPosts').on('click', loadPosts);
    $('#btnViewPost').on('click', viewPosts);

    function loadPosts() {
        $.ajax({
            method:'GET',
            url : URL + 'posts',
            headers : AUTH
        }).then(function (res) {
            $('#posts').empty();
            for (let obj of res) {
                posts[obj.title] = {
                    id : obj._id,
                    body : obj.body
                };
                $('#posts').append(`<option>${obj.title}</option>`);
            }
        }).catch(function (err) {
            console.log(err)
        })
    }

    function viewPosts() {
        let selected = $('#posts').find(':selected').text();
        $.ajax({
            method:'GET',
            url : URL + `comments/?query={"post_id":"${posts[selected].id}"}`,
            headers : AUTH
        }).then(function (res) {
            $('#post-comments').empty();
            $('#post-title').text(selected);
            $('#post-body').text(posts[selected].body);
            for (let obj of res) {
                $('#post-comments').append(`<li>${obj.text}</li>`)
            }
        }).catch(function (err) {
            console.log(err)
        })
    }
}