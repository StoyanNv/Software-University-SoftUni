namespace PhotoShare.Client.Core
{
    using System;
    using PhotoShare.Client.Interfaces;
    using System.Linq;
    using System.Reflection;

    public class CommandDispatcher
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
                .SingleOrDefault(t => t.Name == $"{commandName}Command");

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
