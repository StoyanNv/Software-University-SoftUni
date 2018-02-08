function scoreToHTML(arr) {
    let parsedJson = JSON.parse(arr);
    let res = '<table>\n' + `    <tr><th>name</th><th>score</th></tr>\n`;
    let sanitizeInput = str => str
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');
    for (let obj of parsedJson) {
        let name = sanitizeInput(obj['name']);
        res += `    <tr><td>${name}</td><td>${Number(obj['score'])}</td></tr>\n`;
    }
    res += '</table>';
    return res
}