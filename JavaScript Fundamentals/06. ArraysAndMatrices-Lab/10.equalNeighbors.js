function equalNeighbors(nums) {
    let count = 0;
    for (let i = 0; i < nums.length; i++) {
        for (let j = 0; j < nums[i].length; j++) {
            if (i + 1 < nums.length) {
                if (nums[i][j] === nums[i + 1][j]) {
                    count++;
                }
            }
            if (j + 1 < nums[i].length) {
                if (nums[i][j] === nums[i][j + 1]) {
                    count++;
                }
            }
        }
    }
    return count
}