function printProductionStatistics(params) {
    let stat = new Map();

    for (const row of params) {
        let [brand, model, coundAsString] = row.split(' | ');
        let producedCount = Number(coundAsString);

        if (!stat.get(brand)) {
            stat.set(brand, new Map().set(model, producedCount));
        } else if (!stat.get(brand).get(model)) {
            stat.get(brand).set(model, producedCount);
        } else {
            stat.set(brand, stat.get(brand).set(model, stat.get(brand).get(model) + producedCount));
        }
    }
    for (let [key,value] of stat) {
        console.log(key);
        for (let [model, quantity] of value) {
            console.log(`###${model} -> ${quantity}`)
        }
    }
}