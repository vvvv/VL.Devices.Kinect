// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.AllFramesReadyEventArgs
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;

namespace Microsoft.Kinect
{
  public sealed class AllFramesReadyEventArgs : EventArgs
  {
    private readonly ColorImageFrameReadyEventArgs _colorArgs;
    private readonly DepthImageFrameReadyEventArgs _depthArgs;
    private readonly SkeletonFrameReadyEventArgs _skeletonArgs;

    internal AllFramesReadyEventArgs(
      ColorImageFrameReadyEventArgs colorArgs,
      DepthImageFrameReadyEventArgs depthArgs,
      SkeletonFrameReadyEventArgs skeletonArgs)
    {
      this._colorArgs = colorArgs;
      this._depthArgs = depthArgs;
      this._skeletonArgs = skeletonArgs;
    }

    public ColorImageFrame OpenColorImageFrame()
    {
      if (this._colorArgs == null)
        return (ColorImageFrame) null;
      KinectEtwProvider.EventWriteManagedOpenFrameFromAllFramesEventInfo(0);
      return this._colorArgs.OpenColorImageFrame();
    }

    public DepthImageFrame OpenDepthImageFrame()
    {
      if (this._depthArgs == null)
        return (DepthImageFrame) null;
      KinectEtwProvider.EventWriteManagedOpenFrameFromAllFramesEventInfo(1);
      return this._depthArgs.OpenDepthImageFrame();
    }

    public SkeletonFrame OpenSkeletonFrame()
    {
      if (this._skeletonArgs == null)
        return (SkeletonFrame) null;
      KinectEtwProvider.EventWriteManagedOpenFrameFromAllFramesEventInfo(2);
      return this._skeletonArgs.OpenSkeletonFrame();
    }
  }
}
