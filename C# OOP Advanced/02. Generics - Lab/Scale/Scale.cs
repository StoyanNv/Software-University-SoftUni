using System;
using System.Collections.Generic;

class Scale<T>
    where T : IComparable<T>
{
    private T first;
    private T second;

    public Scale(T first, T second)
    {
        this.first = first;
        this.second = second;
    }
  
    public T GetHavier()
    {
        if (first.CompareTo(second) > 0)
        {
            return this.first;
        }
        if (first.CompareTo(second) < 0)
        {
            return this.second;
        }
        return default(T);
    }
}