class Request {
    constructor(method, uri, version, message, response, fulfilled) {
        [this.method, this.uri, this.version, this.message, this.response, this.fulfilled]
            = [method, uri, version, message, undefined, false]
    }

}