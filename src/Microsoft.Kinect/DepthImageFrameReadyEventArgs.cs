// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.DepthImageFrameReadyEventArgs
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;

namespace Microsoft.Kinect
{
  public sealed class DepthImageFrameReadyEventArgs : EventArgs
  {
    private readonly DepthImageStream _depthImageStream;
    private readonly int _frameNumber;
    private readonly long _timestamp;
    private readonly ImageFrameFlags _frameFlags;
    private bool _isInvalid;

    private DepthImageFrameReadyEventArgs(
      DepthImageStream depthImageStream,
      int frameNumber,
      long timestamp,
      ImageFrameFlags frameFlags,
      bool isInvalid)
    {
      this._depthImageStream = depthImageStream;
      this._frameNumber = frameNumber;
      this._timestamp = timestamp;
      this._frameFlags = frameFlags;
      this._isInvalid = isInvalid;
    }

    internal static DepthImageFrameReadyEventArgs Create(
      DepthImageStream depthImageStream,
      int frameNumber,
      long timestamp,
      ImageFrameFlags frameFlags)
    {
      return new DepthImageFrameReadyEventArgs(depthImageStream, frameNumber, timestamp, frameFlags, false);
    }

    internal static DepthImageFrameReadyEventArgs CreateInvalid() => new DepthImageFrameReadyEventArgs((DepthImageStream) null, 0, 0L, ImageFrameFlags.NUI_IMAGE_FRAME_FLAG_NONE, true);

    public DepthImageFrame OpenDepthImageFrame()
    {
      if (this._isInvalid)
        return (DepthImageFrame) null;
      KinectEtwProvider.EventWriteManagedOpenFrameFromEventInfo(1);
      DepthImageFrame depthImageFrame = DepthImageFrame.Create(this._depthImageStream, this._frameNumber, this._timestamp, this._frameFlags);
      if (depthImageFrame == null)
        this._isInvalid = true;
      return depthImageFrame;
    }
  }
}
