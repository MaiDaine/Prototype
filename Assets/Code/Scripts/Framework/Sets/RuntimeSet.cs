using System.Collections.Generic;
using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    public List<T> items = new List<T>();

    public void Add(T element)
    {
        if (!items.Contains(element))
            items.Add(element);
    }

    public void Remove(T element)
    {
        if (items.Contains(element))
            items.Remove(element);
    }
}