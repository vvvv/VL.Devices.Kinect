// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.FaceTrackingRegisterDepthToColor
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  internal delegate int FaceTrackingRegisterDepthToColor(
    uint depthFrameWidth,
    uint depthFrameHeight,
    uint colorFrameWidth,
    uint colorFrameHeight,
    float zoomFactor,
    Point viewOffset,
    int depthX,
    int depthY,
    ushort depthZ,
    out int colorX,
    out int colorY);
}
