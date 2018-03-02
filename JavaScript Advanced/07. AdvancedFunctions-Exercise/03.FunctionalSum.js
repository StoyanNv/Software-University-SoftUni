function add(number) {
    let sum = 0;

    let res = (function additem(num) {
        sum += num;
        return additem
    }(number));

    res.toString = () => sum.toString();
    return res

};
console.log(add(1).toString());
