using System;
using System.Collections.Generic;
using System.Linq;
public class StartUp
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        IList<Box<double>> listOfBoxes = new List<Box<double>>();
        for (int i = 0; i < n; i++)
        {
            var input = double.Parse(Console.ReadLine());
            var box = new Box<double>(input);
            listOfBoxes.Add(box);
        }
        var element = double.Parse(Console.ReadLine());
        Console.WriteLine(GetGreaterElementsCount(listOfBoxes, element));
    }

    public static int GetGreaterElementsCount<T>(IList<Box<T>> listOfBoxes, T element)
        where T : IComparable
    {
        return listOfBoxes.Count(x => x.Input.CompareTo(element) > 0);
    }
}