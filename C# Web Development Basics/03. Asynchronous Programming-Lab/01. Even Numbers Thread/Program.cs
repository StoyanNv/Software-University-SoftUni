using System;
using System.Linq;
using System.Threading;

namespace _01._Even_Numbers_Thread
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            Thread evenNums = new Thread(() => { PrintEvenNumbers(input[0], input[1]); });
            evenNums.Start();
            evenNums.Join();
            Console.WriteLine("Thread finished work");
        }

        static void PrintEvenNumbers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
