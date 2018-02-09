function JSONsTable(arr) {
    let sanitizeInput = str => str
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');
    let HTML = '<table>\n';
    for (let strObj of arr) {
        let obj = JSON.parse(strObj);

        HTML += '\t<tr>\n' + `\t\t<td>${sanitizeInput(obj['name'] + '')}</td>\n`
            + `\t\t<td>${sanitizeInput(obj['position'] + '')}</td>\n`
            + `\t\t<td>${sanitizeInput(obj['salary'] + '')}</td>\n`
            + '\t<tr>\n';
    }
    HTML+='</table>';
    return HTML
}