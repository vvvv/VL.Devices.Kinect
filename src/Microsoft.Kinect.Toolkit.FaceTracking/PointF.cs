// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.PointF
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System.Diagnostics;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [DebuggerDisplay("({x},{y})")]
  public struct PointF
  {
    private readonly float x;
    private readonly float y;

    public PointF(float x, float y)
    {
      this.x = x;
      this.y = y;
    }

    public static PointF Empty => new PointF(0.0f, 0.0f);

    public float X => this.x;

    public float Y => this.y;

    public static bool operator ==(PointF point1, PointF point2) => point1.Equals(point2);

    public static bool operator !=(PointF point1, PointF point2) => !point1.Equals(point2);

    public override int GetHashCode()
    {
      float num = this.x;
      int hashCode1 = num.GetHashCode();
      num = this.y;
      int hashCode2 = num.GetHashCode();
      return hashCode1 ^ hashCode2;
    }

    public override bool Equals(object obj) => obj is PointF other && this.Equals(other);

    public bool Equals(PointF other) => (double) this.x == (double) other.x && (double) this.y == (double) other.y;
  }
}
