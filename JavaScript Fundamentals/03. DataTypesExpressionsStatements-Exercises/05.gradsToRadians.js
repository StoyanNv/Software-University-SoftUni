function getDegrees(grads) {
    let degrees = (grads * 0.90) % 360;

    return degrees >= 0 ? degrees : 360 + degrees;
}