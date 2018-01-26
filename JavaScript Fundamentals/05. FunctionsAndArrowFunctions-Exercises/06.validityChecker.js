function validateDistance(params) {
    let first = params[0];
    let second = params[1];
    let third = params[2];
    let fourth = params[3];

    console.log(`{${first}, ${second}} to {0, 0} is ${isDistanceInteger(first, second) ? 'valid' : 'invalid'}`);

    console.log(`{${third}, ${fourth}} to {0, 0} is ${isDistanceInteger(third, fourth) ? 'valid' : 'invalid'}`);

    console.log(`{${first}, ${second}} to {${third}, ${fourth}} is ${isDistanceInteger(first, second, third, fourth) ? 'valid' : 'invalid'}`);

    function isDistanceInteger(x1, y1, x2 = 0, y2 = 0) {
        let distance = Math.sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        return distance % 1 === 0;
    }
}