function carFactory(car) {
    let wheels = [];
    let engine = {};
    let wheelSize = car['wheelsize'];
    if (wheelSize % 2 === 0) {
        wheelSize--;
    }
    for (let i = 0; i < 4; i++) {
        wheels.push(wheelSize);
    }
    if (car.power <= 90) {
        engine = {
            power: 90,
            volume: 1800
        }
    } else if (car.power <= 120) {
        engine = {
            power: 120,
            volume: 2400
        }
    } else {
        engine = {
            power: 200,
            volume: 3500
        }
    }


    return {
        model: car['model'],
        engine: engine,
        carriage: {
            type: car['carriage'],
            color: car['color']
        },
        wheels: wheels
    };
}