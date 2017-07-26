using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

public class StartUp
{
    public static void Main()
    {
        var n = int.Parse(Console.ReadLine());
        var listOfBoxes = new List<Box<int>>();
        for (int i = 0; i < n; i++)
        {
            var input = int.Parse(Console.ReadLine());
            var box = new Box<int>(input);
            listOfBoxes.Add(box);
        }
        var indexes = Console.ReadLine()
            .Split(' ')
            .Select(int.Parse)
            .ToArray();
        SwapElements(listOfBoxes, indexes[0], indexes[1]);
        foreach (var box in listOfBoxes)
        {
            Console.WriteLine(box.ToString());
        }
    }

    public static void SwapElements<T>(List<T> listOfBoxes, int index1, int index2)
    {
        var temp = listOfBoxes[index1];
        listOfBoxes[index1] = listOfBoxes[index2];
        listOfBoxes[index2] = temp;
    }
}