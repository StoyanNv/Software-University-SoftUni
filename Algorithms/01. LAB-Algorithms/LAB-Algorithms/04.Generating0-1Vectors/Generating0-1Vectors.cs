using System;

namespace _04.Generating0_1Vectors
{
    class GeneratingVectors
    {
        static void Generate(int[] vector, int index)
        {
            if (vector.Length == index)
            {
                Console.WriteLine(string.Join("", vector));
            }
            else
            {
                for (int i = 0; i <= 1; i++)
                {
                    vector[index] = i;
                    Generate(vector, index + 1);
                }
            }
        }
        static void Main(string[] args)
        {
            var size = int.Parse(Console.ReadLine());

            var vector = new int[size];

            Generate(vector, 0);
        }
    }
}
