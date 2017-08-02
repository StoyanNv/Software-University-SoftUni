using System;

class StartUp
{
    static void Main()
    {
        var command = Console.ReadLine();

        Console.WriteLine($"{command}:");

        var suits = Enum.GetValues(typeof(CardRanks));
        foreach (var suit in suits)
        {
            Console.WriteLine($"Ordinal value: {(int)suit}; Name value: {suit}");
        }
    }
}