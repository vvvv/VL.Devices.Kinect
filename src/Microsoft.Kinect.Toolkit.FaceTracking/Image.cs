// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.Image
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  internal class Image : IDisposable
  {
    private Image.BufferManagement bufferManagement;
    private bool disposed;
    private IFTImage faceTrackingImagePtr;

    public Image()
    {
      this.faceTrackingImagePtr = NativeMethods.FTCreateImage();
      if (this.faceTrackingImagePtr == null)
        throw new InvalidOperationException("Cannot create image instance");
    }

    ~Image() => this.Dispose(false);

    public IntPtr BufferPtr
    {
      get
      {
        this.CheckPtrAndThrow();
        return this.faceTrackingImagePtr.GetBuffer();
      }
    }

    public uint BufferSize
    {
      get
      {
        this.CheckPtrAndThrow();
        return this.faceTrackingImagePtr.GetBufferSize();
      }
    }

    internal IFTImage ImagePtr => this.faceTrackingImagePtr;

    public static uint FormatToSize(FaceTrackingImageFormat format)
    {
      switch (format)
      {
        case FaceTrackingImageFormat.FTIMAGEFORMAT_INVALID:
          return 0;
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_GR8:
          return 1;
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_R8G8B8:
          return 3;
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_X8R8G8B8:
          return 4;
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_A8R8G8B8:
          return 4;
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_B8G8R8X8:
          return 4;
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_B8G8R8A8:
          return 4;
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT16_D16:
          return 2;
        case FaceTrackingImageFormat.FTIMAGEFORMAT_UINT16_D13P3:
          return 2;
        default:
          throw new ArgumentException("Invalid image format specified");
      }
    }

    public void Allocate(uint width, uint height, FaceTrackingImageFormat format)
    {
      this.CheckPtrAndThrow();
      this.bufferManagement = this.bufferManagement == Image.BufferManagement.None ? Image.BufferManagement.LocalNativeImage : throw new InvalidOperationException("Cannot Allocate again. Image already allocated buffer in native image.");
      this.faceTrackingImagePtr.Allocate(width, height, format);
    }

    public void Attach(
      uint width,
      uint height,
      IntPtr imageDataPtr,
      FaceTrackingImageFormat format,
      uint stride)
    {
      this.CheckPtrAndThrow();
      this.bufferManagement = this.bufferManagement == Image.BufferManagement.None ? Image.BufferManagement.External : throw new InvalidOperationException("Cannot Attach again. Image already attached to external buffer.");
      this.faceTrackingImagePtr.Attach(width, height, imageDataPtr, format, stride);
    }

    public void CopyFrom<T>(T[] srcData) where T : struct
    {
      if (this.bufferManagement == Image.BufferManagement.External)
        throw new InvalidOperationException("Cannot copy data as buffer managed externally.");
      if (this.bufferManagement != Image.BufferManagement.LocalNativeImage)
        throw new NotSupportedException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Unsupported buffer management {0} encountered", (object) this.bufferManagement));
      IntPtr bufferPtr = this.BufferPtr;
      uint bufferSize = this.BufferSize;
      if ((long) (srcData.Length * Marshal.SizeOf(typeof (T))) > (long) bufferSize)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Array size for src buffer ({0}) should be less than native image buffer ({1})", (object) srcData.Length, (object) bufferSize));
      if (typeof (T).Equals(typeof (byte)))
      {
        byte[] source = srcData as byte[];
        Marshal.Copy(source, 0, bufferPtr, source.Length);
      }
      else
      {
        if (!typeof (T).Equals(typeof (short)))
          throw new InvalidOperationException("Invalid type of data specified. Only byte & short supported");
        short[] source = srcData as short[];
        Marshal.Copy(source, 0, bufferPtr, source.Length);
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
        return;
      if (this.faceTrackingImagePtr != null)
      {
        Marshal.FinalReleaseComObject((object) this.faceTrackingImagePtr);
        this.faceTrackingImagePtr = (IFTImage) null;
      }
      this.disposed = true;
    }

    private void CheckPtrAndThrow()
    {
      if (this.faceTrackingImagePtr == null)
        throw new InvalidOperationException("Native image pointer in invalid state.");
    }

    private enum BufferManagement
    {
      None,
      LocalNativeImage,
      External,
    }
  }
}
