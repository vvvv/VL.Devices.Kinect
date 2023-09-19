// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.CallbackLock
// Assembly: Microsoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B38B75B-8556-4BDB-9D68-753B7C4809E2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.dll

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Microsoft.Kinect.Toolkit
{
  public sealed class CallbackLock : IDisposable
  {
    private readonly object lockObject;

    public CallbackLock(object lockTarget)
    {
      this.lockObject = lockTarget;
      Monitor.Enter(lockTarget);
    }

    [SuppressMessage("Microsoft.Design", "CA1009:DeclareEventHandlersCorrectly", Justification = "Helper event to defer actions until after lock exit doesn't need arguments")]
    public event LockExitEventHandler LockExit;

    public void Dispose()
    {
      Monitor.Exit(this.lockObject);
      if (this.LockExit == null)
        return;
      this.LockExit();
    }
  }
}
