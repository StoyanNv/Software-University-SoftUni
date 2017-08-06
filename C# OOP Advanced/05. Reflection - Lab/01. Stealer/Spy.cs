using System;
using System.CodeDom;
using System.Linq;
using System.Reflection;
using System.Text;

public class Spy
{
    public string StealFieldInfo(string className, params string[] fieldsNames)
    {
        var sb = new StringBuilder();
        var targetType = Type.GetType(className);

        sb.AppendLine($"Class under investigation: {targetType}");

        var classInstance = Activator.CreateInstance(targetType);

        var fields = targetType.GetFields(BindingFlags.Instance | BindingFlags.Static |
                                          BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var field in fields.Where(f => fieldsNames.Contains(f.Name)))
        {
            sb.AppendLine($"{field.Name} = {field.GetValue(classInstance)}");
        }

        return sb.ToString().Trim();
    }

    public string AnalyzeAcessModifiers(string className)
    {
        var type = Type.GetType(className);
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
        var getPublicProperties = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
        var getPrivateProperties = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
        var sb = new StringBuilder();
        foreach (var field in fields)
        {
            sb.AppendLine($"{field.Name} must be private!");
        }
        foreach (var getter in getPublicProperties.Where(n => n.Name.StartsWith("get")))
        {
            sb.AppendLine($"{getter.Name} have to be public!");
        }
        foreach (var setter in getPrivateProperties.Where(n => n.Name.StartsWith("set")))
        {
            sb.AppendLine($"{setter.Name} have to be private!");
        }
        return $"{sb.ToString().Trim()}";
    }
    public string RevealPrivateMethods(string className)
    {
        var type = Type.GetType(className);
        var sb = new StringBuilder();
        sb.AppendLine($"All Private Methods of Class: {className}");
        sb.AppendLine($"Base Class: {type.BaseType.Name}");
        var methods = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance);
        foreach (var method in methods)
        {
            sb.AppendLine(method.Name);
        }
        return sb.ToString().Trim();
    }
    public string CollectGettersAndSetters(string className)
    {
        var targetType = Type.GetType(className);
        var methods = targetType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var sb = new StringBuilder();

        foreach (var method in methods.Where(m => m.Name.StartsWith("get")))
        {
            sb.AppendLine($"{method.Name} will return {method.ReturnType}");
        }

        foreach (var method in methods.Where(m => m.Name.StartsWith("set")))
        {
            sb.AppendLine($"{method.Name} will set field of {method.GetParameters().First().ParameterType}");
        }

        return sb.ToString().Trim();
    }
}