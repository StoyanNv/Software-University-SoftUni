function nums(n) {
    let res = "<ul>\n";
    for (let i = 1; i <= n; i++) {
        let collor = i % 2 === 0 ? "blue" : "green";
        res += `  <li><span style='color:${collor}'>${i}</span></li>\n`
    }
    res += "</ul>";
    console.log(res)
}
nums(10);