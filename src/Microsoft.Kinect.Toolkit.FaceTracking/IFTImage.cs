// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.IFTImage
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [Guid("1A00A7BC-C217-11E0-AC90-0024811441FD")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IFTImage
  {
    void Allocate(uint width, uint height, FaceTrackingImageFormat format);

    void Attach(
      uint width,
      uint height,
      IntPtr dataPtr,
      FaceTrackingImageFormat format,
      uint stride);

    void Reset();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    uint GetWidth();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    uint GetHeight();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    uint GetStride();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    uint GetBytesPerPixel();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    uint GetBufferSize();

    FaceTrackingImageFormat GetFormat();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    IntPtr GetBuffer();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool IsAttached();

    void CopyTo([In] IFTImage destImage, [In] ref Rect srcRect, uint destRow, uint destColumn);

    void DrawLine(Point startPoint, Point endPoint, uint color, uint lineWidthPx);
  }
}
