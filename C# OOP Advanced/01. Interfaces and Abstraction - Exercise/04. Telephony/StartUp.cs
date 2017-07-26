using System;
public class StartUp
{
    public static void Main()
    {
        var numbers = Console.ReadLine().Split();
        var sites = Console.ReadLine().Split();
        var phone = new Smartphone();
        foreach (var number in numbers)
        {
            Console.WriteLine(phone.call(number));
        }
        foreach (var site in sites)
        {
            Console.WriteLine(phone.browse(site));
        }
    }
}