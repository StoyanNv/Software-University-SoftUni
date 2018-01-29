function buildArray(params) {
    let arr = [];
    for (let i = 0; i < params.length; i++) {
        if (params[i] === 'add') {
            arr.push(i + 1)
        }
        else {
            arr.pop()
        }
    }
    return arr.length === 0
        ? 'Empty'
        : arr.join('\n');
}