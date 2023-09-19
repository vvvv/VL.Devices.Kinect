// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.NuiDepthFilter
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;

namespace Microsoft.Kinect
{
  public class NuiDepthFilter : IDepthFilter
  {
    public object NativeObject { get; set; }

    public unsafe bool ProcessFrame(
      long timestamp,
      int width,
      int height,
      DepthImagePixel[] pixelData)
    {
      int num1 = 0;
      INuiDepthFilter nativeObject = (INuiDepthFilter) this.NativeObject;
      if (nativeObject != null)
      {
        fixed (DepthImagePixel* depthImagePixelPtr = pixelData)
        {
          IntPtr num2 = (IntPtr) (void*) depthImagePixelPtr;
          INuiDepthFilter nuiDepthFilter = nativeObject;
          _LARGE_INTEGER liTimeStamp = new _LARGE_INTEGER()
          {
            QuadPart = timestamp
          };
          int Width = width;
          int Height = height;
          IntPtr pDepthImagePixels = num2;
          ref int local = ref num1;
          KinectExceptionHelper.CheckHr(nuiDepthFilter.ProcessFrame(liTimeStamp, (uint) Width, (uint) Height, pDepthImagePixels, out local));
        }
      }
      return num1 != 0;
    }
  }
}
