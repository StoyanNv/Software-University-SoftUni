public class Box<T>
{
    public Box(T input)
    {
        this.Input = input;
    }

    public T Input { get; }
    public override string ToString()
    {
        return Input.GetType().FullName + ": " + Input.ToString();
    }
}