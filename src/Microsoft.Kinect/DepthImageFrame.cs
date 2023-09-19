// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.DepthImageFrame
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  public sealed class DepthImageFrame : ImageFrame
  {
    public const int PlayerIndexBitmask = 7;
    public const int PlayerIndexBitmaskWidth = 3;
    private readonly DepthImageStream _depthImageStream;
    private DataPool<int, ImageType, ImageResolution, short[], DepthImagePixel[]>.Entry _frameData;
    private readonly object _dataAccessLock = new object();

    private DepthImageFrame(
      DepthImageStream depthImageStream,
      int frameNumber,
      long timestamp,
      ImageFrameFlags frameFlags,
      DataPool<int, ImageType, ImageResolution, short[], DepthImagePixel[]>.Entry pixelData)
      : base(pixelData.Value1, pixelData.Value2, frameNumber, timestamp, frameFlags)
    {
      this._depthImageStream = depthImageStream;
      this._frameData = pixelData;
      this.Format = DepthImageStream.LookUpDepthImageFormat(pixelData.Value1, pixelData.Value2);
    }

    internal static DepthImageFrame Create(
      DepthImageStream depthImageStream,
      int frameNumber,
      long timestamp,
      ImageFrameFlags frameFlags)
    {
      DepthImageFrame depthImageFrame = (DepthImageFrame) null;
      DataPool<int, ImageType, ImageResolution, short[], DepthImagePixel[]>.Entry pixelData = depthImageStream.LockPixelData(frameNumber);
      if (pixelData != null)
        depthImageFrame = new DepthImageFrame(depthImageStream, frameNumber, timestamp, frameFlags, pixelData);
      return depthImageFrame;
    }

    public DepthImageFormat Format { get; private set; }

    public DepthRange Range => !this.FrameFlags.HasFlag((Enum) ImageFrameFlags.NUI_IMAGE_FRAME_FLAG_NEAR_MODE_ENABLED) ? DepthRange.Default : DepthRange.Near;

    public int MaxDepth => this.Range != DepthRange.Near ? 4000 : 3000;

    public int MinDepth => this.Range != DepthRange.Near ? 800 : 400;

    public override int PixelDataLength => DepthImageStream.LookUpPixelDataLength(this.Format);

    public IDepthFilter DepthFilter => this._depthImageStream.Sensor.NuiSensor.GetDepthFilterForTimestamp(this.Timestamp);

    public DepthImagePixel[] GetRawPixelData() => this._frameData.Value4;

    private void CopyPixelDataTo<T>(T[] pixelData, Action<T[], int> copyFunction)
    {
      if (pixelData == null)
        throw new ArgumentNullException(nameof (pixelData));
      if (pixelData.Length != this.PixelDataLength)
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (pixelData));
      this.CopyPixelDataTo<T[]>(pixelData, pixelData.Length, copyFunction);
    }

    private void CopyPixelDataTo<T>(T pixelData, int pixelDataLength, Action<T, int> copyFunction)
    {
      if (pixelDataLength != this.PixelDataLength)
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (pixelDataLength));
      lock (this._dataAccessLock)
      {
        if (this._frameData == null)
          throw new ObjectDisposedException(nameof (DepthImageFrame));
        copyFunction(pixelData, pixelDataLength);
      }
    }

    public unsafe void CopyPixelDataTo(short[] pixelData) => this.CopyPixelDataTo<short>(pixelData, (Action<short[], int>) ((data, length) =>
    {
      fixed (short* source = this._frameData.Value3)
        Marshal.Copy((IntPtr) (void*) source, data, 0, length);
    }));

    public unsafe void CopyDepthImagePixelDataTo(DepthImagePixel[] pixelData) => this.CopyPixelDataTo<DepthImagePixel>(pixelData, (Action<DepthImagePixel[], int>) ((data, length) =>
    {
      fixed (DepthImagePixel* source = this._frameData.Value4)
        fixed (DepthImagePixel* destination = data)
          NativeMethods.CopyMemory((IntPtr) (void*) destination, (IntPtr) (void*) source, length * sizeof (DepthImagePixel));
    }));

    public void CopyPixelDataTo(IntPtr pixelData, int pixelDataLength)
    {
      if (pixelData == IntPtr.Zero)
        throw new ArgumentNullException(nameof (pixelData));
      this.CopyPixelDataTo<IntPtr>(pixelData, pixelDataLength, (Action<IntPtr, int>) ((data, length) => Marshal.Copy(this._frameData.Value3, 0, data, length)));
    }

    public unsafe void CopyDepthImagePixelDataTo(IntPtr pixelData, int pixelDataLength)
    {
      if (pixelData == IntPtr.Zero)
        throw new ArgumentNullException(nameof (pixelData));
      this.CopyPixelDataTo<IntPtr>(pixelData, pixelDataLength, (Action<IntPtr, int>) ((data, length) =>
      {
        fixed (DepthImagePixel* source = this._frameData.Value4)
          NativeMethods.CopyMemory(data, (IntPtr) (void*) source, length * sizeof (DepthImagePixel));
      }));
    }

    [Obsolete("This method is replaced by Microsoft.Kinect.CoordinateMapper.MapDepthPointToColorPoint", false)]
    public ColorImagePoint MapToColorImagePoint(
      int depthX,
      int depthY,
      ColorImageFormat colorImageFormat)
    {
      if (colorImageFormat == ColorImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (colorImageFormat));
      short depthPixelValue = 0;
      if (depthX >= 0 && depthX < this.Width && depthY >= 0 && depthY < this.Height)
        depthPixelValue = this._frameData.Value3[depthY * this.Width + depthX];
      return this._depthImageStream.Sensor.MapDepthToColorImagePoint(this.Format, depthX, depthY, depthPixelValue, colorImageFormat);
    }

    [Obsolete("This method is replaced by Microsoft.Kinect.CoordinateMapper.MapSkeletonPointToDepthPoint", false)]
    public DepthImagePoint MapFromSkeletonPoint(SkeletonPoint skeletonPoint) => this._depthImageStream.Sensor.MapSkeletonPointToDepth(skeletonPoint, this.Format);

    [Obsolete("This method is replaced by Microsoft.Kinect.CoordinateMapper.MapDepthPointToSkeletonPoint", false)]
    public SkeletonPoint MapToSkeletonPoint(int depthX, int depthY)
    {
      short depthPixelValue = 0;
      if (depthX >= 0 && depthX < this.Width && depthY >= 0 && depthY < this.Height)
        depthPixelValue = this._frameData.Value3[depthY * this.Width + depthX];
      return this._depthImageStream.Sensor.MapDepthToSkeletonPoint(this.Format, depthX, depthY, depthPixelValue);
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      lock (this._dataAccessLock)
      {
        this._depthImageStream.UnlockPixelData(this._frameData);
        this._frameData = (DataPool<int, ImageType, ImageResolution, short[], DepthImagePixel[]>.Entry) null;
      }
    }

    internal override ImageStream SourceStream => (ImageStream) this._depthImageStream;
  }
}
