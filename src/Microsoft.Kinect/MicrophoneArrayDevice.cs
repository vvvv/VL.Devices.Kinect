// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.MicrophoneArrayDevice
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
  internal struct MicrophoneArrayDevice
  {
    private const int MaxStrLen = 512;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
    public string DeviceName;
    [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 512)]
    public string DeviceID;
    [MarshalAs(UnmanagedType.I4)]
    public int DeviceIndex;
  }
}
