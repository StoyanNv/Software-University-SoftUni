function countOccurances(str, key) {
    let matches = str.match(new RegExp(`\\b${key}\\b`, 'gi'));
    if (matches === null) {
        return 0;
    }
    else {
        return matches.length;
    }
}
