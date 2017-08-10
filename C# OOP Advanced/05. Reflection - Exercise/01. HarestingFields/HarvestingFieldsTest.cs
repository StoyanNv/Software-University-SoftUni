using System.CodeDom;
using System.Reflection;
using System.Security.Permissions;

namespace _01HarestingFields
{
    using System;

    class HarvestingFieldsTest
    {
        static void Main()
        {
            var input = Console.ReadLine();
            while (input != "HARVEST")
            {
                var type = typeof(HarvestingFields);
                if (input == "private")
                {
                    var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    foreach (var field in fields)
                    {
                        if (field.IsPrivate)
                        {
                            Console.WriteLine($"private {field.FieldType.Name} {field.Name}");
                        }
                    }
                }
                else if (input == "protected")
                {
                    var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                    foreach (var field in fields)
                    {
                        if (field.IsFamily)
                        {
                            Console.WriteLine($"protected {field.FieldType.Name} {field.Name}");
                        }
                    }
                }
                else if (input == "public")
                {
                    var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
                    foreach (var field in fields)
                    {
                        Console.WriteLine($"public {field.FieldType.Name} {field.Name}");
                    }
                }
                else if (input == "all")
                {
                    var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    foreach (var field in fields)
                    {
                        if (field.IsFamily)
                        {
                            Console.WriteLine($"protected {field.FieldType.Name} {field.Name}");
                        }
                        else if (field.IsPrivate)
                        {
                            Console.WriteLine($"private {field.FieldType.Name} {field.Name}");
                        }
                        else
                        {
                            Console.WriteLine($"public {field.FieldType.Name} {field.Name}");
                        }
                    }
                }
                input = Console.ReadLine();
            }
        }
    }
}