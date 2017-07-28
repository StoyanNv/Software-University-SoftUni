public class Tuples<T, U, V>
{
    public Tuples(T item1, U item2, V item3)
    {
        this.Item1 = item1;
        this.Item2 = item2;
        this.Item3 = item3;
    }
    public T Item1 { get; }
    public U Item2 { get; }
    public V Item3 { get; }

    public override string ToString()
    {
        return $"{Item1} -> {Item2} -> {Item3}";
    }
}