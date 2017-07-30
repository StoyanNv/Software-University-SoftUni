using System;
using System.Linq;
public class StartUp
{
    public static void Main()
    {
        var createComand = Console.ReadLine();
        var args = createComand.Split().Skip(1).ToList();
        var list = new ListyIterator<string>(args);

        var comand = Console.ReadLine();
        while (comand != "END")
        {
            try
            {
                if (comand == "Move")
                {
                    Console.WriteLine(list.Move());

                }
                else if (comand == "Print")
                {
                    Console.WriteLine(list.Print());
                }
                else if (comand == "HasNext")
                {
                    Console.WriteLine(list.HasNext());
                }
                else if (comand == "PrintAll")
                {
                    foreach (var item in list)
                    {
                        Console.Write(item + " ");
                    }
                    Console.WriteLine();
                }
                comand = Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                comand = Console.ReadLine();
            }
        }
    }
}