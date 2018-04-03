function attachEvents() {
    const url = ' https://baas.kinvey.com/appdata/kid_S1xGyIeiG';
    const username = 'Stoyan';
    const password = 's';
    const base64 = btoa(username + ':' + password);
    const auth = {
        'Authorization': 'Basic ' + base64,
        'Content-type': 'application/json'
    };

    $('.load').on('click', load);
    $('.add').on('click', add);

    function update() {
        let id = $(this).parent().attr('data-id');
        let angler = $(this).parent().find('.angler').val();
        let weight = $(this).parent().find('.weight').val();
        let species = $(this).parent().find('.species').val();
        let location = $(this).parent().find('.location').val();
        let bait = $(this).parent().find('.bait').val();
        let captureTime = $(this).parent().find('.captureTime').val();

        let obj = {
            "angler": angler,
            "weight": weight,
            "species": species,
            "location": location,
            "bait": bait,
            "captureTime": captureTime
        };
        $.ajax({
            method: 'PUT',
            url: url + `/biggestCatches/${id}`,
            headers: auth,
            data: JSON.stringify(obj)
        }).then(handleSucces).catch(handleError);

        function handleSucces() {
            load()
        }
    }

    function deleteCatch() {
        let id = $(this).parent().attr('data-id');
        $.ajax({
            method: 'DELETE',
            url: url + `/biggestCatches/${id}`,
            headers: auth
        }).then(handleSucces).catch(handleError);

        function handleSucces() {
            load()
        }
    }

    function load() {
        $.ajax({
            method: 'GET',
            url: url + '/biggestCatches',
            headers: auth,
        }).then(handleSucces).catch(handleError);

        function handleSucces(res) {
            $('#catches').empty();
            for (let obj of res) {
                $('#catches').append($(`  <div class="catch" data-id="${obj._id}">
            <label>Angler</label>
            <input type="text" class="angler" value="${obj.angler}"/>
            <label>Weight</label>
            <input type="number" class="weight" value="${obj.weight}"/>
            <label>Species</label>
            <input type="text" class="species" value="${obj.species}"/>
            <label>Location</label>
            <input type="text" class="location" value="${obj.location}"/>
            <label>Bait</label>
            <input type="text" class="bait" value="${obj.bait}"/>
            <label>Capture Time</label>
            <input type="number" class="captureTime" value="${obj.captureTime}"/>
        </div>`).append($('<button class="update">Update</button>').click(update))
                    .append($('<button class="delete">Delete</button>').click(deleteCatch)));
            }
        }
    }

    function add() {
        let angler = $('#addForm').find('.angler').val();
        let weight = $('#addForm').find('.weight').val();
        let species = $('#addForm').find('.species').val();
        let location = $('#addForm').find('.location').val();
        let bait = $('#addForm').find('.bait').val();
        let captureTime = $('#addForm').find('.captureTime').val();

        let obj = {
            "angler": angler,
            "weight": weight,
            "species": species,
            "location": location,
            "bait": bait,
            "captureTime": captureTime
        };
        angler = $('#addForm').find('.angler').val('');
        weight = $('#addForm').find('.weight').val('');
        species = $('#addForm').find('.species').val('');
        location = $('#addForm').find('.location').val('');
        bait = $('#addForm').find('.bait').val('');
        captureTime = $('#addForm').find('.captureTime').val('');
        $.ajax({
            method: 'POST',
            url: url + '/biggestCatches',
            headers: auth,
            data: JSON.stringify(obj)

        }).then(handleSucces).catch(handleError);

        function handleSucces() {
            load()
        }
    }

    function handleError(err) {
        console.log(err)
    }
}