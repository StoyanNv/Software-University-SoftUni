class Task {
    constructor(title, deadline) {
        this.title = title;
        this.deadline = deadline;
        this.status = 'Open';
    }

    get deadline() {
        return this._deadline
    }

    set deadline(newDeadLine) {
        if (newDeadLine < Date.now()) {
            throw new Error('');
          }
        this._deadline = newDeadLine
    }

    get isOverdue() {
        return this._deadline < Date.now();
    }

    rank() {
        if (this.isOverdue && this.status !== 'Complete') {
            return 0;
        }
        else if (this.status === 'In Progress') {
            return 1;
        }
        else if (this.status === 'Open') {
            return 2;
        }
        else {
            return 3;
        }

    }

    static comparator(a, b) {
        let status = {
            'In Progress': 1,
            'Open': 2,
            'Complete': 3,
        };
        let criteria = a.rank() -b.rank();
        if (criteria === 0) {
            return a._deadline - b._deadline
        }
        return criteria
    }

    toString() {
        const statusIcons = {
            'Open': '\u2731',
            'In Progress': '\u219D',
            'Complete': '\u2714',
            'Overdue': '\u26A0'
        };
        if (this.status !== 'Complete' && this.isOverdue) {
            return `[\u26A0] ${this.title} (overdue)`
        }
        if (this.isOverdue) {
            return `[\u2714] ${this.title}`
        }

        return `[${statusIcons[this.status]}] ${this.title} (deadline: ${this.deadline})`
    }
}
