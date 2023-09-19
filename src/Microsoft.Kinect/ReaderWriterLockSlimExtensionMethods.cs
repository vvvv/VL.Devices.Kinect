// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.ReaderWriterLockSlimExtensionMethods
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Threading;

namespace Microsoft.Kinect
{
  internal static class ReaderWriterLockSlimExtensionMethods
  {
    public static AutoLock EnterReadLockAuto(this ReaderWriterLockSlim readerWriterLock) => new AutoLock(readerWriterLock, AutoLock.AutoLockMode.Read);

    public static AutoLock EnterUpgradeableReadLockAuto(this ReaderWriterLockSlim readerWriterLock) => new AutoLock(readerWriterLock, AutoLock.AutoLockMode.UpgradeableRead);

    public static AutoLock EnterWriteLockAuto(this ReaderWriterLockSlim readerWriterLock) => new AutoLock(readerWriterLock, AutoLock.AutoLockMode.Write);
  }
}
