using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TeamBuilder.App.Utilities;

namespace TeamBuilder.App.Core
{
    class CommandParser
    {
        public string DispatchCommand(string[] commandParameters)
        {
            var commandName = commandParameters[0];
            var commandArgs = commandParameters.Skip(1).ToArray();

            var assembly = Assembly.GetExecutingAssembly();

            var commandTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Contains(typeof(ICommand)))
                .ToArray();

            var commandType = commandTypes
                .SingleOrDefault(t => t.Name == $"{commandName}");

            if (commandType == null)
            {
                throw new InvalidOperationException($"Command {commandName} not valid!");
            }


            var constructor = commandType.GetConstructors().First();

            var constructorParameters = constructor
                .GetParameters()
                .Select(pi => pi.ParameterType)
                .ToArray();

            var command = (ICommand)constructor.Invoke(constructorParameters);

            return command.Execute(commandArgs);
        }
    }
}
