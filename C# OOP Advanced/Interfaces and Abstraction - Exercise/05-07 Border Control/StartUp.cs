using System;
using System.Collections.Generic;
using System.Linq;

public class StartUp
{
    public static void Main()
    {
        var entryCount = int.Parse(Console.ReadLine());
        var citizens = new List<Citizen>();
        var rebels = new List<Rebel>();
        for (int i = 0; i < entryCount; i++)
        {
            var buyer = Console.ReadLine().Split(' ');
            if (buyer.Length != 3)
            {
                var citizen = new Citizen(buyer[0], buyer[1], buyer[2], buyer[3]);
                if (!citizens.Select(x => x.Name).Contains(buyer[0]) && !rebels.Select(x => x.Name).Contains(buyer[0]))
                {
                    citizens.Add(citizen);
                }
            }
            else
            {
                var rebel = new Rebel(buyer[0], buyer[1], buyer[2]);
                if (!citizens.Select(x => x.Name).Contains(buyer[0]) && !rebels.Select(x => x.Name).Contains(buyer[0]))
                {
                    rebels.Add(rebel);
                }
            }
        }
        var name = Console.ReadLine();
        while (name != "End")
        {
            foreach (var citizen in citizens)
            {
                if (citizen.Name == name)
                {
                    citizen.BuyFood();
                }
            }
            foreach (var reble in rebels)
            {
                if (reble.Name == name)
                {
                    reble.BuyFood();
                }
            }
            name = Console.ReadLine();
        }
        Console.WriteLine(citizens.Select(x => x.Food).Sum() + rebels.Select(x => x.Food).Sum());
    }
}