// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.ThreadSafeList`1
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Kinect
{
  internal class ThreadSafeList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
  {
    private readonly object _lock;
    private readonly List<T> _list = new List<T>();

    public ThreadSafeList()
      : this(new object())
    {
    }

    public ThreadSafeList(object critSec) => this._lock = critSec;

    public void AddRange(IEnumerable<T> collection)
    {
      lock (this._lock)
        this._list.AddRange(collection);
    }

    public int IndexOf(T item)
    {
      lock (this._lock)
        return this._list.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
      lock (this._lock)
        this._list.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
      lock (this._lock)
        this._list.RemoveAt(index);
    }

    public T this[int index]
    {
      get
      {
        lock (this._lock)
          return this._list[index];
      }
      set
      {
        lock (this._lock)
          this._list[index] = value;
      }
    }

    public void Add(T item)
    {
      lock (this._lock)
        this._list.Add(item);
    }

    public void Clear()
    {
      lock (this._lock)
        this._list.Clear();
    }

    public bool Contains(T item)
    {
      lock (this._lock)
        return this._list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      lock (this._lock)
        this._list.CopyTo(array, arrayIndex);
    }

    public void CopyTo(T[] array)
    {
      lock (this._lock)
        this._list.CopyTo(array);
    }

    public int Count
    {
      get
      {
        lock (this._lock)
          return this._list.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        lock (this._lock)
          return false;
      }
    }

    public bool Remove(T item)
    {
      lock (this._lock)
        return this._list.Remove(item);
    }

    private IEnumerator<T> NewEnumerator() => (IEnumerator<T>) new ThreadSafeList<T>.ThreadSafeEnumerator(this);

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      lock (this._lock)
        return this.NewEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      lock (this._lock)
        return (IEnumerator) this.NewEnumerator();
    }

    private class ThreadSafeEnumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      private readonly ThreadSafeList<T> _list;
      private readonly IEnumerator<T> _enum;

      public ThreadSafeEnumerator(ThreadSafeList<T> list)
      {
        lock (list._lock)
        {
          this._list = new ThreadSafeList<T>();
          this._list.AddRange((IEnumerable<T>) list);
          this._enum = (IEnumerator<T>) this._list._list.GetEnumerator();
        }
      }

      public void Dispose()
      {
        lock (this._list._lock)
          this._enum.Dispose();
      }

      public bool MoveNext()
      {
        lock (this._list._lock)
          return this._enum.MoveNext();
      }

      public void Reset()
      {
        lock (this._list._lock)
          this._enum.Reset();
      }

      public T Current
      {
        get
        {
          lock (this._list._lock)
            return this._enum.Current;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          lock (this._list._lock)
            return (object) this._enum.Current;
        }
      }
    }
  }
}
