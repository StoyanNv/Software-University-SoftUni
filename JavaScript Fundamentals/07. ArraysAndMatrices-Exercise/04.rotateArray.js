function rotateArray(input) {
    let n = Number(input.pop()) % input.length;
    for (let i = 0; i < n; i++) {
        input.unshift(input.pop());
    }

    console.log(input.join(' '));
}