// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.ErrorCode
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  internal enum ErrorCode
  {
    InvalidModels = -1884553215, // 0x8FAC0001
    InvalidInputImage = -1884553214, // 0x8FAC0002
    FaceDetectorFailed = -1884553213, // 0x8FAC0003
    ActiveAppearanceModelFailed = -1884553212, // 0x8FAC0004
    NeuralNetworkFailed = -1884553211, // 0x8FAC0005
    FaceTrackerUninitialized = -1884553210, // 0x8FAC0006
    InvalidModelPath = -1884553209, // 0x8FAC0007
    EvaluationFailed = -1884553208, // 0x8FAC0008
    InvalidCameraConfig = -1884553207, // 0x8FAC0009
    Invalid3DHint = -1884553206, // 0x8FAC000A
    HeadSearchFailed = -1884553205, // 0x8FAC000B
    UserLost = -1884553204, // 0x8FAC000C
    KinectDllLoadFailed = -1884553203, // 0x8FAC000D
    Success = 0,
  }
}
