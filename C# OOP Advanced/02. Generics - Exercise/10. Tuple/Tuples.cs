public class Tuples<T, U>
{
    public Tuples(T item1, U item2)
    {
        this.Item1 = item1;
        this.Item2 = item2;
    }
    public T Item1 { get; }
    public U Item2 { get; }
    public override string ToString()
    {
        return $"{Item1} -> {Item2}";
    }
}