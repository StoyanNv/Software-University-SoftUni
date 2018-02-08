function JSONToHTML(arr) {
    let sanitizeInput = str => str
        .replace(/&/g, '&amp;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;');
    let res = "<table>\n";
    let JSONArr = JSON.parse(arr);
    let keys = Object.keys(JSONArr[0]);
    let firstRow = '   <tr>';
    for (let key of keys) {
        firstRow += `<th>${sanitizeInput(key)}</th>`
    }
    res += firstRow +'</tr>' + '\n';
    for (let obj of JSONArr) {
        let nextRow = '   <tr>';
        for (let i = 0; i < keys.length; i++) {
            nextRow += `<td>${sanitizeInput(obj[keys[i]] + '')}</td>`;
        }
        res += nextRow + '</tr>'+'\n'
    }
    res += '</table>';
    console.log(res);
}