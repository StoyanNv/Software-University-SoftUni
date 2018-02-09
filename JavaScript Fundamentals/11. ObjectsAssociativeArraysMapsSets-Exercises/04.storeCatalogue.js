function storeCatalogue(arr) {
    let catalogue = {};
    for (let obj of arr) {
        let tokens = obj.split(' : ');
        let product = tokens[0];
        let price = tokens[1];
        let key = product[0];
        if (catalogue.hasOwnProperty(key)) {
            let obj = {product: product, price: price};
            catalogue[key].push(obj);
        }
        else {
            let obj = {product: product, price: price};
            catalogue[key] = [];
            catalogue[key].push(obj);
        }

    }
    let keys = Object.keys(catalogue).sort();
    for (let key of keys) {
        console.log(key);
        let sortedArr = catalogue[key].sort(function (a, b) {
            return (a.product > b.product) ? 1 : ((b.product > a.product) ? -1 : 0);
        });
        for (let product of sortedArr) {
            console.log(`  ${product['product']}: ${product['price']} `)
        }
    }
}

storeCatalogue(['Appricot : 20.4',
    'Fridge : 1500',
    'TV : 1499',
    'Deodorant : 10',
    'Boiler : 300',
    'Apple : 1.25',
    'Anti-Bug Spray : 15',
    'T-Shirt : 10']
);