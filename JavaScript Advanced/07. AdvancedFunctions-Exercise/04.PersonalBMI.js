function PersonalBMI(name, age, weight, height) {
    let bmi = Math.round(weight / (height / 100) / (height / 100));
    let info = {
        name: name,
        personalInfo: {
            age: age,
            weight: weight,
            height: height
        },
        BMI: bmi,
        status: ''
    };
    if (bmi < 18.5) {
        info.status = 'underweight';
    } else if (bmi < 25) {
        info.status = 'normal';
    } else if (bmi < 30) {
        info.status = 'overweight';
    } else {
        info.status = 'obese';
        info['recommendation'] = 'admission required'
    }
    return info
}

console.log(PersonalBMI('Honey Boo Boo', 9, 57, 137));