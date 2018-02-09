function heroicInventory(arr) {
    let heroes = [];
    for (let hero of arr) {
        let curHeroObj = {};
        let tokens = hero.split(/\s*\/\s*/).filter(e => e !== '');
        let heroName = tokens[0];
        let heroLevel = Number(tokens[1]);
        let heroItems = [];
        if (tokens.length > 2) {
            heroItems = tokens[2].split(/,\s*/).filter(e => e !== '');
        }
        curHeroObj = {name: heroName, level: heroLevel, items: heroItems};
        heroes.push(curHeroObj)
    }
    return JSON.stringify(heroes)
}