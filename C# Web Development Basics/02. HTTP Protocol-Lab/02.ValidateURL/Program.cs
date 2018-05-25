using System;
using System.Net;
using System.Text.RegularExpressions;

namespace _12.ValidateURL
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var regex = @"^(https|http):\/\/([a-zA-Z0-9\-\.]+)(?::(443|80))?(\/[a-zA-Z0-9\/%_\=\&\-\+\.]*)*(\?[0-9a-zA-Z_\=\&\-\+\.%]+)?(#[0-9a-zA-Z_\=\&\-\+\.]+)?$";
            var regMatcher = new Regex(regex);
            var validation = regMatcher.Match(input);
            if (validation.Success)
            {
                var groups = regMatcher.Matches(input);
                foreach (Match match in groups)
                {
                    var protocol = match.Groups[1].Value;
                    var host = match.Groups[2].Value;
                    var port = match.Groups[3].Value;
                    var path = match.Groups[4].Value;
                    var query = match.Groups[5].Value;
                    var fragment = match.Groups[6].Value;
                    if (protocol == "http" && string.IsNullOrEmpty(port))
                    {
                        port = "80";
                    }
                    else if (protocol == "http" && port != "80")
                    {
                        Console.WriteLine("Invalid URL");
                        return;
                    }
                    if (protocol == "https" && string.IsNullOrEmpty(port))
                    {
                        port = "443";
                    }
                    else if (protocol == "https" && port != "443")
                    {
                        Console.WriteLine("Invalid URL");
                        return;
                    }
                    if (string.IsNullOrEmpty(path))
                    {
                        path = "/";
                    }
                    Console.WriteLine($"Protocol: {protocol}");
                    Console.WriteLine($"Host: {host}");
                    Console.WriteLine($"Port: {port}");
                    Console.WriteLine($"Path: {path}");
                    if (!string.IsNullOrEmpty(query))
                    {
                        Console.WriteLine($"Query: {query.Substring(1, query.Length - 1)}");
                    }
                    if (!string.IsNullOrEmpty(fragment))
                    {
                        Console.WriteLine($"Fragment: {fragment.Substring(1, fragment.Length - 1)}");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid URL");
            }
        }
    }
}