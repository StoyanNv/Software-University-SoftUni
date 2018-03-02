function sortArray(arr, sortMethod) {
    let ascSort = function (a, b) {
        return a - b;
    };
    let descSort = function (a, b) {
        return b - a;
    };
    let sortStrategies = {
        'asc': ascSort,
        'desc': descSort
    };
    return arr.sort(sortStrategies[sortMethod])
}