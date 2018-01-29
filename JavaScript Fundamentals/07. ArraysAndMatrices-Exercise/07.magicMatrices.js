function isMagicMatrix(matrix) {
    let magicalNumber = Number.MIN_SAFE_INTEGER;
    for (let i = 0; i < matrix.length; i++) {
        if (magicalNumber !== matrix[i].reduce((a, b) => a + b) && i !== 0) {
            return false
        }
        magicalNumber = matrix[i].reduce((a, b) => a + b);
         //Col check
        let colSum = [];
        for (let j = 0; j < matrix.length; j++) {
            colSum.push(matrix[j][i]);
        }
        if (colSum.reduce((a, b) => a + b) !== magicalNumber) {
            return false;
        }
        else {
            colSum = [];
        }
    }
    return true
}

console.log(isMagicMatrix(
    [[4, 5, 6],
        [6, 5, 4],
        [5, 5, 5]]
));
