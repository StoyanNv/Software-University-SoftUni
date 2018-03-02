let manager = (
    function name() {
        let bool = true;
        let ingredients = {
            protein: 0,
            carbohydrate: 0,
            fat: 0,
            flavour: 0
        };
        let food = {
            apple: {carbohydrate: 1, flavour: 2},
            coke: {carbohydrate: 10, flavour: 20},
            burger: {carbohydrate: 5, fat: 7, flavour: 3},
            omelet: {protein: 5, fat: 1, flavour: 1},
            cheverme: {protein: 10, carbohydrate: 10, fat: 10, flavour: 10}
        };

        //
        return (cmd) => {
            if (cmd.split(' ')[0] === 'restock') {
                ingredients[cmd.split(' ')[1]] += Number(cmd.split(' ')[2]);
                return 'Success'

            }
            else if (cmd.split(' ')[0] === 'prepare') {
                let product = cmd.split(' ')[1];
                let quantity = Number(cmd.split(' ')[2]);

                if (food[product]['protein'] !== undefined) {
                    if (food[product]['protein'] * quantity <= ingredients.protein) {
                        bool = true;
                    } else {
                        bool = false;
                        return `Error: not enough protein in stock`;
                    }
                }

                if (food[product]['carbohydrate'] !== undefined) {
                    if (food[product]['carbohydrate'] * quantity <= ingredients.carbohydrate) {
                        bool = true;
                    } else {
                        bool = false;
                        return `Error: not enough carbohydrate in stock`;
                    }
                }
                if (food[product]['fat'] !== undefined) {
                    if (food[product]['fat'] * quantity <= ingredients.fat) {
                        bool = true;

                    } else {
                        bool = false;
                        return `Error: not enough fat in stock`;
                    }
                }
                if (food[product]['flavour'] !== undefined) {
                    if (food[product]['flavour'] * quantity <= ingredients.flavour) {
                        bool = true;
                    } else {
                        bool = false;
                        return `Error: not enough flavour in stock`;
                    }
                }
                if (bool) {
                    if (food[product]['fat'] !== undefined) {
                        ingredients.fat -= food[product]['fat'] * quantity;
                    }
                    if (food[product]['protein'] !== undefined) {
                        ingredients.protein -= food[product]['protein'] * quantity;
                    }
                    if (food[product]['carbohydrate'] !== undefined) {
                        ingredients.carbohydrate -= food[product]['carbohydrate'] * quantity;
                    }
                    if (food[product]['flavour'] !== undefined) {
                        ingredients.flavour -= food[product]['flavour'] * quantity;
                    }

                    return 'Success'
                }
            }

            else if (cmd.split(' ')[0] === 'report') {
                return `protein=${ingredients.protein} carbohydrate=${ingredients.carbohydrate} fat=${ingredients.fat} flavour=${ingredients.flavour}`
            }
        }
    }
)();
console.log('Test 1');
console.log(robot("restock flavour 50"));
console.log(robot("prepare coke 4"));
robot()

console.log('\nTest 2');
console.log(robot("restock carbohydrate 10"));
console.log(robot("restock flavour 10"));
console.log(robot("prepare apple 1"));
console.log(robot("restock fat 10"));
console.log(robot("prepare burger 1"));
console.log(robot("report"));
// manager("restock carbohydrate 10");
// manager("restock flavour 10");
// manager("prepare apple 1");
// manager("restock fat 10");
// manager("prepare burger 1");
// manager("report");

// manager('prepare cheverme 1');
// manager('restock protein 10');
// manager('prepare cheverme 1');
// manager('restock carbohydrate 10');
// manager('prepare cheverme 1');
// manager('restock fat 10');
// manager('prepare cheverme 1');
// manager('restock flavour 10');
// manager('prepare cheverme 1');
// manager('report');