function cityMarkets(arr) {
    let townsWithProducts = new Map();
    for (let obj of arr) {
        let tokens = obj.split(/\s*->\s*/).filter(e => e !== '');
        let town = tokens[0];
        let product = tokens[1];
        let income = tokens[2].split(/\s*:\s*/).filter(e => e !== '').reduce(function (a, b) {
            return a * b;
        });
        if (!townsWithProducts.has(town)) {
            townsWithProducts.set(town, new Map());
        }
        let oldIncome = townsWithProducts.get(town).get(product);
        if (oldIncome) {
            income += oldIncome;
        }
        townsWithProducts.get(town).set(product, income);
    }
    let res = ``;
    for (let [name, obj] of townsWithProducts) {
        res += `Town - ${name}\n`;
        for (let [product, income] of obj) {
            res += `$$$${product} : ${income}\n`;
        }
    }
    return res.trim();
}