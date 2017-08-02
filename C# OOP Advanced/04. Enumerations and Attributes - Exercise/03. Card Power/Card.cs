using System;

public class Card : IComparable<Card>
{
    private CardRanks ranks;
    private CardSuits suits;
    public Card(string rank, string suit)
    {
        this.ranks = (CardRanks)Enum.Parse(typeof(CardRanks), rank);
        this.suits = (CardSuits)Enum.Parse(typeof(CardSuits), suit);
    }

    public int Power()
    {
        return (int)ranks + (int)suits;
    }

    public override string ToString()
    {
        return $"{ranks} of {suits}";
    }

    public int CompareTo(Card other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        var ranksComparison = ranks.CompareTo(other.ranks);
        if (ranksComparison != 0) return ranksComparison;
        return suits.CompareTo(other.suits);
    }
}