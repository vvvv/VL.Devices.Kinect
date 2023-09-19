// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.KinectChangedEventArgs
// Assembly: Microsoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B38B75B-8556-4BDB-9D68-753B7C4809E2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.dll

using System;

namespace Microsoft.Kinect.Toolkit
{
  public class KinectChangedEventArgs : EventArgs
  {
    public KinectChangedEventArgs(KinectSensor oldSensor, KinectSensor newSensor)
    {
      this.OldSensor = oldSensor;
      this.NewSensor = newSensor;
    }

    public KinectSensor OldSensor { get; private set; }

    public KinectSensor NewSensor { get; private set; }
  }
}
