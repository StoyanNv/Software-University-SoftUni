function firstAndLastKNumbers(nums) {
    var k = nums.shift();
    console.log(nums.slice(0, k).join(' '));
    console.log(nums.slice(nums.length - k, nums.length).join(' '));
}