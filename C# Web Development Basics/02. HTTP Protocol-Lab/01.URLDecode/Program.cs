using System;
using System.Net;

namespace _01.URLDecode
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var decodet = WebUtility.UrlDecode(input);
            Console.WriteLine(decodet);
        }
    }
}
