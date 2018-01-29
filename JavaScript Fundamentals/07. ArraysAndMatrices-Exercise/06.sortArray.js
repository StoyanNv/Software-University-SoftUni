function specialSortedArray(arr) {
    return arr.sort((a, b) => {
        if (a.length - b.length !== 0) {
            return a.length - b.length
        }
        if (a.toLowerCase() < b.toLowerCase()) {
            return -1;
        }
        if (a.toLowerCase() > b.toLowerCase()) {
            return 1
        }
        return 0;
    }).join('\n')
}