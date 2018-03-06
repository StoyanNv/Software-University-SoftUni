function check(obj) {
    if (obj['handsShaking'] === false) {
        return obj;
    }
    else {
        let alcoholLevelRequired = (obj['weight'] * obj['experience']) / 10;
        obj['bloodAlcoholLevel'] = obj['bloodAlcoholLevel'] + alcoholLevelRequired;
        obj['handsShaking'] = false;
        return obj;
    }
}