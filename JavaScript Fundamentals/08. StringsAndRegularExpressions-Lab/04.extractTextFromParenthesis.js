function extractTextFromParenthesis(text) {
    let res = [];
    let startIndex = text.indexOf('(');
    while (startIndex > -1) {
        let endIndex = text.indexOf(')', startIndex);
        if (endIndex === -1) {
            break;
        }
        let sub = text.substring(startIndex + 1, endIndex);
        res.push(sub);
        startIndex = text.indexOf('(', endIndex);
    }
    return res.join(', ')
}