// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.ColorImageStream
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  public sealed class ColorImageStream : ImageStream
  {
    private ImageType _imageType;
    private ColorImageFormat _colorImageFormat;
    private readonly DataPool<int, ImageType, ImageResolution, byte[]> _colorDataPool;
    private ColorCameraSettings _cameraSettings;

    private float ScaleValue(float value) => this.Format == ColorImageFormat.RgbResolution1280x960Fps12 || this.Format == ColorImageFormat.RawBayerResolution1280x960Fps12 ? 2f * value : value;

    public float NominalFocalLengthInPixels => this.ScaleValue(531.15f);

    public float NominalInverseFocalLengthInPixels => 1f / this.NominalFocalLengthInPixels;

    public float NominalDiagonalFieldOfView => this.ScaleValue(73.9f);

    public float NominalHorizontalFieldOfView => this.ScaleValue(62f);

    public float NominalVerticalFieldOfView => this.ScaleValue(48.6f);

    public ColorCameraSettings CameraSettings
    {
      get
      {
        if (this._cameraSettings == null)
          this._cameraSettings = new ColorCameraSettings(this.Sensor);
        return this._cameraSettings;
      }
    }

    internal ColorImageStream(KinectSensor mainNui)
      : base(mainNui)
    {
      this._colorDataPool = new DataPool<int, ImageType, ImageResolution, byte[]>(this.BufferCount);
      this.Format = ColorImageFormat.RgbResolution640x480Fps30;
    }

    public void Enable() => this.Enable(ColorImageFormat.RgbResolution640x480Fps30);

    public void Enable(ColorImageFormat format)
    {
      if (format == ColorImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (format));
      if (this.Sensor.IsRunning && this.IsEnabled && this.Format == format)
        return;
      this.Format = format;
      this.InternalEnable();
    }

    public ColorImageFormat Format
    {
      get => this._colorImageFormat;
      private set
      {
        this._colorImageFormat = value;
        switch (this._colorImageFormat)
        {
          case ColorImageFormat.RgbResolution640x480Fps30:
          case ColorImageFormat.RgbResolution1280x960Fps12:
            this._imageType = ImageType.Color;
            this.FrameBytesPerPixel = 4;
            break;
          case ColorImageFormat.YuvResolution640x480Fps15:
            this._imageType = ImageType.ColorYuv;
            this.FrameBytesPerPixel = 4;
            break;
          case ColorImageFormat.RawYuvResolution640x480Fps15:
            this._imageType = ImageType.ColorYuvRaw;
            this.FrameBytesPerPixel = 2;
            break;
          case ColorImageFormat.InfraredResolution640x480Fps30:
            this._imageType = ImageType.ColorInfrared;
            this.FrameBytesPerPixel = 2;
            break;
          case ColorImageFormat.RawBayerResolution640x480Fps30:
          case ColorImageFormat.RawBayerResolution1280x960Fps12:
            this._imageType = ImageType.ColorBayer;
            this.FrameBytesPerPixel = 1;
            break;
          default:
            throw new ArgumentException("Invalid ColorImageFormat.", nameof (value));
        }
        this.Resolution = ColorImageStream.LookUpImageResolution(this.Format);
        this.FillHeightWidth();
        this.FramePixelDataLength = this.FrameWidth * this.FrameHeight * this.FrameBytesPerPixel;
      }
    }

    public ColorImageFrame OpenNextFrame(int millisecondsWait)
    {
      ColorImageFrame colorImageFrame = (ColorImageFrame) null;
      if (!this.Sensor.IsRunning)
        throw new InvalidOperationException(Resources.SensorMustBeRunning);
      if (!this.IsEnabled)
        throw new InvalidOperationException(Resources.ColorStreamMustBeEnabled);
      if (this.Sensor.HasColorInvocations)
        throw new InvalidOperationException(Resources.CannotPollAndUseEvents);
      KinectEtwProvider.EventWriteManagedOpenNextFrameInfo(0, millisecondsWait);
      int frameNumber;
      long timestamp;
      ImageFrameFlags frameFlags;
      if (this.TryGetNextFrameInternal(millisecondsWait, out frameNumber, out timestamp, out frameFlags))
        colorImageFrame = ColorImageFrame.Create(this, frameNumber, timestamp, frameFlags);
      return colorImageFrame;
    }

    internal override ImageType ImageType => this._imageType;

    internal override void StorePixels(
      int frameNumber,
      _NUI_LOCKED_RECT lockedRect,
      ImageType imageType,
      ImageResolution resolution,
      IntPtr nativeStreamHandle,
      ref _NUI_IMAGE_FRAME pNativeFrame)
    {
      DataPool<int, ImageType, ImageResolution, byte[], bool?>.Entry entry = this._colorDataPool.CheckOutFreeEntryForUpdate();
      entry.Key = frameNumber;
      entry.Value1 = imageType;
      entry.Value2 = resolution;
      int width;
      int height;
      ImageStream.ResolutionToHeightWidth(resolution, out width, out height);
      int bytesPerPixel = ImageStream.ImageTypeToBytesPerPixel(imageType);
      int length = width * height * bytesPerPixel;
      if (entry.Value3 == null || entry.Value3.Length != length)
        entry.Value3 = new byte[length];
      if (length == lockedRect.size)
        Marshal.Copy(lockedRect.pBits, entry.Value3, 0, entry.Value3.Length);
      this._colorDataPool.CheckInEntryForUpdate(entry);
    }

    internal DataPool<int, ImageType, ImageResolution, byte[], bool?>.Entry LockPixelData(
      int frameNumber)
    {
      DataPool<int, ImageType, ImageResolution, byte[], bool?>.Entry entry;
      this._colorDataPool.TryLockEntry(frameNumber, out entry);
      return entry;
    }

    internal void UnlockPixelData(
      DataPool<int, ImageType, ImageResolution, byte[], bool?>.Entry entry)
    {
      this._colorDataPool.UnlockEntry(entry);
    }

    internal static ImageResolution LookUpImageResolution(ColorImageFormat format)
    {
      switch (format)
      {
        case ColorImageFormat.RgbResolution640x480Fps30:
        case ColorImageFormat.YuvResolution640x480Fps15:
        case ColorImageFormat.RawYuvResolution640x480Fps15:
        case ColorImageFormat.InfraredResolution640x480Fps30:
        case ColorImageFormat.RawBayerResolution640x480Fps30:
          return ImageResolution.Resolution640x480;
        case ColorImageFormat.RgbResolution1280x960Fps12:
        case ColorImageFormat.RawBayerResolution1280x960Fps12:
          return ImageResolution.Resolution1280x960;
        default:
          return ImageResolution.Invalid;
      }
    }

    internal static ColorImageFormat LookUpColorImageFormat(
      ImageType imageType,
      ImageResolution imageResolution)
    {
      switch (imageType)
      {
        case ImageType.Color:
          switch (imageResolution)
          {
            case ImageResolution.Resolution640x480:
              return ColorImageFormat.RgbResolution640x480Fps30;
            case ImageResolution.Resolution1280x960:
              return ColorImageFormat.RgbResolution1280x960Fps12;
          }
          break;
        case ImageType.ColorYuv:
          if (imageResolution == ImageResolution.Resolution640x480)
            return ColorImageFormat.YuvResolution640x480Fps15;
          break;
        case ImageType.ColorYuvRaw:
          if (imageResolution == ImageResolution.Resolution640x480)
            return ColorImageFormat.RawYuvResolution640x480Fps15;
          break;
        case ImageType.ColorInfrared:
          if (imageResolution == ImageResolution.Resolution640x480)
            return ColorImageFormat.InfraredResolution640x480Fps30;
          break;
        case ImageType.ColorBayer:
          switch (imageResolution)
          {
            case ImageResolution.Resolution640x480:
              return ColorImageFormat.RawBayerResolution640x480Fps30;
            case ImageResolution.Resolution1280x960:
              return ColorImageFormat.RawBayerResolution1280x960Fps12;
          }
          break;
      }
      throw new InvalidOperationException();
    }

    internal static int LookUpPixelDataLength(ColorImageFormat format)
    {
      switch (ColorImageStream.LookUpImageResolution(format))
      {
        case ImageResolution.Resolution640x480:
          return 307200;
        case ImageResolution.Resolution1280x960:
          return 1228800;
        default:
          return 0;
      }
    }
  }
}
