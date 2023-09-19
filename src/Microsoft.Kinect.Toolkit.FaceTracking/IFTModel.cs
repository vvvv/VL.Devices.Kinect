// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.IFTModel
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [Guid("1A00A7BD-C217-11E0-AC90-0024811441FD")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IFTModel
  {
    [MethodImpl(MethodImplOptions.PreserveSig)]
    uint GetSUCount();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    uint GetAUCount();

    void GetTriangles(out IntPtr trianglesPtr, out uint triangleCount);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    uint GetVertexCount();

    void Get3DShape(
      IntPtr shapeUnitCoeffsPtr,
      uint shapeUnitCount,
      IntPtr animUnitCoeffPtr,
      uint animUnitCount,
      float scale,
      ref Vector3DF rotationXYZ,
      ref Vector3DF translationXYZ,
      IntPtr vertices,
      uint vertexCount);

    void GetProjectedShape(
      CameraConfig cameraConfig,
      float zoomFactor,
      Point viewOffset,
      IntPtr shapeUnitCoeffPtr,
      uint shapeUnitCount,
      IntPtr animUnitCoeffsPtr,
      uint animUnitCount,
      float scale,
      ref Vector3DF rotationXYZ,
      ref Vector3DF translationXYZ,
      IntPtr vertices,
      uint vertexCount);
  }
}
