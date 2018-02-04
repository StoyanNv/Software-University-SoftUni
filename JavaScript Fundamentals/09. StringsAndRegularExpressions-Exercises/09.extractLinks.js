function extractLinks(arr) {
    let pattern = /w{3}\.[A-Za-z0-9\-]+(\.[a-z]+)+/g;
    let links = [];

    for (let i = 0; i < arr.length; i++) {
        let match = pattern.exec(arr[i]);
        while (match) {
            links.push(match[0]);
            match = pattern.exec(arr[i]);
        }
    }
    return links.join('\n');
}