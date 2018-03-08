let expect = require('chai').expect;
const SUM_OF_NUMBERS = require('../04.SumOfNumbers');

describe("sum(arr) - sum array of numbers", function () {
    it("should return 3 for [1, 2]", function () {
        expect(SUM_OF_NUMBERS([1, 2])).to.be.equal(3);
    });
    it("should return 1 for [1]", function () {
        expect(SUM_OF_NUMBERS([1])).to.be.equal(1);
    });
    it("should return 0 for empty array", function () {
        expect(SUM_OF_NUMBERS([])).to.be.equal(0);
    });
    it("should return 3 for [1.5, 2.5, -1]", function () {
        expect(SUM_OF_NUMBERS([1.5, 2.5, -1])).to.be.equal(3);
    });
    it("should return NaN for invalid data", function () {
        expect(SUM_OF_NUMBERS(['test'])).to.be.NaN
    });
});
