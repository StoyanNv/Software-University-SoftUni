function sumLastKNumbersSequence(n, k) {
    let seq = [1];
    for (let i = 1; i < n; i++) {
        seq[i] = seq.slice(Math.max(0, seq.length - k), seq.length).reduce((a, b) => {
            return a + b
        }, 0);
    }
    console.log(seq)
}
sumLastKNumbersSequence(6,3);