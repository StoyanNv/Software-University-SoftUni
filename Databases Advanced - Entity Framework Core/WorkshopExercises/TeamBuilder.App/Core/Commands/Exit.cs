namespace TeamBuilder.App.Core.Commands
{
    using System;
    using TeamBuilder.App.Utilities;
    class Exit : ICommand
    {
        public string Execute(string[] data)
        {
            Check.CheckLength(0, data);

            Environment.Exit(0);

            return "Bye!";
        }
    }
}
