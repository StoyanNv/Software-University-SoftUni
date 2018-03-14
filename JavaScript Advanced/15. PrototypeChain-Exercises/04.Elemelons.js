function solve() {

    class Melon {
        constructor(weight, melonSort) {
            if (new.target === Melon) {
                throw new Error("Cannot instantiate directly");
            }
            this.weight = Number(weight);
            this.melonSort = melonSort;
            this._elementIndex = this.weight * this.melonSort.length;
        }

        get elementIndex() {
            return this._elementIndex;
        }

        toString() {
            return `Element: ${(this.constructor.name.slice(0, -5))}\nSort: ${this.melonSort}\nElement Index: ${this._elementIndex}`
        }
    }

    class Watermelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort)
        }
    }

    class Firemelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort)
        }
    }

    class Earthmelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort)
        }
    }

    class Airmelon extends Melon {
        constructor(weight, melonSort) {
            super(weight, melonSort)
        }
    }

    class Melolemonmelon extends Watermelon {

        constructor(weight, melonSort) {
            super(weight, melonSort);
            this.element = "Water";
            this.elements = ["Fire", "Earth", "Air", "Water"];
            this.index = 0;
        }

        morph() {
            this.element = this.elements[this.index++]
        }

        toString() {
            return `Element: ${this.element}\nSort: ${this.melonSort}\nElement Index: ${this._elementIndex}`
        }
    }

    return {
        Melon,
        Watermelon,
        Firemelon,
        Earthmelon,
        Airmelon,
        Melolemonmelon
    }
}