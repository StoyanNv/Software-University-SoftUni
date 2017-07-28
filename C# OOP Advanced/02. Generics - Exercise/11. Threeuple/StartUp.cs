using System;

public class StartUp
{
    public static void Main()
    {
        var input1 = Console.ReadLine().Split(' ');
        var name = $"{input1[0]} {input1[1]}";
        var address = input1[2];
        var town = input1[3];
        var tuple = new Tuples<string, string, string>(name, address, town);
        Console.WriteLine(tuple);

        var input2 = Console.ReadLine().Split(' ');
        var name2 = input2[0];
        var beer = int.Parse(input2[1]);
        var drunkOrNot = input2[2];
        var state = false;
        if (drunkOrNot == "drunk")
        {
            state = true;
        }
        var tuple2 = new Tuples<string, int, bool>(name2, beer, state);
        Console.WriteLine(tuple2);

        var input3 = Console.ReadLine().Split(' ');
        var name3 = input3[0];
        var value = double.Parse(input3[1]);
        var bank = input3[2];
        var tuple3 = new Tuples<string, double, string>(name3, value, bank);
        Console.WriteLine(tuple3);
    }
}