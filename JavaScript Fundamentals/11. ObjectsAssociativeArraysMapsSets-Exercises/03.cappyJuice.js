function cappyJuice(arr) {
    let bottleCapacity = 1000;
    let bottles = new Map();
    let juiceCollection = {};

    for (let row of arr) {
        let splittedRow = row.split(' => ');
        let juiceType = splittedRow[0];
        let quantity = Number(splittedRow[1]);

        if (juiceCollection[juiceType]) {
            juiceCollection[juiceType] += quantity;
        } else {
            juiceCollection[juiceType] = quantity;
        }

        let juiceTyps = Object.keys(juiceCollection);
        for (let type of juiceTyps) {
            let bottleCount = Math.floor(juiceCollection[type] / bottleCapacity);
            if (bottleCount !== 0) {
                bottles.set(type, bottleCount)
            }
        }
    }
    return [...bottles].map(e => `${e[0]} => ${e[1]}`).join('\n');
}