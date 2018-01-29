function increasingSequence(input) {
    input.unshift(input[0]);
    return input.filter((e, i) => input[i] >= input[i-1]).join('\n')
}