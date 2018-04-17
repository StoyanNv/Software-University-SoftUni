let expect = require('chai').expect;

class Sumator {
    constructor() {
        this.data = [];
    }

    add(item) {
        this.data.push(item);
    }

    sumNums() {
        let sum = 0;
        for (let item of this.data)
            if (typeof (item) === 'number') {
                sum += item;
            }
        return sum;
    }

    removeByFilter(filterFunc) {
        this.data = this.data.filter(x => !filterFunc(x));
    }

    toString() {
        if (this.data.length > 0) {
            return this.data.join(", ");
        }
        else {
            return '(empty)';
        }
    }
}
describe("", function () {
    let sumator;
    beforeEach(function () {
        sumator = new Sumator();
    });
    it("The Sumator must have add property.", function () {
        expect(Sumator.prototype.hasOwnProperty('add')).to.be.equal(true)
    });
    it("The data property must be empty by default", function () {
        expect(sumator.data.length).to.be.equal(0)
    });
    it("The data property should be able to collect different types of data.", function () {
        sumator.add(5);
        sumator.add('gosho');
        sumator.add({});
        expect(sumator.data.length).to.be.equal(3)
    });
    it("The Sumator must sum positive numbers.", function () {
        sumator.add(5);
        sumator.add(5);
        sumator.add(5);
        expect(sumator.sumNums()).to.be.equal(15)
    });
    it("The Sumator must sum floating point numbers.", function () {
        sumator.add(5.5);
        sumator.add(5.5);
        sumator.add(5.5);
        expect(sumator.sumNums()).to.be.equal(16.5)
    });
    it("The Sumator must sum negative numbers", function () {
        sumator.add(-5);
        sumator.add(-5);
        sumator.add(-5);
        expect(sumator.sumNums()).to.be.equal(-15)
    });
    it("The Sumator must sum only numbers", function () {
        sumator.add(5);
        sumator.add(5.5);
        sumator.add(-5);
        sumator.add(0);
        sumator.add({});
        sumator.add('gosho');
        expect(sumator.sumNums()).to.be.equal(5.5)
    });
    it("The Sumator must sum floating point numbers", function () {
        sumator.add(5);
        sumator.add(5);
        sumator.add({});
        sumator.add('gosho');
        expect(sumator.sumNums()).to.be.equal(10)
    });
    it("The Sumator must sum floating point numbers", function () {
        sumator.add(function () {

        });
        sumator.add({});
        sumator.add('gosho');
        expect(sumator.sumNums()).to.be.equal(0)
    });
    it("The toString function should return the string values of the data property", function () {
        sumator.add(5);
        sumator.add(5);
        sumator.add(5);
        sumator.add('gosho');
        expect(sumator.toString()).to.be.equal('5, 5, 5, gosho')
    });
    it("The toString function should return (empty) if data property is empty.", function () {
        expect(sumator.toString()).to.be.equal('(empty)')
    });
    it("The removeByFilter(x => x > 6) function should remove all numbers greater then 6.", function () {
        sumator.add(5);
        sumator.add(6);
        sumator.add(7);
        sumator.add('gosho');
        sumator.removeByFilter(x => x > 6);
        expect(sumator.toString()).to.be.equal('5, 6, gosho')
    });
    it("The removeByFilter(x => x.length === 5) function should remove all elements with length equal to 5.", function () {
        sumator.add(5);
        sumator.add(6);
        sumator.add(7);
        sumator.add('gosho');
        sumator.removeByFilter(x => x.length === 5);
        expect(sumator.toString()).to.be.equal('5, 6, 7')
    });
});