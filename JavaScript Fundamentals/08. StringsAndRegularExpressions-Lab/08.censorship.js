function censor(text, words) {
    for (let i = 0; i < words.length; i++) {
        let rep = '-'.repeat(words[i].length);
        while (text.indexOf(words[i]) > -1) {
            text = text.replace(words[i], rep)
        }
    }
    return text
}