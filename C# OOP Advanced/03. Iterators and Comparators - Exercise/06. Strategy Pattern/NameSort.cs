using System.Collections.Generic;
public class NameSort : IComparer<Person>
{
    public int Compare(Person x, Person y)
    {
        var result = x.Name.Length - y.Name.Length;
        if (result == 0)
        {
            var firstXLetter = char.ToLower(x.Name[0]);
            var firstYLetter = char.ToLower(y.Name[0]);
            result = firstXLetter.CompareTo(firstYLetter);
        }
        return result;
    }
}