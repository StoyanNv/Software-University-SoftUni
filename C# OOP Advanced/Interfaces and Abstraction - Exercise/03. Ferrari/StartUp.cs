using System;

public class StartUp
{
    public static void Main()
    {
        var driver = Console.ReadLine();
        var ferrari = new Ferrari(driver);
        Console.WriteLine($"488-Spider/{ferrari.UseBrakes()}/{ferrari.PushTheGasPedal()}/{ferrari.Driver}");
        string ferrariName = typeof(Ferrari).Name;
        string iCarInterfaceName = typeof(ICar).Name;

        bool isCreated = typeof(ICar).IsInterface;
        if (!isCreated)
        {
            throw new Exception("No interface ICar was created");
        }
    }
}