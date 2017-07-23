public class Citizen : IRegistrable, IBreathable, IHuman, IBuyer
{
    public Citizen(string name, string age, string id, string birthday)
    {
        this.Name = name;
        this.Age = age;
        this.ID = id;
        this.Year = birthday;
        this.Food = 0;
    }
    public string Year { get; }
    public string Name { get; }
    public string Age { get; }
    public string ID { get; }
    public void BuyFood()
    {
        this.Food += 10;
    }
    public int Food { get; set; }
}