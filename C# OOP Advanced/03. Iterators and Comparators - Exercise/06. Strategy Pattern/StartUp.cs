using System;
using System.Collections.Generic;
public class StartUp
{
    public static void Main()
    {
        var namesSet = new SortedSet<Person>(new NameSort());
        var ageSet = new SortedSet<Person>(new AgeSort());

        var n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            var input = Console.ReadLine().Split(' ');
            var person = new Person(input[0], int.Parse(input[1]));
            namesSet.Add(person);
            ageSet.Add(person);
        }
        foreach (var person in namesSet)
        {
            Console.WriteLine(person);
        }
        foreach (var person in ageSet)
        {
            Console.WriteLine(person);
        }
    }
}