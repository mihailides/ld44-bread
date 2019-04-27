using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShuffleBag<T> : IList<T>
{
    private readonly List<T> data = new List<T>();
    private int cursor;
    private T last;

    public ShuffleBag() { }
    
    public ShuffleBag(params T[] list)
    {
        AddRange(list);
    }

    public T Next()
    {
        if (cursor < 1)
        {
            cursor = data.Count - 1;
            return data.Count < 1 ? default(T) : data[0];
        }
        var grab = Mathf.FloorToInt(Random.value * (cursor + 1));
        var temp = data[grab];
        data[grab] = data[cursor];
        data[cursor] = temp;
        cursor--;
        return temp;
    }
	
    #region IList[T] implementation
    public int IndexOf(T item)
    {
        return data.IndexOf(item);
    }
	
    public void Insert(int index, T item)
    {
        data.Insert(index, item);
    }
	
    public void RemoveAt(int index)
    {
        data.RemoveAt(index);
    }
	
    public T this[int index]
    {
        get => data [index];
        set => data [index] = value;
    }
    #endregion
	
    #region IEnumerable[T] implementation
    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return data.GetEnumerator();
    }
    #endregion
	
    #region ICollection[T] implementation
    public void Add(T item)
    {
        data.Add(item);
        cursor = data.Count - 1;
    }

    public void AddRange(IEnumerable<T> items)
    {
        data.AddRange(items);
        cursor = data.Count - 1;
    }
    
    public int Count => data.Count;

    public void Clear()
    {
        data.Clear();
    }
	
    public bool Contains(T item)
    {
        return data.Contains(item);
    }
	
    public void CopyTo(T[] array, int arrayIndex)
    {
        foreach (var item in data)
        {
            array.SetValue(item, arrayIndex);
            arrayIndex = arrayIndex + 1;
        }
    }
	
    public bool Remove(T item)
    {
        return data.Remove(item);
    }
	
    public bool IsReadOnly => false;
    #endregion
	
    #region IEnumerable implementation
    IEnumerator IEnumerable.GetEnumerator()
    {
        return data.GetEnumerator();
    }
    #endregion
	
}