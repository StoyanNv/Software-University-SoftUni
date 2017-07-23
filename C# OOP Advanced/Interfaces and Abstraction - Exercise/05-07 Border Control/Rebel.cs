public class Rebel : IReble, IBuyer
{
    public Rebel(string name, string age, string group)
    {
        this.Name = name;
        this.Age = age;
        this.Group = group;
        this.Food = 0;
    }
    public string Name { get; }
    public string Age { get; }
    public string Group { get; }
    public void BuyFood()
    {
        Food += 5;
    }
    public int Food { get; set; }
}