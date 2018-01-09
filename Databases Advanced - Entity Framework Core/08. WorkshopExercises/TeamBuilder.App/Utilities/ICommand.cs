using System;
using System.Collections.Generic;
using System.Text;

namespace TeamBuilder.App.Utilities
{
    public interface ICommand
    {
        string Execute(string[] data);
    }
}
