function processOddNumbers(nums) {
    console.log(nums.filter((e, i) => i % 2 !== 0).map(e => e * 2).reverse())
}