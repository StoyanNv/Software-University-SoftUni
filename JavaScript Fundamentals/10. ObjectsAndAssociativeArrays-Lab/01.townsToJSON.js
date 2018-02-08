function townsToJSON(arr) {
    let res = [];
    arr = arr.slice(1);
    for (let obj of arr) {
        let tokens = obj.split('|').filter(e => e !== '').map(e => e.trim());
        let town = tokens[0];
        let latitude = tokens[1];
        let longitude = tokens[2];
        let townObj = {
            Town: town, Latitude: Number(latitude), Longitude: Number(longitude)
        };
        res.push(townObj);

    }
    return JSON.stringify(res)
}