function calculate(firstOperand, secondOperand, operator) {
    let sum = (firstOperand, secondOperand) => firstOperand + secondOperand;
    let subtract = (firstOperand, secondOperand) => firstOperand - secondOperand;
    let multiply = (firstOperand, secondOperand) => firstOperand * secondOperand;
    let divide = (firstOperand, secondOperand) => firstOperand / secondOperand;

    switch (operator) {
        case '+': return sum(firstOperand, secondOperand, operator);
        case '-': return subtract(firstOperand, secondOperand, operator);
        case '*': return multiply(firstOperand, secondOperand, operator);
        case '/': return divide(firstOperand, secondOperand, operator);
    }
}