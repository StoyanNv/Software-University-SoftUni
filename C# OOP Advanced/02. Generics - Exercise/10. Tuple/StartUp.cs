using System;

public class StartUp
{
    public static void Main()
    {
        var input1 = Console.ReadLine().Split(' ');
        var name = $"{input1[0]} {input1[1]}";
        var address = input1[2];
        var tuple = new Tuples<string, string>(name, address);
        Console.WriteLine(tuple);

        var input2 = Console.ReadLine().Split(' ');
        var name2 = input2[0];
        var beer = int.Parse(input2[1]);
        var tuple2 = new Tuples<string, int>(name2, beer);
        Console.WriteLine(tuple2);

        var input3 = Console.ReadLine().Split(' ');
        var name3 = input3[0];
        var value = double.Parse(input3[1]);
        var tuple3 = new Tuples<string,double>(name3, value);
        Console.WriteLine(tuple3);
    }
}