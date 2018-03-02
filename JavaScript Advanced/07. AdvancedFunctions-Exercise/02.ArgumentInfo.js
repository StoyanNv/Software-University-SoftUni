function result() {
    let argumentsList = arguments;
    let count = new Map();
    for (let i = 0; i < argumentsList.length; i++) {
        console.log(typeof(argumentsList[i]) + ': ' + argumentsList[i]);
        if (count.get(typeof(argumentsList[i]))) {
            count.set(typeof(argumentsList[i]), count.get(typeof(argumentsList[i])) + 1);
        } else {
            count.set(typeof(argumentsList[i]), 1);
        }
    }
    [...count].sort((a, b) => b[1] - a[1]).forEach(md => {
        console.log(`${md[0]} = ${md[1]}`);
    });
}


