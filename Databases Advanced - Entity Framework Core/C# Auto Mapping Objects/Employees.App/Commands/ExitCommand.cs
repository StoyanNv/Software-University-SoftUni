﻿namespace Employees.App.Commands
{
    using System;

    class ExitCommand : ICommand
    {
        public string Execute(params string[] args)
        {
            Console.WriteLine("Bye");
            Environment.Exit(0);
            return string.Empty;
        }
    }
}
