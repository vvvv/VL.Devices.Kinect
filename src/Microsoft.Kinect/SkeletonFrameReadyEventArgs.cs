// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.SkeletonFrameReadyEventArgs
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;

namespace Microsoft.Kinect
{
  public sealed class SkeletonFrameReadyEventArgs : EventArgs
  {
    private readonly SkeletonStream _skeletonStream;
    private readonly int _frameNumber;
    private readonly long _timestamp;
    private readonly SkeletonTrackingMode _trackingMode;
    private bool _isInvalid;

    private SkeletonFrameReadyEventArgs(
      SkeletonStream skeletonStream,
      int frameNumber,
      long timestamp,
      SkeletonTrackingMode trackingMode,
      bool isInvalid)
    {
      this._skeletonStream = skeletonStream;
      this._frameNumber = frameNumber;
      this._timestamp = timestamp;
      this._trackingMode = trackingMode;
      this._isInvalid = isInvalid;
    }

    internal static SkeletonFrameReadyEventArgs Create(
      SkeletonStream skeletonImageStream,
      int frameNumber,
      long timestamp,
      SkeletonTrackingMode trackingMode)
    {
      return new SkeletonFrameReadyEventArgs(skeletonImageStream, frameNumber, timestamp, trackingMode, false);
    }

    internal static SkeletonFrameReadyEventArgs CreateInvalid() => new SkeletonFrameReadyEventArgs((SkeletonStream) null, 0, 0L, SkeletonTrackingMode.Default, true);

    public SkeletonFrame OpenSkeletonFrame()
    {
      if (this._isInvalid)
        return (SkeletonFrame) null;
      KinectEtwProvider.EventWriteManagedOpenFrameFromEventInfo(2);
      SkeletonFrame skeletonFrame = SkeletonFrame.Create(this._skeletonStream, this._frameNumber, this._timestamp, this._trackingMode);
      if (skeletonFrame == null)
        this._isInvalid = true;
      return skeletonFrame;
    }
  }
}
