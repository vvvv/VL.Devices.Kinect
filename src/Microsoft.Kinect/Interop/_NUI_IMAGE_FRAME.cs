// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Interop._NUI_IMAGE_FRAME
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Interop
{
  [StructLayout(LayoutKind.Sequential, Pack = 8)]
  internal struct _NUI_IMAGE_FRAME
  {
    public _LARGE_INTEGER liTimeStamp;
    public uint dwFrameNumber;
    public _NUI_IMAGE_TYPE eImageType;
    public _NUI_IMAGE_RESOLUTION eResolution;
    [MarshalAs(UnmanagedType.Interface)]
    public INuiFrameTexture pFrameTexture;
    public uint dwFrameFlags;
    public _NUI_IMAGE_VIEW_AREA ViewArea;
  }
}
