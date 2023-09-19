// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.IFTResult
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [Guid("1A00A7BB-C217-11E0-AC90-0024811441FD")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  internal interface IFTResult
  {
    void Reset();

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int CopyTo([In] IFTResult destResult);

    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetStatus();

    void GetFaceRect(out Rect rect);

    void Get2DShapePoints(out IntPtr pointsPtr, out uint pointCount);

    void Get3DPose(out float scale, out Vector3DF rotationXYZ, out Vector3DF translationXYZ);

    void GetAUCoefficients(out IntPtr animUnitCoeffPtr, out uint animUnitCount);
  }
}
