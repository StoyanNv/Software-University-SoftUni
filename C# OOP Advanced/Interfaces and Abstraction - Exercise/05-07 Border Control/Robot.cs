public class Robot : IExistable,IRegistrable
{
    public Robot(string name, string id)
    {
        this.Name = name;
        this.ID = id;
    }
    public string Name { get; }
    public string ID { get; }
}