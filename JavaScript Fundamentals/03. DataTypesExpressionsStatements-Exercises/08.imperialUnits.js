function imperialUnits(num) {
    let feet = Math.floor(num / 12);
    let inches = num % 12;

    return `${feet}'-${inches}"`;
}
console.log(imperialUnits(36));