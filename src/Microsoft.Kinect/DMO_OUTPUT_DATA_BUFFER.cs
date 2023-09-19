// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.DMO_OUTPUT_DATA_BUFFER
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  [ComVisible(true)]
  internal struct DMO_OUTPUT_DATA_BUFFER
  {
    public IntPtr Buffer;
    public uint Status;
    public long Timestamp;
    public long Timelength;
  }
}
