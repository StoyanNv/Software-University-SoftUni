function formatAsXml(params) {
    let res = '<?xml version="1.0" encoding="UTF-8"?>\n' +
        '<quiz>\n';
    for (let i = 0; i < params.length; i += 2) {
        let question = params[i];
        let answer = params[i + 1];
        res += '  <question>\n' + `    ${question}\n` + '  </question>\n';
        res += '  <answer>\n' + `    ${answer}\n` + '  </answer>\n';
    }
    res += '</quiz>';
    return res
}