function diagonalSums(nums) {
    let mainSum = 0, secondarySum = 0;
    for (let row = 0; row < nums.length; row++) {
        mainSum += nums[row][row];
        secondarySum += nums[row][nums.length - row - 1];
    }
    console.log(mainSum + ' ' + secondarySum);
}