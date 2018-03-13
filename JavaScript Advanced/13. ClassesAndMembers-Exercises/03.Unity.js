class Rat {
    constructor(name) {
        this.name = name;
        this.rats = [];
    }

    unite(otherRat) {
        if (typeof(otherRat) === 'object') {
            this.rats.push(otherRat)
        }
    }

    toString() {
        return this.name + this.rats.map(r => '\n' + '##' + r.name).join('');
    }

    getRats() {
        return this.rats
    }
}