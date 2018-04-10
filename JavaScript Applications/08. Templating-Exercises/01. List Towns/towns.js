function attachEvents() {
    $('#btnLoadTowns').on('click', listTowns);

    async function listTowns() {
        $('#root').empty();
        let listHtml = await $.get('template.html');
        let towns = $('#towns').val().split(', ');
        if (towns[0] !== '') {
            let listTemplate = Handlebars.compile(listHtml);
            let obj = {
                towns: towns
            };
            let list = listTemplate(obj);
            $('#root').append(list);
        }
    }
}