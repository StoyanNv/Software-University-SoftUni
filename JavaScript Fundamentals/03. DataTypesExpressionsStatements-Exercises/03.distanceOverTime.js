function getDistanceInMeters([v1, v2, time]) {
    let speed1InKmH = v1 * 1000 / 3600;
    let speed2InKmH = v2 * 1000 / 3600;

    let dist1 = speed1InKmH * time;
    let dist2 = speed2InKmH * time;

    return Math.abs(dist1 - dist2);
}