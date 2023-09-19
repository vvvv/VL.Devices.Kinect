// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Interop._NUI_SKELETON_FRAME
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Interop
{
  [StructLayout(LayoutKind.Sequential, Pack = 8)]
  internal struct _NUI_SKELETON_FRAME
  {
    public _LARGE_INTEGER liTimeStamp;
    public uint dwFrameNumber;
    public uint dwFlags;
    public _Vector4 vFloorClipPlane;
    public _Vector4 vNormalToGravity;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
    public _NUI_SKELETON_DATA[] SkeletonData;
  }
}
