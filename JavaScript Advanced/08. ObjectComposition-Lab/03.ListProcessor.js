function listProcessor(arr) {
    let res = [];
    let obj = (function () {
        return {
            add: function (element) {
                res.push(element)
            },
            remove: function (element) {
                res = res.filter(x => x !== element);
            },
            print: function () {
                console.log(res.join(','))
            }
        }
    }());

    for (let i = 0; i < arr.length; i++) {
        let cmd = arr[i].split(' ')[0];
        let element = arr[i].split(' ')[1];
        obj[cmd](element)
    }
}