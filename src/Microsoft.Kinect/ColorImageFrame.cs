// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.ColorImageFrame
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  public sealed class ColorImageFrame : ImageFrame
  {
    private readonly ColorImageStream _colorImageStream;
    private DataPool<int, ImageType, ImageResolution, byte[], bool?>.Entry _frameData;
    private readonly object _dataAccessLock = new object();

    private ColorImageFrame(
      ColorImageStream colorImageStream,
      int frameNumber,
      long timestamp,
      ImageFrameFlags frameFlags,
      DataPool<int, ImageType, ImageResolution, byte[], bool?>.Entry pixelData)
      : base(pixelData.Value1, pixelData.Value2, frameNumber, timestamp, frameFlags)
    {
      this._colorImageStream = colorImageStream;
      this._frameData = pixelData;
      this.Format = ColorImageStream.LookUpColorImageFormat(pixelData.Value1, pixelData.Value2);
    }

    internal static ColorImageFrame Create(
      ColorImageStream colorImageStream,
      int frameNumber,
      long timestamp,
      ImageFrameFlags frameFlags)
    {
      ColorImageFrame colorImageFrame = (ColorImageFrame) null;
      DataPool<int, ImageType, ImageResolution, byte[], bool?>.Entry pixelData = colorImageStream.LockPixelData(frameNumber);
      if (pixelData != null)
        colorImageFrame = new ColorImageFrame(colorImageStream, frameNumber, timestamp, frameFlags, pixelData);
      return colorImageFrame;
    }

    public ColorImageFormat Format { get; private set; }

    public override int PixelDataLength => this.Width * this.Height * this.BytesPerPixel;

    public byte[] GetRawPixelData() => this._frameData.Value3;

    public unsafe void CopyPixelDataTo(byte[] pixelData)
    {
      if (pixelData == null)
        throw new ArgumentNullException(nameof (pixelData));
      if (pixelData.Length != this.PixelDataLength)
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (pixelData));
      lock (this._dataAccessLock)
      {
        if (this._frameData == null)
          throw new ObjectDisposedException(nameof (ColorImageFrame));
        fixed (byte* source = this._frameData.Value3)
          Marshal.Copy((IntPtr) (void*) source, pixelData, 0, this.PixelDataLength);
      }
    }

    public void CopyPixelDataTo(IntPtr pixelData, int pixelDataLength)
    {
      if (pixelData == IntPtr.Zero)
        throw new ArgumentNullException(nameof (pixelData));
      if (pixelDataLength != this.PixelDataLength)
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (pixelDataLength));
      lock (this._dataAccessLock)
      {
        if (this._frameData == null)
          throw new ObjectDisposedException(nameof (ColorImageFrame));
        Marshal.Copy(this._frameData.Value3, 0, pixelData, pixelDataLength);
      }
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      lock (this._dataAccessLock)
      {
        this._colorImageStream.UnlockPixelData(this._frameData);
        this._frameData = (DataPool<int, ImageType, ImageResolution, byte[], bool?>.Entry) null;
      }
    }

    internal override ImageStream SourceStream => (ImageStream) this._colorImageStream;
  }
}
