using System;
public class Person : IComparable<Person>
{
    public Person(string name, int age, string town)
    {
        this.Name = name;
        this.Age = age;
        this.Town = town;
    }
    public string Name { get; }
    public int Age { get; }
    public string Town { get; }

    public int CompareTo(Person other)
    {
        var nameCompare = this.Name.CompareTo(other.Name);
        if (nameCompare == 0)
        {
            if (this.Age.CompareTo(other.Age) == 0)
            {
                return this.Town.CompareTo(other.Town);
            }
            return this.Age.CompareTo(other.Age);
        }
        return this.Name.CompareTo(other.Name);
    }
}