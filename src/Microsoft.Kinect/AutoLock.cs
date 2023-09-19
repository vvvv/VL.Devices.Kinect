// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.AutoLock
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Threading;

namespace Microsoft.Kinect
{
  internal struct AutoLock : IDisposable
  {
    private readonly ReaderWriterLockSlim _lock;
    private AutoLock.AutoLockMode _lockMode;

    public AutoLock(
      ReaderWriterLockSlim readerWriterLock,
      AutoLock.AutoLockMode initialAutoLockMode)
    {
      this._lock = readerWriterLock;
      this._lockMode = initialAutoLockMode;
      switch (this._lockMode)
      {
        case AutoLock.AutoLockMode.Read:
          this._lock.EnterReadLock();
          break;
        case AutoLock.AutoLockMode.UpgradeableRead:
          this._lock.EnterUpgradeableReadLock();
          break;
        case AutoLock.AutoLockMode.Write:
          this._lock.EnterWriteLock();
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof (initialAutoLockMode));
      }
    }

    public AutoLock UpgradeToWrite()
    {
      if (this._lockMode != AutoLock.AutoLockMode.UpgradeableRead)
        throw new InvalidOperationException();
      return new AutoLock(this._lock, AutoLock.AutoLockMode.Write);
    }

    public void Dispose()
    {
      switch (this._lockMode)
      {
        case AutoLock.AutoLockMode.None:
          this._lockMode = AutoLock.AutoLockMode.None;
          break;
        case AutoLock.AutoLockMode.Read:
          this._lock.ExitReadLock();
          goto case AutoLock.AutoLockMode.None;
        case AutoLock.AutoLockMode.UpgradeableRead:
          this._lock.ExitUpgradeableReadLock();
          goto case AutoLock.AutoLockMode.None;
        case AutoLock.AutoLockMode.Write:
          this._lock.ExitWriteLock();
          goto case AutoLock.AutoLockMode.None;
        default:
          throw new InvalidOperationException();
      }
    }

    internal enum AutoLockMode
    {
      None,
      Read,
      UpgradeableRead,
      Write,
    }
  }
}
