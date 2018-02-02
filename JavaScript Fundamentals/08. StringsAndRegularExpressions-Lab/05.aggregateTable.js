function aggregateTable(lines) {
    let sum = 0, list = [];
    for (let i = 0; i < lines.length; i++) {
        let arr = lines[i].split('|'),
            townName = arr[1].trim(),
            income = Number(arr[2].trim());
        list.push(townName.trim());
        sum += income;
    }
    console.log(list.join(', '));
    console.log(sum);
}