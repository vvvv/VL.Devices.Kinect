// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.StatusChangedEventArgs
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;

namespace Microsoft.Kinect
{
  public sealed class StatusChangedEventArgs : EventArgs
  {
    internal StatusChangedEventArgs()
    {
    }

    public KinectStatus Status { get; internal set; }

    public KinectSensor Sensor { get; internal set; }
  }
}
