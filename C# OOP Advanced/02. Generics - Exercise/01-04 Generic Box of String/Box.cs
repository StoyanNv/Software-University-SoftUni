public class Box<T>
{
    private T input;
    public Box(T input)
    {
        this.input = input;
    }

    public override string ToString()
    {
        return input.GetType().FullName + ": " + input.ToString();
    }
}