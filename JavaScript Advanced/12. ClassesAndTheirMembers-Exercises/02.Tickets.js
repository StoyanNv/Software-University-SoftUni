function ticketManager(arr, sortStyle) {
    let res = [];

    class Ticket {
        constructor(destination, price, status) {
            [this.destination, this.price, this.status] = [destination, price, status]
        }

        static sorter(ticket1, ticket2, sortMethod) {
            if (sortMethod === 'destination') {
                if (ticket1.destination < ticket2.destination) return -1;
                if (ticket1.destination > ticket2.destination) return 1;
                return 0;
            } else if (sortMethod === 'price') {
                if (ticket1.price < ticket2.price) return -1;
                if (ticket1.price > ticket2.price) return 1;
                return 0;
            } else if (sortMethod === 'status') {
                if (ticket1.status < ticket2.status) return -1;
                if (ticket1.status > ticket2.status) return 1;
                return 0;
            }
        }
    }

    for (let i = 0; i < arr.length; i++) {
        let currTicket = new Ticket(arr[i].split('|')[0], Number(arr[i].split('|')[1]), arr[i].split('|')[2])
        res.push(currTicket)
    }
    res.sort((a,b)=>Ticket.sorter(a,b,sortStyle));
    return res
}

console.log(ticketManager(['Philadelphia|94.20|available',
        'New York City|95.99|available',
        'New York City|95.99|sold',
        'Boston|126.20|departed'],
    'destination'
));