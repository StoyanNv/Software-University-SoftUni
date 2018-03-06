function solve() {
    let obj = {
        extend: function (template) {
            for (let el in template) {
                if (typeof (template[el]) === 'function') {
                    obj.__proto__[el] = template[el];
                }
                else {
                    obj[el] = template[el];
                }
            }
        }
    };
    return obj
}