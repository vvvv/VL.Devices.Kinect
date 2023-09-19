// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.SensorData
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  internal class SensorData
  {
    private readonly Image videoFrame;
    private readonly Image depthFrame;
    private readonly float zoomFactor;
    private readonly Point viewOffset;

    public SensorData(Image videoFrame, Image depthFrame, float zoomFactor, Point viewOffset)
    {
      this.videoFrame = videoFrame;
      this.depthFrame = depthFrame;
      this.zoomFactor = zoomFactor;
      this.viewOffset = viewOffset;
    }

    internal FaceTrackingSensorData FaceTrackingSensorData => new FaceTrackingSensorData()
    {
      VideoFrame = this.videoFrame != null ? this.videoFrame.ImagePtr : (IFTImage) null,
      DepthFrame = this.depthFrame != null ? this.depthFrame.ImagePtr : (IFTImage) null,
      ZoomFactor = this.zoomFactor,
      ViewOffset = this.viewOffset
    };
  }
}
