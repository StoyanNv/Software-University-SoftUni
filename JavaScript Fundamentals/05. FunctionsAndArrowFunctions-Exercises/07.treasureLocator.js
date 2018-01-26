function treasureLocator(input) {
    for (let i = 0; i < input.length - 1; i += 2) {
        let x = input[i];
        let y = input[i + 1];
        isInside(x, y)
    }

    function isInside(x, y) {
        if (y >= 0 && y <= 1 && x >= 8 && x <= 9) {
            console.log('Tokelau');
        }
        else if (y >= 1 && y <= 3 && x >= 1 && x <= 3) {
            console.log('Tuvalu')
        }
        else if (y >= 3 && y <= 6 && x >= 5 && x <= 7) {
            console.log('Samoa')
        }
        else if (y >= 6 && y <= 8 && x >= 0 && x <= 2) {
            console.log('Tonga')
        }
        else if (y >= 7 && y <= 8 && x >= 4 && x <= 9) {
            console.log('Cook')
        }
        else {
            console.log('On the bottom of the ocean')
        }
    }
}