using System;
using System.Collections.Generic;
using System.Linq;

public class StartUp
{
    public static void Main()
    {
        var playerOne = Console.ReadLine();
        var playerTwo = Console.ReadLine();
        var cardsOne = new List<Card>();
        var cardTwo = new List<Card>();

        for (int i = 0; i < 5; i++)
        {
            var card = Console.ReadLine().Split(new string[] { " of " }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                var currCard = new Card(card[0], card[1]);
                if (!cardsOne.Select(x=>x.ToString()).Contains(currCard.ToString()) && !cardTwo.Select(x => x.ToString()).Contains(currCard.ToString()))
                {
                    cardsOne.Add(currCard);
                }
                else
                {
                    Console.WriteLine("Card is not in the deck.");
                    i--;
                }

            }
            catch
            {
                Console.WriteLine("No such card exists.");
                i--;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            var card = Console.ReadLine().Split(new string[] { " of " }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                var currCard = new Card(card[0], card[1]);
                if (!cardsOne.Select(x => x.ToString()).Contains(currCard.ToString()) && !cardTwo.Select(x => x.ToString()).Contains(currCard.ToString()))
                {
                    cardTwo.Add(currCard);
                }
                else
                {
                    Console.WriteLine("Card is not in the deck.");
                    i--;
                }
            }
            catch
            {
                Console.WriteLine("No such card exists.");
                i--;
            }
        }
        var powerOne = cardsOne.OrderByDescending(x => x.Power()).FirstOrDefault();
        var powerTwo = cardTwo.OrderByDescending(x => x.Power()).FirstOrDefault();
        if (powerOne.Power() > powerTwo.Power())
        {
            Console.WriteLine($"{playerOne} wins with {powerOne}.");
        }
        else if (powerTwo.Power() > powerOne.Power())
        {
            Console.WriteLine($"{playerTwo} wins with {powerTwo}.");
        }
    }
}