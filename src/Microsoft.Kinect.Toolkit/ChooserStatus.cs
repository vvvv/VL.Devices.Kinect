// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.ChooserStatus
// Assembly: Microsoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B38B75B-8556-4BDB-9D68-753B7C4809E2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.dll

using System;

namespace Microsoft.Kinect.Toolkit
{
  [Flags]
  public enum ChooserStatus
  {
    None = 0,
    SensorInitializing = 1,
    SensorStarted = 2,
    NoAvailableSensors = 16, // 0x00000010
    SensorConflict = 32, // 0x00000020
    SensorNotPowered = 64, // 0x00000040
    SensorInsufficientBandwidth = 128, // 0x00000080
    SensorNotGenuine = 256, // 0x00000100
    SensorNotSupported = 512, // 0x00000200
    SensorError = 1024, // 0x00000400
  }
}
