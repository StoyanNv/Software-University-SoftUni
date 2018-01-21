function composeImg(params) {
    let path = params[0];
    let alt = params[1];

    return `<img src="${path}" alt="${alt}">`;
} 