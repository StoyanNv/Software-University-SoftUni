using System;

namespace _03.RecursiveDrawing
{
    class RecursiveDrawing
    {
        static void Draw(int size)
        {
            if (size <= 0)
            {
                return;
            }

            Console.WriteLine(new string('*', size));
            Draw(size - 1);
            Console.WriteLine(new string('#', size));

        }

        static void Main(string[] args)
        {
            var input = int.Parse(Console.ReadLine());
            Draw(input);
        }
    }
}
