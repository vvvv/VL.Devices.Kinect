// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.ImageStreamFlags
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;

namespace Microsoft.Kinect
{
  [Flags]
  internal enum ImageStreamFlags : uint
  {
    NUI_IMAGE_STREAM_FLAG_NONE = 0,
    NUI_IMAGE_STREAM_FLAG_SUPPRESS_NO_FRAME_DATA = 65536, // 0x00010000
    NUI_IMAGE_STREAM_FLAG_ENABLE_NEAR_MODE = 131072, // 0x00020000
    NUI_IMAGE_STREAM_FLAG_DISTINCT_OVERFLOW_DEPTH_VALUES = 262144, // 0x00040000
  }
}
