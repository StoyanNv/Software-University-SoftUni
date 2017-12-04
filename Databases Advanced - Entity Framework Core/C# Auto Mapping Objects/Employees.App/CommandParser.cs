namespace Employees.App
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Employees.App.Commands;
    class CommandParser
    {
        public string DispatchCommand(string[] commandParameters)
        {
            var commandName = commandParameters[0];
            var commandArgs = commandParameters.Skip(1).ToArray();

            var assembly = Assembly.GetExecutingAssembly();

            var typs = assembly
                .GetTypes()
                .Where(t => t.GetInterfaces()
                .Contains(typeof(ICommand)))
                .ToArray();

            var commandType = typs.SingleOrDefault(c => c.Name == commandName + "Command");

            if (commandType == null)
            {
                throw new InvalidOperationException($"Command {commandName} not valid!");
            }
            var constructor = commandType.GetConstructors().First();

            var constructorParameters = constructor.GetParameters().Select(pi => pi.ParameterType).ToArray();

            var command = (ICommand)constructor.Invoke(constructorParameters);

            return command.Execute(commandArgs);
        }
    }
}