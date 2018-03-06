(function solve() {
    Array.prototype.last = function () {
        return this[this.length - 1]
    };
    Array.prototype.skip = function (n) {
        let res = [];
        for (let i = n; i < this.length; i++) {
            res.push(this[i]);
        }
        return res
    };
    Array.prototype.take = function (n) {
        let res = [];
        for (let i = 0; i < n; i++) {
            res.push(this[i]);
        }
        return res
    };
    Array.prototype.sum = function () {
        let sum = 0;
        for (let i = 0; i < this.length; i++) {
            sum += this[i];
        }
        return sum;
    };
    Array.prototype.average = function () {
        return this.sum() / this.length;
    }
})();