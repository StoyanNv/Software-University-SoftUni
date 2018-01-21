function composeObj(params) {
    let obj = {};
    if(params.length <= 1) {
        return obj;
    }
    for(i = 0; i < params.length - 1; i++) {
        obj[params[i++]] = params[i];
    }
    return obj;
}