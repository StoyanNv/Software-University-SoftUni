function smallestTwoNumbers(nums) {
    console.log(nums.sort((a, b) => a - b).slice(0, 2))
}