// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.NativeMethods
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  internal static class NativeMethods
  {
    [DllImport("FaceTrackLib.dll", CharSet = CharSet.Unicode)]
    public static extern IFTFaceTracker FTCreateFaceTracker(IntPtr reserved);

    [DllImport("FaceTrackLib.dll", CharSet = CharSet.Unicode)]
    public static extern IFTImage FTCreateImage();
  }
}
