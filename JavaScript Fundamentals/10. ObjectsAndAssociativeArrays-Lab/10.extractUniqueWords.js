function printUniqueWords(params) {
    console.log(Array.from(new Set(params.join(' ').toLowerCase().match(/\w+/g))).join(', '));
}