public class Smartphone : IPhone
{
    public string call(string number)
    {
        foreach (var charr in number)
        {
            if (!char.IsDigit(charr))
            {
                return "Invalid number!";
            }
        }
        return $"Calling... {number}";
    }

    public string browse(string site)
    {
        foreach (var charr in site)
        {
            if (char.IsDigit(charr))
            {
                return "Invalid URL!";
            }
        }
        return $"Browsing: {site}!";
    }
}