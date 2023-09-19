// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.ColorImageFrameReadyEventArgs
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;

namespace Microsoft.Kinect
{
  public sealed class ColorImageFrameReadyEventArgs : EventArgs
  {
    private readonly ColorImageStream _colorImageStream;
    private readonly int _frameNumber;
    private readonly long _timestamp;
    private readonly ImageFrameFlags _frameFlags;
    private bool _isInvalid;

    private ColorImageFrameReadyEventArgs(
      ColorImageStream colorImageStream,
      int frameNumber,
      long timestamp,
      ImageFrameFlags frameFlags,
      bool isInvalid)
    {
      this._colorImageStream = colorImageStream;
      this._frameNumber = frameNumber;
      this._timestamp = timestamp;
      this._frameFlags = frameFlags;
      this._isInvalid = isInvalid;
    }

    internal static ColorImageFrameReadyEventArgs Create(
      ColorImageStream colorImageStream,
      int frameNumber,
      long timestamp,
      ImageFrameFlags frameFlags)
    {
      return new ColorImageFrameReadyEventArgs(colorImageStream, frameNumber, timestamp, frameFlags, false);
    }

    internal static ColorImageFrameReadyEventArgs CreateInvalid() => new ColorImageFrameReadyEventArgs((ColorImageStream) null, 0, 0L, ImageFrameFlags.NUI_IMAGE_FRAME_FLAG_NONE, true);

    public ColorImageFrame OpenColorImageFrame()
    {
      if (this._isInvalid)
        return (ColorImageFrame) null;
      KinectEtwProvider.EventWriteManagedOpenFrameFromEventInfo(0);
      ColorImageFrame colorImageFrame = ColorImageFrame.Create(this._colorImageStream, this._frameNumber, this._timestamp, this._frameFlags);
      if (colorImageFrame == null)
        this._isInvalid = true;
      return colorImageFrame;
    }
  }
}
