using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TeamBuilder.App.Core
{
    public class Engine
    {
        public void Run()
        {
            while (true)
            {
                try
                {
                    string input = Console.ReadLine();
                    var parser = new CommandParser();
                    var inputArgs = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                    var parsedCommand = parser.DispatchCommand(inputArgs);
                    Console.WriteLine(parsedCommand);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.GetBaseException().Message);
                }
            }
        }
    }
}
