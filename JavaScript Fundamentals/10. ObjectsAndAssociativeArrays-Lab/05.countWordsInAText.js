function countWords(inputLines) {
    let text = inputLines.join('\n');
    let words = text.split(/[^A-Za-z0-9_]+/)
        .filter(w => w !== '');
    let wordsCount = {};
    for (let i = 0; i < words.length; i++) {
        if (wordsCount.hasOwnProperty(words[i])) {
            wordsCount[words[i]]++;
        }
        else {
            wordsCount[words[i]] = 1;
        }
    }
    return JSON.stringify(wordsCount);
}