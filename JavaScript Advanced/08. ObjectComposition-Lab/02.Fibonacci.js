function getFibonator() {
    let firstNum = 0;
    let secNum = 1;
    return function () {
        let next = firstNum + secNum;
        firstNum = secNum;
        secNum = next;
        return firstNum
    }
}