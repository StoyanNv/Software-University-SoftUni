function printArrayWithGivenDelimiter(input) {
    let delimeter = input[input.length-1];
    input.pop();
    let res = input[0];
    for (let i = 1; i < input.length; i++) {
        res+=delimeter+input[i];
    }
    return res;
}