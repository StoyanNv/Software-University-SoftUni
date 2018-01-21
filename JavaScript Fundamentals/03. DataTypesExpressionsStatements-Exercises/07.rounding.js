function round(params) {
    let number = params[0];
    let precision = params[1];
    var factor = Math.pow(10, precision);
    return Math.round(number * factor) / factor;
}