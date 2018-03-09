let expect = require('chai').expect;
const CHAR_LOOKUP = require('../03.CharLookup');

describe("lookupChar", function () {
    it('with an integer as first parameter, should return undefined', function () {
        expect(CHAR_LOOKUP(0, 0)).to.be.undefined
    });
    it('with a string as second parameter, should return undefined', function () {
        expect(CHAR_LOOKUP('a', '0')).to.be.undefined
    });
    it('with a double as second parameter, should return undefined', function () {
        expect(CHAR_LOOKUP('test', 1.1)).to.be.undefined
    });
    it('with a negative integer as second parameter, should print Incorrect index', function () {
        expect(CHAR_LOOKUP('test', -1)).to.be.equal('Incorrect index')
    });

    it('with an index bigger then string.length as second parameter, should print Incorrect index', function () {
        expect(CHAR_LOOKUP('add', 3)).to.be.equal('Incorrect index')
    });

    it('with add as first parameter and 2 as second parameter, should return d', function () {
        expect(CHAR_LOOKUP('add', 2)).to.be.equal('d')
    });
});