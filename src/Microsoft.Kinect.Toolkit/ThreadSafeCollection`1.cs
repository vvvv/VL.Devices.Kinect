// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.ThreadSafeCollection`1
// Assembly: Microsoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B38B75B-8556-4BDB-9D68-753B7C4809E2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.dll

using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Kinect.Toolkit
{
  public sealed class ThreadSafeCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
  {
    private readonly object lockObject;
    private readonly List<T> list = new List<T>();

    public ThreadSafeCollection()
      : this(new object())
    {
    }

    public ThreadSafeCollection(object existingLock) => this.lockObject = existingLock;

    public int Count
    {
      get
      {
        lock (this.lockObject)
          return this.list.Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        lock (this.lockObject)
          return false;
      }
    }

    public T this[int index]
    {
      get
      {
        lock (this.lockObject)
          return this.list[index];
      }
      set
      {
        lock (this.lockObject)
          this.list[index] = value;
      }
    }

    public void Add(T item)
    {
      lock (this.lockObject)
        this.list.Add(item);
    }

    public void Clear()
    {
      lock (this.lockObject)
        this.list.Clear();
    }

    public bool Contains(T item)
    {
      lock (this.lockObject)
        return this.list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      lock (this.lockObject)
        this.list.CopyTo(array, arrayIndex);
    }

    public void CopyTo(T[] array)
    {
      lock (this.lockObject)
        this.list.CopyTo(array);
    }

    public void AddRange(IEnumerable<T> collection)
    {
      lock (this.lockObject)
        this.list.AddRange(collection);
    }

    public int IndexOf(T item)
    {
      lock (this.lockObject)
        return this.list.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
      lock (this.lockObject)
        this.list.Insert(index, item);
    }

    public void RemoveAt(int index)
    {
      lock (this.lockObject)
        this.list.RemoveAt(index);
    }

    public bool Remove(T item)
    {
      lock (this.lockObject)
        return this.list.Remove(item);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      lock (this.lockObject)
        return this.NewEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      lock (this.lockObject)
        return (IEnumerator) this.NewEnumerator();
    }

    private IEnumerator<T> NewEnumerator() => (IEnumerator<T>) new ThreadSafeCollection<T>.ThreadSafeEnumerator(this);

    private class ThreadSafeEnumerator : IEnumerator<T>, IDisposable, IEnumerator
    {
      private readonly ThreadSafeCollection<T> collection;
      private readonly IEnumerator<T> enumerator;

      public ThreadSafeEnumerator(ThreadSafeCollection<T> collection)
      {
        lock (collection.lockObject)
        {
          this.collection = new ThreadSafeCollection<T>();
          this.collection.AddRange((IEnumerable<T>) collection);
          this.enumerator = (IEnumerator<T>) this.collection.list.GetEnumerator();
        }
      }

      public T Current
      {
        get
        {
          lock (this.collection.lockObject)
            return this.enumerator.Current;
        }
      }

      object IEnumerator.Current
      {
        get
        {
          lock (this.collection.lockObject)
            return (object) this.enumerator.Current;
        }
      }

      public void Dispose()
      {
        lock (this.collection.lockObject)
          this.enumerator.Dispose();
      }

      public bool MoveNext()
      {
        lock (this.collection.lockObject)
          return this.enumerator.MoveNext();
      }

      public void Reset()
      {
        lock (this.collection.lockObject)
          this.enumerator.Reset();
      }
    }
  }
}
