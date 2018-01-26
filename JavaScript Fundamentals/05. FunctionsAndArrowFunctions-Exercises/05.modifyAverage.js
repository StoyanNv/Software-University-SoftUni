function modifyAverage(number) {
    let stringNum = number.toString();
    if (checkAvg(stringNum) > 5) {
        console.log(stringNum)
    }
    else {
        stringNum += 9;
        modifyAverage(stringNum)
    }

    function checkAvg(stringNumber) {
        let sum = 0;
        let count = 0;
        for (let num of stringNumber) {
            let currNum = Number(num);
            sum += currNum;
            count++;
        }
        return sum / count
    }
}