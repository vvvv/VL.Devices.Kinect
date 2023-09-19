// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.HeadPoints
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [StructLayout(LayoutKind.Sequential)]
  internal class HeadPoints
  {
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
    private Vector3DF[] points;

    public Vector3DF[] Points
    {
      set => this.points = value;
    }
  }
}
