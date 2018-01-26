function speedCheck(params) {
    function getLimit(zone) {
        switch (zone) {
            case'motorway':
                return 130;
            case'interstate':
                return 90;
            case'city':
                return 50;
            case'residential':
                return 20;
        }
    }

    function getInfraction(speed, limit) {
        let overSpeed = speed - limit;
        if (overSpeed <= 0) {
            return false
        }
        else {
            if (overSpeed <= 20) {
                return 'speeding';
            }
            if (overSpeed > 20 && overSpeed <= 40) {
                return 'excessive speeding';
            }
            if (overSpeed > 40) {
                return 'reckless driving';
            }
        }
    }

    let speedLimit = getLimit(params[1]);
    let infraction = getInfraction(params[0], speedLimit);
    if (infraction) {
        return infraction
    }
}