using System;
using System.Collections.Generic;
using System.Linq;

namespace _03.RequestParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var paths = new Dictionary<string, string>();
            int counter = 0;
            while (input != "END")
            {
                var tockens = input.Split('/', StringSplitOptions.RemoveEmptyEntries);
                var pathRequest = tockens[0] + counter;
                var methodRequest = tockens[1] + counter;
                paths.Add(pathRequest, methodRequest);
                counter++;
                input = Console.ReadLine();
            }

            var request = Console.ReadLine().Split("/", StringSplitOptions.RemoveEmptyEntries);
            var method = request[0].ToLower().Trim();
            var path = request[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)[0].ToLower();
            var found = false;
            foreach (var kvp in paths)
            {
                var key = kvp.Key.Substring(0, kvp.Key.Length - 1);
                var value = kvp.Value.Substring(0, kvp.Value.Length - 1);
             
                if (kvp.Key.Substring(0, kvp.Key.Length - 1) == path && kvp.Value.Substring(0, kvp.Value.Length - 1) == method)
                {
                    Console.WriteLine("HTTP/1.1 200 OK\r\nContent-Length: 2\r\nContent-Type: text/plain\r\n\r\nOK");
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("HTTP/1.1 404 NotFound\r\nContent-Length: 9\r\nContent-Type: text/plain\r\n\r\nNotFound");
            }
        }
    }
}
