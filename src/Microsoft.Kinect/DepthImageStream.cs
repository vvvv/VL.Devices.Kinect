// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.DepthImageStream
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  public sealed class DepthImageStream : ImageStream
  {
    internal const int _DefaultMinDepth = 800;
    internal const int _DefaultMaxDepth = 4000;
    internal const int _NearMinDepth = 400;
    internal const int _NearMaxDepth = 3000;
    private const int _TooNearDepth = 0;
    private const int _TooFarDepth = 4095;
    private const int _UnknownDepth = -1;
    private const int DepthBytesPerPixelConstant = 2;
    private DepthRange _range;
    private int _minDepth;
    private int _maxDepth;
    private DepthImageFormat _depthImageFormat;
    private readonly DataPool<int, ImageType, ImageResolution, short[], DepthImagePixel[]> _depthDataPool;

    private float ScaleValue(float value)
    {
      switch (this.Format)
      {
        case DepthImageFormat.Resolution640x480Fps30:
          return 2f * value;
        case DepthImageFormat.Resolution320x240Fps30:
          return value;
        case DepthImageFormat.Resolution80x60Fps30:
          return 0.25f * value;
        default:
          return value;
      }
    }

    public float NominalFocalLengthInPixels => this.ScaleValue(285.63f);

    public float NominalInverseFocalLengthInPixels => 1f / this.NominalFocalLengthInPixels;

    public float NominalDiagonalFieldOfView => this.ScaleValue(70f);

    public float NominalHorizontalFieldOfView => this.ScaleValue(58.5f);

    public float NominalVerticalFieldOfView => this.ScaleValue(45.6f);

    internal DepthImageStream(KinectSensor mainNui)
      : base(mainNui, ImageStreamFlags.NUI_IMAGE_STREAM_FLAG_SUPPRESS_NO_FRAME_DATA | ImageStreamFlags.NUI_IMAGE_STREAM_FLAG_DISTINCT_OVERFLOW_DEPTH_VALUES)
    {
      this.Range = DepthRange.Default;
      this._depthDataPool = new DataPool<int, ImageType, ImageResolution, short[], DepthImagePixel[]>(this.BufferCount);
      this.Format = DepthImageFormat.Resolution640x480Fps30;
      this.FrameBytesPerPixel = 2;
    }

    public void Enable() => this.Enable(DepthImageFormat.Resolution640x480Fps30);

    public void Enable(DepthImageFormat format)
    {
      if (format == DepthImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (format));
      if (this.Sensor.IsRunning && this.IsEnabled && this.Format == format)
        return;
      this.Format = format;
      this.InternalEnable();
    }

    public DepthRange Range
    {
      get => this._range;
      set
      {
        switch (value)
        {
          case DepthRange.Default:
            this.ImageStreamFlags &= ~ImageStreamFlags.NUI_IMAGE_STREAM_FLAG_ENABLE_NEAR_MODE;
            this._range = value;
            this._minDepth = 800;
            this._maxDepth = 4000;
            break;
          case DepthRange.Near:
            this.ImageStreamFlags |= ImageStreamFlags.NUI_IMAGE_STREAM_FLAG_ENABLE_NEAR_MODE;
            this._range = value;
            this._minDepth = 400;
            this._maxDepth = 3000;
            break;
          default:
            throw new ArgumentOutOfRangeException(Resources.UnexpectedDepthRange);
        }
      }
    }

    public DepthImageFormat Format
    {
      get => this._depthImageFormat;
      private set
      {
        this._depthImageFormat = value;
        this.Resolution = DepthImageStream.LookUpImageResolution(this._depthImageFormat);
        this.FillHeightWidth();
        this.FramePixelDataLength = this.FrameWidth * this.FrameHeight;
      }
    }

    public int TooNearDepth => 0;

    public int TooFarDepth => 4095;

    public int UnknownDepth => -1;

    public int MinDepth => this._minDepth;

    public int MaxDepth => this._maxDepth;

    public DepthImageFrame OpenNextFrame(int millisecondsWait)
    {
      DepthImageFrame depthImageFrame = (DepthImageFrame) null;
      if (!this.Sensor.IsRunning)
        throw new InvalidOperationException(Resources.SensorMustBeRunning);
      if (!this.IsEnabled)
        throw new InvalidOperationException(Resources.DepthStreamMustBeEnabled);
      if (this.Sensor.HasDepthInvocations)
        throw new InvalidOperationException(Resources.CannotPollAndUseEvents);
      KinectEtwProvider.EventWriteManagedOpenNextFrameInfo(1, millisecondsWait);
      int frameNumber;
      long timestamp;
      ImageFrameFlags frameFlags;
      if (this.TryGetNextFrameInternal(millisecondsWait, out frameNumber, out timestamp, out frameFlags))
        depthImageFrame = DepthImageFrame.Create(this, frameNumber, timestamp, frameFlags);
      return depthImageFrame;
    }

    internal override ImageType ImageType => this.Sensor.SkeletonStream.IsEnabled ? ImageType.DepthAndPlayerIndex : ImageType.Depth;

    internal override unsafe void StorePixels(
      int frameNumber,
      _NUI_LOCKED_RECT lockedRect,
      ImageType imageType,
      ImageResolution resolution,
      IntPtr nativeStreamHandle,
      ref _NUI_IMAGE_FRAME pNativeFrame)
    {
      DataPool<int, ImageType, ImageResolution, short[], DepthImagePixel[]>.Entry entry = this._depthDataPool.CheckOutFreeEntryForUpdate();
      entry.Key = frameNumber;
      entry.Value1 = imageType;
      entry.Value2 = resolution;
      int width;
      int height;
      ImageStream.ResolutionToHeightWidth(resolution, out width, out height);
      int length = width * height;
      if (entry.Value3 == null || entry.Value3.Length != length)
        entry.Value3 = new short[length];
      if (entry.Value4 == null || entry.Value4.Length != length)
        entry.Value4 = new DepthImagePixel[length];
      if (length * 2 == lockedRect.size)
        Marshal.Copy(lockedRect.pBits, entry.Value3, 0, entry.Value3.Length);
      bool nearMode;
      INuiFrameTexture ppTexture;
      this.Sensor.NuiSensor.NuiImageFrameGetDepthImagePixelFrameTexture(nativeStreamHandle, ref pNativeFrame, out nearMode, out ppTexture);
      pNativeFrame.dwFrameFlags |= nearMode ? 131072U : 0U;
      if (ppTexture != null)
      {
        _NUI_LOCKED_RECT pLockedRect = new _NUI_LOCKED_RECT();
        tagRECT pRect = new tagRECT();
        KinectExceptionHelper.CheckHr(ppTexture.LockRect(0U, ref pLockedRect, ref pRect, 0U));
        fixed (DepthImagePixel* destination = entry.Value4)
          NativeMethods.CopyMemory((IntPtr) (void*) destination, pLockedRect.pBits, length * sizeof (DepthImagePixel));
      }
      this._depthDataPool.CheckInEntryForUpdate(entry);
    }

    internal DataPool<int, ImageType, ImageResolution, short[], DepthImagePixel[]>.Entry LockPixelData(
      int frameNumber)
    {
      DataPool<int, ImageType, ImageResolution, short[], DepthImagePixel[]>.Entry entry;
      this._depthDataPool.TryLockEntry(frameNumber, out entry);
      return entry;
    }

    internal void UnlockPixelData(
      DataPool<int, ImageType, ImageResolution, short[], DepthImagePixel[]>.Entry entry)
    {
      this._depthDataPool.UnlockEntry(entry);
    }

    internal static ImageResolution LookUpImageResolution(DepthImageFormat format)
    {
      switch (format)
      {
        case DepthImageFormat.Resolution640x480Fps30:
          return ImageResolution.Resolution640x480;
        case DepthImageFormat.Resolution320x240Fps30:
          return ImageResolution.Resolution320x240;
        case DepthImageFormat.Resolution80x60Fps30:
          return ImageResolution.Resolution80x60;
        default:
          return ImageResolution.Invalid;
      }
    }

    internal static DepthImageFormat LookUpDepthImageFormat(
      ImageType imageType,
      ImageResolution imageResolution)
    {
      switch (imageType)
      {
        case ImageType.DepthAndPlayerIndex:
        case ImageType.Depth:
          switch (imageResolution)
          {
            case ImageResolution.Resolution80x60:
              return DepthImageFormat.Resolution80x60Fps30;
            case ImageResolution.Resolution320x240:
              return DepthImageFormat.Resolution320x240Fps30;
            case ImageResolution.Resolution640x480:
              return DepthImageFormat.Resolution640x480Fps30;
          }
          break;
      }
      throw new InvalidOperationException();
    }

    internal static int LookUpPixelDataLength(DepthImageFormat format)
    {
      switch (format)
      {
        case DepthImageFormat.Resolution640x480Fps30:
          return 307200;
        case DepthImageFormat.Resolution320x240Fps30:
          return 76800;
        case DepthImageFormat.Resolution80x60Fps30:
          return 4800;
        default:
          return 0;
      }
    }
  }
}
