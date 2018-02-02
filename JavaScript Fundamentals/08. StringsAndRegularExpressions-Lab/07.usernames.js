function extractUsernames(inputEmails) {
    let results = [];
    for (let email of inputEmails) {
        let user = email.split('@')[0];
        let domain = email.split('@')[1].split('.').map(x => x[0]);
        results.push(user + '.' + domain.join(''))
    }
    return results.join(', ')
}