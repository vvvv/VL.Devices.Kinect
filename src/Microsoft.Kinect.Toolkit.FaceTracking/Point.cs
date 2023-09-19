// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.Point
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System.Diagnostics;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [DebuggerDisplay("({X},{Y})")]
  public struct Point
  {
    public Point(int x, int y)
      : this()
    {
      this.X = x;
      this.Y = y;
    }

    public static Point Empty => new Point(0, 0);

    public int X { get; private set; }

    public int Y { get; private set; }

    public static bool operator ==(Point point1, Point point2) => point1.Equals(point2);

    public static bool operator !=(Point point1, Point point2) => !point1.Equals(point2);

    public override int GetHashCode() => this.X ^ this.Y;

    public override bool Equals(object obj) => obj is Point other && this.Equals(other);

    public bool Equals(Point other) => this.X == other.X && this.Y == other.Y;
  }
}
