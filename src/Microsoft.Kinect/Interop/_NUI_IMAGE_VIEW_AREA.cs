// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Interop._NUI_IMAGE_VIEW_AREA
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Interop
{
  [StructLayout(LayoutKind.Sequential, Pack = 4)]
  internal struct _NUI_IMAGE_VIEW_AREA
  {
    public int eDigitalZoom;
    public int lCenterX;
    public int lCenterY;
  }
}
