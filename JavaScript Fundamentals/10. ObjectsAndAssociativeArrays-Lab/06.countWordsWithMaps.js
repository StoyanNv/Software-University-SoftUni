function countWords(inputLines) {
    let words = inputLines.join('\n').toLowerCase().split(/[^A-Za-z0-9_]+/).filter(w => w !== '');

    let wordsCount = new Map();
    for (let i = 0; i < words.length; i++) {
        if (wordsCount.has(words[i])) {
            wordsCount.set(words[i], wordsCount.get(words[i]) + 1);
        }
        else {
            wordsCount.set(words[i], 1);
        }
    }
    let allWords = Array.from(wordsCount.keys()).sort();
    allWords.forEach(w => console.log(`'${w}' -> ${wordsCount.get(w)} times`));
}

countWords(['Far too slow, you\'re far too slow.']);