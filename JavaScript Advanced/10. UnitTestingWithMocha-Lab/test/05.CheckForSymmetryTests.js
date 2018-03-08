let expect = require('chai').expect;
const SYMMETRY = require('../05.CheckForSymmetry');

describe("Check For Symmetry", function () {
    it("check if function takes array as argument", function () {
        expect(SYMMETRY('test')).to.be.equal(false);
    });
    it("should return true for [1,2,3,3,2,1]", function () {
        let symmetric = SYMMETRY([1, 2, 3, 3, 2, 1]);
        expect(symmetric).to.be.equal(true);
    });
    it("should return true for [1,2,3,4,2,1]", function () {
        let symmetric = SYMMETRY([1, 2, 3, 4, 2, 1]);
        expect(symmetric).to.be.equal(false);
    });
    it("should return false for [-1,2,1]", function () {
        let symmetric = SYMMETRY([-1, 2, 1]);
        expect(symmetric).to.be.equal(false);
    });
    it("should return true for [-1,2,-1]", function () {
        let symmetric = SYMMETRY([-1, 2, -1]);
        expect(symmetric).to.be.equal(true);
    });
    it("should return TRUE", function () {
        expect(SYMMETRY([5, 'hi', {
            a: 5
        }, new Date(), {
            a: 5
        }, 'hi', 5])).to.be.equal(true);
    });
});