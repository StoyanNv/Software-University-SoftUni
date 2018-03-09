let expect = require('chai').expect;
let jsdom = require('jsdom-global')();
let $ = require('jquery');
const nuke = require('../06.ArmageDOM');

describe("Nuke tests", function () {
    let initialHTML;
    beforeEach(function () {
        document.body.innerHTML =
            `<body>
<div id="target">
    <div class="nested target">
        <p>This is some text</p>
    </div>
    <div class="target">
        <p>Empty div</p>
    </div>
    <div class="inside">
        <span class="nested">Some more text</span>
        <span class="target">Some more text</span>
    </div>
</div>
</body>`;
        initialHTML = $('body').html();
    });

    it('Test with valid and invalid selector', function () {
        let selector1 = $('.inside');
        let selector2 = 175;
        nuke(selector1, selector2);
        let modifiedHTML = $('body').html();
        expect(modifiedHTML).to.be.equal(initialHTML);

    });
    it('Test with equal selector', function () {
        let selector1 = $('.nested');
        nuke(selector1, selector1);
        let modifiedHTML = $('body').html();
        expect(modifiedHTML).to.be.equal(initialHTML);
    });
    it('Test with valid selector', function () {
        let selector1 = $('.nested');
        let selector2 = $('.target')
        nuke(selector1, selector2);
        let modifiedHTML = selector1.html();
        expect(modifiedHTML).to.not.equal(initialHTML);
    });
    it('Test with valid selectors DO NOT DELETE ANYTHING', function () {
        let selector1 = $('.nested');
        let selector2 = $('.inside');
        nuke(selector1, selector2);
        let modifiedHTML = $('body').html();
        expect(modifiedHTML).to.be.equal(initialHTML);
    });
});