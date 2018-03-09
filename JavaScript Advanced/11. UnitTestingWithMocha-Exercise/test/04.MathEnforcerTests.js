let expect = require('chai').expect;
const MATH_ENFORCER = require('../04.MathEnforcer');

describe("mathEnforcer", function () {
    it('Add Five with 5 should return 10', function () {
        expect(MATH_ENFORCER.addFive(5)).to.be.equal(10)
    });
    it('Subtract Ten with 15 should return 5', function () {
        expect(MATH_ENFORCER.subtractTen(15)).to.be.equal(5)
    });
    it('Sum with 5 and 5 should return 10', function () {
        expect(MATH_ENFORCER.sum(5, 5)).to.be.equal(10)
    });
    it('Add Five with 5.5 should return 10.5', function () {
        expect(MATH_ENFORCER.addFive(5.5)).to.be.equal(10.5)
    });
    it('Subtract Ten with 15.5 should return 5.5', function () {
        expect(MATH_ENFORCER.subtractTen(15.5)).to.be.equal(5.5)
    });
    it('Sum with 5.5 and 5.5 should return 11', function () {
        expect(MATH_ENFORCER.sum(5.5, 5.5)).to.be.equal(11)
    });
    it('Add Five with "5" should return undefined', function () {
        expect(MATH_ENFORCER.addFive('5')).to.be.undefined
    });
    it('Subtract Ten with "5" should return undefined', function () {
        expect(MATH_ENFORCER.subtractTen('5')).to.be.undefined
    });
    it('Sum with "5" and 5 should return undefined', function () {
        expect(MATH_ENFORCER.sum('5', 5)).to.be.undefined
    });
    it('Sum with 5 and "5" should return undefined', function () {
        expect(MATH_ENFORCER.sum(5, '5')).to.be.undefined
    });
    it('Add Five with -5 should return 0', function () {
        expect(MATH_ENFORCER.addFive(-5)).to.be.equal(0)
    });
    it('Subtract Ten with -10 should return -20', function () {
        expect(MATH_ENFORCER.subtractTen(-10)).to.be.equal(-20)
    });
});