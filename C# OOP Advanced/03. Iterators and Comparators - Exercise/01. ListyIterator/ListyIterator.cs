using System;
using System.Collections;
using System.Collections.Generic;
public class ListyIterator<T> : IEnumerable<T>
{
    private readonly List<T> data;
    private int currentInternalIndex;

    public ListyIterator(IEnumerable<T> collectionData)
    {
        this.data = new List<T>(collectionData);
    }

    public bool Move()
    {
        if (this.currentInternalIndex < this.data.Count - 1)
        {
            this.currentInternalIndex++;
        }
        else
        {
            return false;
        }

        return true;
    }

    public bool HasNext() => this.currentInternalIndex < this.data.Count - 1;

    public T Print()
    {
        if (this.data.Count == 0)
        {
            throw new ArgumentException("Invalid Operation!");
        }

        return this.data[this.currentInternalIndex];
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this.data.Count; i++)
        {
            yield return this.data[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}