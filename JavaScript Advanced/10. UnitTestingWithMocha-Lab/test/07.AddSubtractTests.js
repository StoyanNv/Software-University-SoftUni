let expect = require('chai').expect;
const ADD_SUBTRACT = require('../07.AddSubtract');

describe("createCalculator()", function() {
    let calc;
    beforeEach(function() {
        calc = ADD_SUBTRACT();
    });
    it("should return 5 after {add 3; add 2}", function() {
        calc.add(3); calc.add(2); let value = calc.get();
        expect(value).to.be.equal(5);
    });
    it("should return -1 after {add -3; add 2}", function() {
        calc.add(-3); calc.add(2); let value = calc.get();
        expect(value).to.be.equal(-1);
    });
    it("should return 3 after {subtract -3}", function() {
        calc.subtract(-3); let value = calc.get();
        expect(value).to.be.equal(3);
    });
    it("should return NaN after {add 'test'}", function() {
        calc.subtract('test'); let value = calc.get();
        expect(value).to.be.NaN
    });
    it("should return -100", function () {
        calc.add('-99');
        calc.add(-1);
        expect(calc.get()).to.be.equal(-100);
    });
});
