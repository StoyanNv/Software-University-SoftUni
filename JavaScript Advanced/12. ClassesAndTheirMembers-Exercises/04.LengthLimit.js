class Stringer {
    constructor(innerString, innerLength) {
        this.innerString = innerString;
        this.innerLength = innerLength;
    }

    get innerLength() {
        return this._innerLength;
    }

    set innerLength(len) {
        if (Number(len) < 0) {
            this._innerLength = 0;
        }
        else {
            this._innerLength = len;
        }
    }

    toString() {
        if (this._innerLength === 0) {
            return "..."
        }
        if (this.innerString.length > this._innerLength) {
            return this.innerString.substr(0, this._innerLength) + "..."
        }
        return this.innerString.substr(0, this._innerLength)
    }

    increase(value) {
        this.innerLength += Number(value)
    }

    decrease(value) {
        this.innerLength -= Number(value)
    }
}