using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace _03BarracksFactory.Core
{
    using Contracts;

    public class CommandInterpreter : ICommandInterpreter
    {
        private const string CommandSuffix = "Command";

        private IRepository repository;
        private IUnitFactory unitFactory;

        public CommandInterpreter(IRepository repository, IUnitFactory unitFactory)
        {
            this.repository = repository;
            this.unitFactory = unitFactory;
        }
        public IExecutable InterpretCommand(string[] data, string commandName)
        {
            string commandFullName = CultureInfo
                .CurrentCulture
                .TextInfo
                .ToTitleCase(commandName) + CommandSuffix;
            var commandType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .FirstOrDefault(t => t.Name == commandFullName);
            object[] commandParams =
            {
                data,
                this.repository, 
                this.unitFactory

            };
            if (commandType == null)
            {
                throw new InvalidOperationException("Invalid command!");
            }
            return (IExecutable)Activator.CreateInstance(commandType, commandParams);
        }
    }
}