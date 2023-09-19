// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Interop._NUI_SKELETON_DATA
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Interop
{
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  internal struct _NUI_SKELETON_DATA
  {
    public _NUI_SKELETON_TRACKING_STATE eTrackingState;
    public uint dwTrackingID;
    public uint dwEnrollmentIndex;
    public uint dwUserIndex;
    public _Vector4 Position;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public _Vector4[] SkeletonPositions;
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
    public _NUI_SKELETON_POSITION_TRACKING_STATE[] eSkeletonPositionTrackingState;
    public uint dwQualityFlags;
  }
}
