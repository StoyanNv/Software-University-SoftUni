function printEveryNthElementFromAnArray(input) {
    let n = Number(input[input.length - 1]);
    input.pop();
    let res = [];
    for (let i = 0; i < input.length; i += n) {
        res.push(input[i]);
    }
    return res.join('\n');
}