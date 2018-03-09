function validateRequest(obj) {
    const VALID_METHODS = ['GET', 'POST', 'DELETE', 'CONNECT'];
    const VALID_VERSIONS = ['HTTP/0.9', 'HTTP/1.0', 'HTTP/1.1', 'HTTP/2.0'];
    const uriRegex = /^[A-Za-z0-9.]+$|^\*$/g;
    const messageRegex = /^[^<>\\&'"]+$/g;

    if (!obj.hasOwnProperty('method') || !VALID_METHODS.includes(obj['method'])) {
        throw new Error("Invalid request header: Invalid Method");
    }
    if (!obj.hasOwnProperty('uri') || obj['uri'] === '' || !uriRegex.test(obj['uri'])) {
        throw new Error("Invalid request header: Invalid URI");
    }
    if (!obj.hasOwnProperty('version') || !VALID_VERSIONS.includes(obj['version'])) {
        throw new Error("Invalid request header: Invalid Version");
    }
    if (!obj.hasOwnProperty('message')|| !messageRegex.test(obj['message']) && obj['message'] !== '') {
        throw new Error("Invalid request header: Invalid Message");
    }
    return obj
}