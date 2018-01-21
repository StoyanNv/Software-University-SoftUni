function isPointInside(params) {
    let pX = params[0];
    let pY = params[1];

    let xMin = params[2];
    let xMax = params[3];
    let yMin = params[4];
    let yMax = params[5];

    return pX <= xMax && pX >= xMin && pY <= yMax && pY >= yMin
        ? 'inside'
        : 'outside';
}
