// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.CameraConfig
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [StructLayout(LayoutKind.Sequential)]
  internal class CameraConfig
  {
    public const uint MaxResolution = 16384;
    private readonly uint width;
    private readonly uint height;
    private readonly float focalLength;
    private readonly FaceTrackingImageFormat imageFormat;
    private readonly uint bytesPerPixel;
    private readonly uint stride;
    private readonly uint frameBufferLength;

    public CameraConfig(
      uint width,
      uint height,
      float focalLength,
      FaceTrackingImageFormat imageFormat)
    {
      this.width = width;
      this.height = height;
      this.focalLength = focalLength;
      this.imageFormat = imageFormat;
      this.bytesPerPixel = Image.FormatToSize(this.imageFormat);
      this.stride = this.width * this.bytesPerPixel;
      switch (this.imageFormat)
      {
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_GR8:
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_R8G8B8:
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_X8R8G8B8:
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_A8R8G8B8:
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_B8G8R8X8:
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_B8G8R8A8:
          this.frameBufferLength = this.height * this.stride;
          break;
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT16_D16:
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT16_D13P3:
          this.frameBufferLength = this.height * this.width;
          break;
        default:
          throw new ArgumentException("Invalid image format specified");
      }
    }

    public uint Width => this.width;

    public uint Height => this.height;

    public FaceTrackingImageFormat ImageFormat => this.imageFormat;

    public uint Stride => this.stride;

    public uint FrameBufferLength => this.frameBufferLength;
  }
}
