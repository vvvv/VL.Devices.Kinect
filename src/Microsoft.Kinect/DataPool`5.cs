// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.DataPool`5
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;

namespace Microsoft.Kinect
{
  internal class DataPool<K, V1, V2, V3, V4>
  {
    private readonly DataPool<K, V1, V2, V3, V4>.EntryPrivate[] _dataPool;
    private int _nextPoolEntry;
    private readonly object _dataPoolLock = new object();

    public DataPool(int length)
    {
      this._dataPool = length >= 0 ? new DataPool<K, V1, V2, V3, V4>.EntryPrivate[length] : throw new ArgumentOutOfRangeException(nameof (length));
      for (int index = 0; index < this._dataPool.Length; ++index)
      {
        this._dataPool[index] = new DataPool<K, V1, V2, V3, V4>.EntryPrivate();
        this._dataPool[index].IsInPool = true;
        this._dataPool[index].IsValid = true;
      }
    }

    public DataPool<K, V1, V2, V3, V4>.Entry CheckOutFreeEntryForUpdate()
    {
      lock (this._dataPoolLock)
      {
        int nextPoolEntry = this._nextPoolEntry;
        this._nextPoolEntry = (this._nextPoolEntry + 1) % this._dataPool.Length;
        DataPool<K, V1, V2, V3, V4>.EntryPrivate entryPrivate = this._dataPool[nextPoolEntry];
        if (!entryPrivate.IsValid || entryPrivate.IsLocked)
        {
          entryPrivate.IsInPool = false;
          entryPrivate = new DataPool<K, V1, V2, V3, V4>.EntryPrivate();
          entryPrivate.IsInPool = true;
          entryPrivate.IsValid = true;
          this._dataPool[nextPoolEntry] = entryPrivate;
        }
        entryPrivate.IsValid = false;
        return (DataPool<K, V1, V2, V3, V4>.Entry) entryPrivate;
      }
    }

    public void CheckInEntryForUpdate(DataPool<K, V1, V2, V3, V4>.Entry entry)
    {
      lock (this._dataPoolLock)
      {
        DataPool<K, V1, V2, V3, V4>.EntryPrivate entryPrivate = (DataPool<K, V1, V2, V3, V4>.EntryPrivate) entry;
        if (!entryPrivate.IsInPool || entryPrivate.IsLocked || entryPrivate.IsValid)
          return;
        entryPrivate.IsValid = true;
      }
    }

    public bool TryLockEntry(K key, out DataPool<K, V1, V2, V3, V4>.Entry entry)
    {
      entry = (DataPool<K, V1, V2, V3, V4>.Entry) null;
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      lock (this._dataPoolLock)
      {
        for (int index = 0; index < this._dataPool.Length; ++index)
        {
          if (this._dataPool[index].IsValid && this._dataPool[index].Key.Equals((object) key))
          {
            DataPool<K, V1, V2, V3, V4>.EntryPrivate entryPrivate = this._dataPool[index];
            entryPrivate.AddRef();
            entry = (DataPool<K, V1, V2, V3, V4>.Entry) entryPrivate;
            break;
          }
        }
      }
      return entry != null;
    }

    public void UnlockEntry(DataPool<K, V1, V2, V3, V4>.Entry entry)
    {
      lock (this._dataPoolLock)
        ((DataPool<K, V1, V2, V3, V4>.EntryPrivate) entry).Release();
    }

    public abstract class Entry
    {
      public K Key { get; set; }

      public V1 Value1 { get; set; }

      public V2 Value2 { get; set; }

      public V3 Value3 { get; set; }

      public V4 Value4 { get; set; }
    }

    private class EntryPrivate : DataPool<K, V1, V2, V3, V4>.Entry
    {
      private int _refCount;

      public void AddRef() => ++this._refCount;

      public void Release() => --this._refCount;

      public bool IsLocked => this._refCount > 0;

      public bool IsInPool { get; set; }

      public bool IsValid { get; set; }
    }
  }
}
