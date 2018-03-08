function printDeckOfCards(cards) {
    let res = [];

    function makeCard(card, suit) {
        const FACE = ['2', '3', '4', '5', '6', '7', '8', '9', '10', 'J', 'Q', 'K', 'A'];
        let SUIT = {
            'S': '\u2660',
            'H': '\u2665',
            'D': '\u2666',
            'C': '\u2663'
        };
        if (FACE.indexOf(card) < 0 || !SUIT.hasOwnProperty(suit)) {
            throw new Error('Error')
        }
        return {
            card, suit,
            toString: function () {
                return card + SUIT[suit]
            }
        }
    }

    for (let i = 0; i < cards.length; i++) {
        let card = cards[i].substring(0, cards[i].length - 1);
        let suit = cards[i].substr(cards[i].length - 1, 1);
        try {
            res.push(makeCard(card, suit) + '');
        } catch(ex) {
            console.log('Invalid card: ' + cards[i]);
            return;
        }
    }
    console.log(res.join(' '))
}