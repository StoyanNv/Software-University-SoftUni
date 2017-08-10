using System.Linq;
using System.Reflection;

namespace _02BlackBoxInteger
{
    using System;

    class BlackBoxIntegerTests
    {
        static void Main(string[] args)
        {
            var type = typeof(BlackBoxInt);
            BlackBoxInt activator = (BlackBoxInt)Activator.CreateInstance(type,true);
            var input = Console.ReadLine();
            while (input != "END")
            {
                var tokens = input.Split('_');
                var methodName = tokens[0];
                var num = int.Parse(tokens[1]);
                type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(activator, new object[] { num });
                var valueState = (int)type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).First().GetValue(activator);
                Console.WriteLine(valueState);
                input = Console.ReadLine();
            }
        }
    }
}