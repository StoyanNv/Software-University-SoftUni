function solve(arr) {
    let closure = (function () {
        let str = '';
        return {
            append: (s) => str += s,
            removeStart: (n) => str = str.substring(n),
            removeEnd: (n) => str = str.substring(0, str.length - n),
            print: () => console.log(str)
        }
    })();
    for (let st of arr) {
        let command = st.split(' ')[0];
        let value = st.split(' ')[1];
        closure[command](value)
    }
}
solve(['append hello',
    'append again',
    'removeStart 3',
    'removeEnd 4',
    'print'])