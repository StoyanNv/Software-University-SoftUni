function captureTheNumbers(input) {
    let pattern = /[0-9]+/g;
    let res = [];
    for (let i = 0; i < input.length; i++) {
       let match = pattern.exec(input[i]);
       while (match){
           res.push(match);
           match = pattern.exec(input[i]);
       }
    }
    return res.join(' ');
}