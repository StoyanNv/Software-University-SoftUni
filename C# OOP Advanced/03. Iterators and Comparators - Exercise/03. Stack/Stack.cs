using System;
using System.Collections;
using System.Collections.Generic;

public class Stack<T> : IEnumerable<T>
{
    private T[] elements;

    public Stack()
    {
        this.elements = new T[10];
    }

    public int Count { get; set; }

    public void Push(T element)
    {
        if (this.Count == elements.Length)
        {
            Array.Resize(ref this.elements, elements.Length * 2);
        }
        this.elements[Count] = element;
        Count++;
    }

    public T Pop()
    {
        if (Count == 0)
        {
            throw new ArgumentException("No elements");
        }
        var temp = this.elements[elements.Length - 1];
        this.elements[this.Count] = default(T);

        Count--;
        return temp;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = this.Count - 1; i >= 0; i--)
        {
            yield return elements[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}