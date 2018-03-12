let Extensible= (function () {
    let id = 0;
    return class obj {
        constructor() {
            this.id = id++;
        }
        extend(template) {
            for (let el in template) {
                if (typeof (template[el]) === 'function') {
                    obj.prototype[el] = template[el];
                }
                else {
                    this[el] = template[el];
                }
            }
        }
    }
})();
let obj1 = new Extensible();
let obj2 = new Extensible();
let obj3 = new Extensible();
console.log(obj1.id);
console.log(obj2.id);
console.log(obj3.id);
