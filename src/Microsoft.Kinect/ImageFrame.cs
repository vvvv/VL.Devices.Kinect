// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.ImageFrame
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Microsoft.Kinect
{
  public abstract class ImageFrame : IDisposable
  {
    private int _isDisposed;

    internal ImageFrame(
      ImageType imageType,
      ImageResolution imageResolution,
      int frameNumber,
      long timestamp,
      ImageFrameFlags frameFlags)
    {
      int width;
      int height;
      ImageStream.ResolutionToHeightWidth(imageResolution, out width, out height);
      int bytesPerPixel = ImageStream.ImageTypeToBytesPerPixel(imageType);
      this.Width = width;
      this.Height = height;
      this.BytesPerPixel = bytesPerPixel;
      this.FrameNumber = frameNumber;
      this.Timestamp = timestamp;
      this.FrameFlags = frameFlags;
    }

    public long Timestamp { get; private set; }

    public int FrameNumber { get; private set; }

    public int Width { get; private set; }

    public int Height { get; private set; }

    public int BytesPerPixel { get; private set; }

    public abstract int PixelDataLength { get; }

    public void Dispose()
    {
      if (Interlocked.Exchange(ref this._isDisposed, 1) != 0)
        return;
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    ~ImageFrame()
    {
      Debugger.Log(0, "Performance", string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}\n", (object) Resources.ImageFrameNotDisposed));
      this.Dispose(false);
    }

    protected abstract void Dispose(bool disposing);

    internal abstract ImageStream SourceStream { get; }

    internal ImageFrameFlags FrameFlags { get; private set; }
  }
}
