// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.SkeletonPoint
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  [DebuggerDisplay("X:{X} Y:{Y} Z:{Z}")]
  [Serializable]
  [StructLayout(LayoutKind.Sequential, Size = 16, Pack = 4)]
  public struct SkeletonPoint
  {
    public float X { get; set; }

    public float Y { get; set; }

    public float Z { get; set; }

    public override bool Equals(object obj) => obj is SkeletonPoint skeletonPoint && this.Equals(skeletonPoint);

    public bool Equals(SkeletonPoint skeletonPoint) => this.X.Equals(skeletonPoint.X) && this.Y.Equals(skeletonPoint.Y) && this.Z.Equals(skeletonPoint.Z);

    public override int GetHashCode() => this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode();

    public static bool operator ==(SkeletonPoint skeletonPoint1, SkeletonPoint skeletonPoint2) => skeletonPoint1.Equals(skeletonPoint2);

    public static bool operator !=(SkeletonPoint skeletonPoint1, SkeletonPoint skeletonPoint2) => !skeletonPoint1.Equals(skeletonPoint2);
  }
}
