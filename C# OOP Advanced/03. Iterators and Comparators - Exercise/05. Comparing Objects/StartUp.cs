using System;
using System.Collections.Generic;
public class StartUp
{
    public static void Main()
    {
        var input = Console.ReadLine();
        var peoples = new List<Person>();
        while (input != "END")
        {
            var args = input.Split(' ');
            var person = new Person(args[0], int.Parse(args[1]), args[2]);
            peoples.Add(person);
            input = Console.ReadLine();
        }
        var index = int.Parse(Console.ReadLine());
        var equal = 0;
        var notEqual = 0;
        var total = peoples.Count;
        for (int i = 0; i < peoples.Count; i++)
        {
            if (i != index)
            {
                if (index > peoples.Count - 1)
                {
                    break;
                }
                if (peoples[index].CompareTo(peoples[i]) == 0)
                {
                    if (equal == 0)
                    {
                        equal += 2;
                    }
                    else
                    {
                        equal++;
                    }
                }
                else
                {
                    notEqual++;
                }
            }
        }
        if (equal == 0)
        {
            Console.WriteLine("No matches");
        }
        else
        {
            Console.Write(equal + " ");
            Console.Write(notEqual + " ");
            Console.WriteLine(total);
        }
    }
}