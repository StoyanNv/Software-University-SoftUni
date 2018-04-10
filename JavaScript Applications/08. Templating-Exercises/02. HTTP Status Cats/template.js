$(() => {
    async function renderCatTemplate() {
        let cats = window.cats;
        let cat = await $.get('catTemplate.hbs');
        let catTemplate = Handlebars.compile(cat);
        let obj = {cats};
        $('#allCats').append(catTemplate(obj))
    }
    renderCatTemplate();
});
function show(id) {
    let btn = $(`#btn${id}`);
    if (btn.text() === "Show status code") {
        btn.text('Hide Status Code')
    }
    else{
        btn.text("Show status code")
    }
    $(`#${id}`).toggle();
}