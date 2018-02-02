function matchAllWords(text) {
    let reg = /[A-Za-z]+/g;
    let arr = text.match(reg);
    return arr.join('|')
}