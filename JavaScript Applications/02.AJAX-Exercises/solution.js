function attachEvents() {
    $('#submit').on('click', send);
    $('#refresh').on('click', refresh);

    $.ajax({
        method: 'GET',
        url: 'https://messenger-5747d.firebaseio.com/messenger.json',
        success: handleSucces,
        error: handleError
    });

    function handleSucces(res) {
        for (let msg in res) {
            $('#messages').append(res[msg].author + ': ' + res[msg].content + '\n')
        }
    }

    function handleError(err) {
        console.log(err)
    }

    function send() {
        let author = $('#author').val();
        let content = $('#content').val();
        let timestamp = Date.now();
        let obj = JSON.stringify({author, content, timestamp});
        $.ajax({
            method: 'POST',
            url: 'https://messenger-5747d.firebaseio.com/messenger.json',
            data: obj,
            success: handleSucces,
            error: handleError
        });

        function handleSucces(res) {
            // for (let msg in res) {
            //     $('#messages').append(res[msg].author + ': ' + res[msg].content + '\n')
            // }
        }

        function handleError(err) {
            console.log(err)
        }
         author = $('#author').val('');
         content = $('#content').val('');
    }

    function refresh() {
        $.ajax({
            method: 'GET',
            url: 'https://messenger-5747d.firebaseio.com/messenger.json',
            success: handleSucces,
            error: handleError
        });

        function handleSucces(res) {
            $('#messages').empty();
            for (let msg in res) {
                $('#messages').append(res[msg].author + ': ' + res[msg].content + '\n')
            }
        }

        function handleError(err) {
            console.log(err)
        }
    }
}