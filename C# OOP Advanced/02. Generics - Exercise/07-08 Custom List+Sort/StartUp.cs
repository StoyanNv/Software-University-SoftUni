using System;
public class StartUp
{
    public static void Main()
    {
        var input = Console.ReadLine();
        var list = new CustomList<string>();
        while (input != "END")
        {
            var comands = input.Split(' ');
            if (comands[0] == "Add")
            {
                list.Add(comands[1]);
            }
            else if (comands[0] == "Remove")
            {
                list.Remove(int.Parse(comands[1]));
            }
            else if (comands[0] == "Contains")
            {
                if (list.Contains(comands[1]))
                {
                    Console.WriteLine("True");
                }
                else
                {
                    Console.WriteLine("False");
                }
            }
            else if (comands[0] == "Swap")
            {
                list.Swap(int.Parse(comands[1]), int.Parse(comands[2]));
            }
            else if (comands[0] == "Greater")
            {
                Console.WriteLine(list.CountGreaterThan(comands[1]));
            }
            else if (comands[0] == "Max")
            {
                Console.WriteLine(list.Max());
            }
            else if (comands[0] == "Min")
            {
                Console.WriteLine(list.Min());
            }
            else if (comands[0] == "Print")
            {
                foreach (var item in list.Print)
                {
                    Console.WriteLine(item);
                }
            }
            else if (comands[0] == "Sort")
            {
                list = Sorter.Sort(list);
            }
            input = Console.ReadLine();
        }
    }
}