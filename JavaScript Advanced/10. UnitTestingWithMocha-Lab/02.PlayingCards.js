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