// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.FaceTrackingSensorData
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  internal struct FaceTrackingSensorData
  {
    public IFTImage VideoFrame;
    public IFTImage DepthFrame;
    public float ZoomFactor;
    public Point ViewOffset;
  }
}
