let expect = require('chai').expect;
let jsdom = require('jsdom-global')();
let $ = require('jquery');
const SHARED_OBJECT = require('../05.SharedObject');

document.body.innerHTML =
    `<body>
<div id="wrapper">
    <input type="text" id="name">
    <input type="text" id="income">
</div>
</body>`;


describe('Shared Object Tests', function () {
    //changeName function
    it('Test changeName() with empty string', function () {
        SHARED_OBJECT.changeName('');
        expect(SHARED_OBJECT.name).to.be.null
    });

    it('Test changeName() with empty string NAME Field Value"', function () {
        SHARED_OBJECT.changeName('');
        expect($('#name').val()).to.be.equal('');
    });

    it('Test changeName() with valid string NAME Field Value"', function () {
        SHARED_OBJECT.changeName('Ivan');
        expect($('#name').val()).to.be.equal('Ivan');
    });

    //changeIncome
    it('Test changeIncome() with floatng point Number', function () {
        SHARED_OBJECT.changeIncome(5.5);
        expect(SHARED_OBJECT.income).to.be.null;
    });

    it('Test changeIncome() with Negative Number', function () {
        SHARED_OBJECT.changeIncome(-5);
        expect(SHARED_OBJECT.income).to.be.null;
    });

    it('Test changeIncome() with Number Zero ', function () {
        SHARED_OBJECT.changeIncome(0);
        expect(SHARED_OBJECT.income).to.be.null;
    });

    it('Test changeIncome() with a valid Number', function () {
        SHARED_OBJECT.changeIncome(10);
        expect(SHARED_OBJECT.income).to.be.equal(10);
    });

    it('Test changeIncome() with a valid Number INCOME Field Value', function () {
        SHARED_OBJECT.changeIncome(20);
        let incomeField = $('#income');
        expect(incomeField.val()).to.be.equal('20');
    });
    //updateName
    it('Test updateName() with a valid string', function () {
        $('#name').val('Stefan');
        SHARED_OBJECT.updateName();
        expect(SHARED_OBJECT.name).to.be.equal('Stefan');
    });
    it('Test updateName() with a empty string', function () {
        SHARED_OBJECT.changeName('Ivan');
        $('#name').val('');
        SHARED_OBJECT.updateName();
        expect(SHARED_OBJECT.name).to.be.equal('Ivan');
    });
    //updateIncome
    it('Test updateIncome() with a invalid value', function () {
        SHARED_OBJECT.changeIncome(5);
        $('#income').val('a');
        SHARED_OBJECT.updateIncome();
        expect(SHARED_OBJECT.income).to.be.equal(5);
    });
    it('Test updateIncome() with a valid value', function () {
        SHARED_OBJECT.changeIncome(5);
        $('#income').val('6');
        SHARED_OBJECT.updateIncome();
        expect(SHARED_OBJECT.income).to.be.equal(6);
    });
    it('Test updateIncome() with a negative number', function () {
        SHARED_OBJECT.changeIncome(5);
        $('#income').val(-6);
        SHARED_OBJECT.updateIncome();
        expect(SHARED_OBJECT.income).to.be.equal(5);
    });

    it('Test updateIncome() with zero', function () {
        SHARED_OBJECT.changeIncome(5);
        $('#income').val(0);
        SHARED_OBJECT.updateIncome();
        expect(SHARED_OBJECT.income).to.be.equal(5);
    });
});