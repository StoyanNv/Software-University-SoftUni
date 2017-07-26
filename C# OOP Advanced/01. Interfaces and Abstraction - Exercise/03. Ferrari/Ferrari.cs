public class Ferrari : ICar
{
    public Ferrari(string driver)
    {
        Driver = driver;
    }
    public string Driver { get; }
    public string UseBrakes()
    {
        return "Brakes!";
    }

    public string PushTheGasPedal()
    {
        return "Zadu6avam sA!";
    }
}