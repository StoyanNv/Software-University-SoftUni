let expect = require('chai').expect;
const RGB_TO_HEX = require('../06.RGBТoHex');

describe("RGB_TO_HEX(red, green, blue)", function () {
    describe("Nominal cases (valid input)", function () {
        it("should return #FFFFFF", function () {
            expect(RGB_TO_HEX(255, 255, 255)).to.be.equal('#FFFFFF');
        });

        it("should return #000000", function () {
            expect(RGB_TO_HEX(0, 0, 0)).to.be.equal('#000000');
        });

        it("should return #0C0D0E", function () {
            expect(RGB_TO_HEX(12, 13, 14)).to.be.equal('#0C0D0E');
        });
    });
    describe("Special cases (invalid input)", function () {
        it("should return undefined on (1, 'test', 1)", function () {
            let hex = RGB_TO_HEX(1, 'test', 1);
            expect(hex).to.be.undefined;
        });
        it("should return undefined on ('test', 1, 1)", function () {
            let hex = RGB_TO_HEX('test', 1, 1);
            expect(hex).to.be.undefined;
        });
        it("should return undefined on (1, 1, 'test')", function () {
            let hex = RGB_TO_HEX(1, 1, 'test');
            expect(hex).to.be.undefined;
        });
        it("should return undefined on (-1, 1, 0')", function () {
            let hex = RGB_TO_HEX(-1, 1, 0);
            expect(hex).to.be.undefined;
        });
        it("should return undefined on (1, -1, 0')", function () {
            let hex = RGB_TO_HEX(1, -1, 0);
            expect(hex).to.be.undefined;
        });
        it("should return undefined on (1, 1, -1')", function () {
            let hex = RGB_TO_HEX(1, 1, -1);
            expect(hex).to.be.undefined;
        });
        it("should return undefined on (256, 1, 0')", function () {
            let hex = RGB_TO_HEX(256, 1, 0);
            expect(hex).to.be.undefined;
        });
        it("should return undefined on (1, 256, 0')", function () {
            let hex = RGB_TO_HEX(1, 256, 0);
            expect(hex).to.be.undefined;
        });
        it("should return undefined on (1, 1, 256')", function () {
            let hex = RGB_TO_HEX(1, 1, 256);
            expect(hex).to.be.undefined;
        });
    });
});