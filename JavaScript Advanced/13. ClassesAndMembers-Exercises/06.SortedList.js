class Sorted {
    constructor() {
        this.arr = [];
        this.size = 0;
    }
    add(value) {
        this.arr.push(value);
        this.size++;
        this.arr.sort((a, b) => {
            return a - b
        })
    }
    remove(index) {
        if (index >= 0 && index < this.arr.length) {
            this.arr.splice(index, 1);
            this.size--;
        }
    }
    get(index) {
        if (index >= 0 && index < this.arr.length) {
            return this.arr[index];
        }
    }
}