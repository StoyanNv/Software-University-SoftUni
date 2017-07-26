public class Pet : IExistable, IBreathable
{
    public Pet(string name, string year)
    {
        this.Name = name;
        this.Year = year;
    }
    public string Name { get; }
    public string Year { get; }
}