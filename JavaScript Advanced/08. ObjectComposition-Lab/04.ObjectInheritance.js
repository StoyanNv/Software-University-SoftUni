function cars(arr) {
    let cmdExec = (function () {
        let obj = {};

        function create(elements) {
            if (elements.length > 2) {
                obj[elements[1]] = Object.create(obj[elements[3]]);
            }
            else {
                obj[elements[1]] = {}
            }
        }

        function set(arr) {
            obj[arr[1]][arr[2]] = arr[3]
        }

        function print(arr) {
            let toPrint = arr[1];
            let res = [];
            for (let key in obj[toPrint]) {
                res.push(key + ':' + obj[toPrint][key])
            }
            console.log(res.join(', '));
        }

        return {create, set, print}
    })();
    for (let i = 0; i < arr.length; i++) {
        let cmd = arr[i].split(' ')[0];
        let elements = arr[i].split(' ');
        cmdExec[cmd](elements)
    }
}