using System;

namespace _02.RecursiveFactorial
{
    class RecursiveFactorial
    {
        static long Factorial(int num)
        {
            if (num == 0)
            {
                return 1;
            }
            return num * Factorial(num - 1);
        }

        static void Main(string[] args)
        {
            var input = int.Parse(Console.ReadLine());
            Factorial(input);
            Console.WriteLine(Factorial(input));
        }
    }
}
