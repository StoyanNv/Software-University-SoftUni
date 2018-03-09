let expect = require('chai').expect;
const EVEN_OR_ODD = require('../02.EvenOrOdd');

function isOddOrEven(string) {
    if (typeof(string) !== 'string') {
        return undefined;
    }
    if (string.length % 2 === 0) {
        return "even";
    }

    return "odd";
}

describe("isOddOrEven", function () {
    it('with a number parameter, should return undefined', function () {
        expect(EVEN_OR_ODD(13)).to.be.undefined
    });
    it('with an empty string parameter, should return even', function () {
        expect(EVEN_OR_ODD('')).to.be.equal('even')
    });
    it('with an even string parameter, should return even', function () {
        expect(EVEN_OR_ODD('even')).to.be.equal('even')
    });

    it('with an odd string parameter, should return odd', function () {
        expect(EVEN_OR_ODD('odd')).to.be.equal('odd')
    });
});
