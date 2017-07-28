using System;
using System.Collections.Generic;
using System.Linq;

public class CustomList<T>
    : ICustomList<T> where T : IComparable
{
    private readonly List<T> _unknown;
    private IList<T> elements;
    public CustomList()
    {
        this.elements = new List<T>();
    }

    public CustomList(List<T> list)
    {
        this.elements = list;
    }

    public IList<T> Print
    {
        get => elements;
    }
    public void Add(T element)
    {
        elements.Add(element);
    }

    public bool Contains(T element)
    {
        return elements.Contains(element);
    }

    public int CountGreaterThan(T element)
    {
        return this.elements.Count(x => x.CompareTo(element) > 0);
    }

    public T Max()
    {
        return elements.Max();
    }

    public T Min()
    {
        return elements.Min();
    }

    public T Remove(int index)
    {
        var temp = elements[index];
        elements.RemoveAt(index);
        return temp;
    }

    public void Swap(int index1, int index2)
    {
        var temp = elements[index1];
        elements[index1] = elements[index2];
        elements[index2] = temp;
    }
}