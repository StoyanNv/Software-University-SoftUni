function populationInTowns(dataRows) {
    let total = new Map();
    for (let i = 0; i < dataRows.length; i++) {
        let currRow = dataRows[i].split(/\s*<->\s*/).filter(e => e !== '');
        if (total.has(currRow[0])) {
            total.set(currRow[0], total.get(currRow[0]) + Number(currRow[1]));
        }
        else {
            total.set(currRow[0], Number(currRow[1]));
        }
    }
    for (let [town, sum] of total) {
        console.log(town + " : " + sum);
    }
}