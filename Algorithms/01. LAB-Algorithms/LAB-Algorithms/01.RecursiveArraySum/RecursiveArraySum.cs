using System;
using System.Linq;

namespace ConsoleApp1
{
    class RecursiveArraySum
    {
        private static int Sum(int[] array, int index)
        {
            if (array.Length == index)
            {
                return 0;
            }
            var currentSum = array[index] + Sum(array, index + 1);

            return currentSum;
        }
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            if (input != null)
            {
                var array = input
                    .Split(' ')
                    .Select(int.Parse)
                    .ToArray();

                var result = Sum(array, 0);

                Console.WriteLine(result);
            }
        }
    }
}
