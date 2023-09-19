// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.ImageStream
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;
using System.Threading;

namespace Microsoft.Kinect
{
  public abstract class ImageStream
  {
    internal ManualResetEvent NextFrameEvent = new ManualResetEvent(false);
    private bool _capturing;
    private IntPtr _nativeStreamHandle;
    private ImageStreamFlags _imageStreamFlags;
    private readonly object _nativeStreamInitLock = new object();

    internal ImageStream(KinectSensor sensor, ImageStreamFlags imageStreamFlags = ImageStreamFlags.NUI_IMAGE_STREAM_FLAG_SUPPRESS_NO_FRAME_DATA)
    {
      this.Sensor = sensor;
      this.BufferCount = 2;
      this.IsEnabled = false;
      this._imageStreamFlags = imageStreamFlags;
    }

    public int FrameWidth { get; private set; }

    public int FrameHeight { get; private set; }

    public int FrameBytesPerPixel { get; protected set; }

    public int FramePixelDataLength { get; protected set; }

    internal KinectSensor Sensor { get; private set; }

    internal int BufferCount { get; private set; }

    internal ImageResolution Resolution { get; set; }

    internal abstract ImageType ImageType { get; }

    public bool IsEnabled { get; private set; }

    internal void InternalEnable()
    {
      if (this.Sensor.IsRunning)
        this.Open();
      this.IsEnabled = true;
    }

    public void Disable()
    {
      KinectEtwProvider.EventWriteManagedImageStreamDisabledInfo(this is ColorImageStream ? 0 : 1);
      if (!this.IsEnabled)
        return;
      if (this.Sensor.IsRunning)
        this.Close();
      this.IsEnabled = false;
    }

    internal void Start()
    {
      if (!this.IsEnabled)
        return;
      this.Open();
    }

    internal void Stop()
    {
      if (!this._capturing)
        return;
      this.Close();
    }

    internal void FillHeightWidth()
    {
      int width;
      int height;
      ImageStream.ResolutionToHeightWidth(this.Resolution, out width, out height);
      this.FrameWidth = width;
      this.FrameHeight = height;
    }

    internal static void ResolutionToHeightWidth(
      ImageResolution resolution,
      out int width,
      out int height)
    {
      switch (resolution)
      {
        case ImageResolution.Resolution80x60:
          width = 80;
          height = 60;
          break;
        case ImageResolution.Resolution320x240:
          width = 320;
          height = 240;
          break;
        case ImageResolution.Resolution640x480:
          width = 640;
          height = 480;
          break;
        case ImageResolution.Resolution1280x960:
          width = 1280;
          height = 960;
          break;
        default:
          throw new InvalidOperationException();
      }
    }

    internal static int ImageTypeToBytesPerPixel(ImageType imageType)
    {
      switch (imageType)
      {
        case ImageType.DepthAndPlayerIndex:
        case ImageType.ColorYuvRaw:
        case ImageType.Depth:
        case ImageType.ColorInfrared:
          return 2;
        case ImageType.Color:
        case ImageType.ColorYuv:
          return 4;
        case ImageType.ColorBayer:
          return 1;
        default:
          throw new InvalidOperationException();
      }
    }

    internal ImageStreamFlags ImageStreamFlags
    {
      get
      {
        if (this._capturing && this.Sensor.IsRunning)
          this._imageStreamFlags = (ImageStreamFlags) this.Sensor.NuiSensor.NuiImageStreamGetImageFrameFlags(this._nativeStreamHandle);
        return this._imageStreamFlags;
      }
      set
      {
        if (this._capturing && this.Sensor.IsRunning)
          this.Sensor.NuiSensor.NuiImageStreamSetImageFrameFlags(this._nativeStreamHandle, (uint) value);
        this._imageStreamFlags = value;
      }
    }

    private void Open()
    {
      lock (this._nativeStreamInitLock)
      {
        this.NextFrameEvent.Reset();
        this.Sensor.NuiSensor.NuiImageStreamOpen((_NUI_IMAGE_TYPE) this.ImageType, (_NUI_IMAGE_RESOLUTION) this.Resolution, (uint) this.ImageStreamFlags, (uint) this.BufferCount, this.NextFrameEvent.SafeWaitHandle.DangerousGetHandle(), out this._nativeStreamHandle);
      }
      this._capturing = true;
    }

    private void Close() => this._capturing = false;

    internal abstract void StorePixels(
      int frameNumber,
      _NUI_LOCKED_RECT lockedRect,
      ImageType imageType,
      ImageResolution resolution,
      IntPtr nativeStreamHandle,
      ref _NUI_IMAGE_FRAME pNativeFrame);

    internal bool TryGetNextFrameInternal(
      int millisecondsWait,
      out int frameNumber,
      out long timestamp,
      out ImageFrameFlags frameFlags)
    {
      frameNumber = 0;
      timestamp = 0L;
      frameFlags = ImageFrameFlags.NUI_IMAGE_FRAME_FLAG_NONE;
      lock (this._nativeStreamInitLock)
      {
        if (this._nativeStreamHandle == IntPtr.Zero)
          throw new InvalidOperationException(Resources.NativeStreamCantBeZero);
        _NUI_IMAGE_FRAME pImageFrame;
        int nextFrameNoThrow = this.Sensor.NuiSensor.NuiImageStreamGetNextFrameNoThrow(this._nativeStreamHandle, (uint) millisecondsWait, out pImageFrame);
        if (nextFrameNoThrow < 0)
        {
          this.NextFrameEvent.Reset();
          return false;
        }
        if (nextFrameNoThrow == 0)
        {
          if (pImageFrame.pFrameTexture != null)
          {
            try
            {
              INuiFrameTexture pFrameTexture = pImageFrame.pFrameTexture;
              _NUI_SURFACE_DESC pDesc = new _NUI_SURFACE_DESC();
              KinectExceptionHelper.CheckHr(pFrameTexture.GetLevelDesc(0U, ref pDesc));
              ImageType imageType = this.ImageType;
              ImageResolution resolution = this.Resolution;
              if (pImageFrame.eImageType != (_NUI_IMAGE_TYPE) imageType || pImageFrame.eResolution != (_NUI_IMAGE_RESOLUTION) resolution)
                return false;
              _NUI_LOCKED_RECT pLockedRect = new _NUI_LOCKED_RECT();
              tagRECT pRect = new tagRECT();
              KinectExceptionHelper.CheckHr(pFrameTexture.LockRect(0U, ref pLockedRect, ref pRect, 0U));
              frameNumber = (int) pImageFrame.dwFrameNumber;
              timestamp = pImageFrame.liTimeStamp.QuadPart;
              this.StorePixels(frameNumber, pLockedRect, imageType, resolution, this._nativeStreamHandle, ref pImageFrame);
              KinectExceptionHelper.CheckHr(pFrameTexture.UnlockRect(0U));
              frameFlags = (ImageFrameFlags) pImageFrame.dwFrameFlags;
              return true;
            }
            finally
            {
              this.Sensor.NuiSensor.NuiImageStreamReleaseFrame(this._nativeStreamHandle, ref pImageFrame);
            }
          }
        }
        return false;
      }
    }

    internal void Dispose()
    {
      this.Close();
      lock (this._nativeStreamInitLock)
      {
        if (this.NextFrameEvent == null)
          return;
        this.NextFrameEvent.Close();
      }
    }
  }
}
