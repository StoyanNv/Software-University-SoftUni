using System;

public class StartUp
{
    public static void Main()
    {
        var input = Console.ReadLine();
        var stack = new Stack<string>();
        while (input != "END")
        {
            if (input.Contains("Push"))
            {
                var items = input.Split(new[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 1; i < items.Length; i++)
                {
                    stack.Push(items[i]);
                }
            }
            else
            {
                try
                {
                    stack.Pop();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            input = Console.ReadLine();
        }
        for (int i = 0; i < 2; i++)
        {
            foreach (var element in stack)
            {
                Console.WriteLine(element);
            }
        }
    }
}