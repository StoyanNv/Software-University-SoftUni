function evenPositionElements(elements) {
    var result = [];
    for (let i = 0; i < elements.length; i++) {
        if (i % 2 === 0) {
            result.push(elements[i])
        }
    }
    return result.join(' ');
}