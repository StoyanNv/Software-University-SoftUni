(function () {
        String.prototype.ensureStart = function (str) {
            if (!this.toString().startsWith(str)) {
                return result = str + this.toString();
            }
            return this.toString();
        };
        String.prototype.ensureEnd = function (str) {
            if (!this.toString().endsWith(str)) {
                return result = this.toString() + str;
            }
            return this.toString();
        };
        String.prototype.isEmpty = function () {
            if (this.toString() === '') {
                return true;
            }
            return false;
        };
        String.prototype.truncate = function (n) {
            let currentString = this.toString();

            if (currentString.length <= n) {
                return currentString;
            }

            if (n < 4) {
                return '.'.repeat(n)
            }
            while (currentString.lastIndexOf(' ') !== -1) {
                let lastSpace = currentString.lastIndexOf(' ');
                if (currentString.length >= n - 1) {
                    currentString = currentString.substring(0, lastSpace);
                }
                else {
                    return currentString + '...'
                }
            }
            return this.split(' ')[0].substring(0, n - 3) + '...';
        }
        String.format = function () {
            let string  = arguments[0];
            for (let i = 0; i < arguments.length-1; i++) {
                string = string.replace(`{${i}}`,arguments[i+1])
            }
            return string
        }
    }
)()