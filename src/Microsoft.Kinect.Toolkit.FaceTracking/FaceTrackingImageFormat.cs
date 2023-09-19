// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.FaceTrackingImageFormat
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System.ComponentModel;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal enum FaceTrackingImageFormat
  {
    FTIMAGEFORMAT_INVALID,
    FTIMAGEFORMAT_UINT8_GR8,
    FTIMAGEFORMAT_UINT8_R8G8B8,
    FTIMAGEFORMAT_UINT8_X8R8G8B8,
    FTIMAGEFORMAT_UINT8_A8R8G8B8,
    FTIMAGEFORMAT_UINT8_B8G8R8X8,
    FTIMAGEFORMAT_UINT8_B8G8R8A8,
    FTIMAGEFORMAT_UINT16_D16,
    FTIMAGEFORMAT_UINT16_D13P3,
  }
}
