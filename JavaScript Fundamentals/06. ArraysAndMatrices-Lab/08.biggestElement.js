function biggestElement(matrix) {
    console.log(Math.max.apply(Math, matrix.map(arr => Math.max.apply(Math, arr))));
}