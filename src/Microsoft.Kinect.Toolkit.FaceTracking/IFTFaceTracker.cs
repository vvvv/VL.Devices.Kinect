// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.IFTFaceTracker
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [Guid("1A00A7BA-C217-11E0-AC90-0024811441FD")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IFTFaceTracker
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int Initialize(
      CameraConfig videoCameraConfig,
      CameraConfig depthCameraConfig,
      IntPtr depthToColorMappingFunc,
      string modelPath);

    void Reset();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CreateFTResult(out IFTResult faceTrackResult);

    void SetShapeUnits(float scale, float[] shapeUnitCoeffsPtr, uint shapeUnitCount);

    void GetShapeUnits(
      out float scale,
      out IntPtr shapeUnitCoeffsPtr,
      [In, Out] ref uint shapeUnitCount,
      [MarshalAs(UnmanagedType.Bool)] out bool haveConverged);

    void SetShapeComputationState([MarshalAs(UnmanagedType.Bool)] bool isEnabled);

    void GetComputationState([MarshalAs(UnmanagedType.Bool)] out bool isEnabled);

    void GetFaceModel(out IFTModel model);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int StartTracking(
      ref FaceTrackingSensorData sensorData,
      ref Rect roi,
      HeadPoints headPoints,
      IFTResult faceTrackResult);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int ContinueTracking(
      ref FaceTrackingSensorData sensorData,
      HeadPoints headPoints,
      IFTResult faceTrackResult);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int DetectFaces(
      ref FaceTrackingSensorData sensorData,
      ref Rect roi,
      IntPtr faces,
      ref uint facesCount);
  }
}
