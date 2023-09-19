// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.ColorImagePoint
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Diagnostics;

namespace Microsoft.Kinect
{
  [DebuggerDisplay("X:{X} Y:{Y}")]
  public struct ColorImagePoint
  {
    public int X { get; set; }

    public int Y { get; set; }

    public override int GetHashCode() => this.X.GetHashCode() + this.Y.GetHashCode();

    public override bool Equals(object obj) => obj is ColorImagePoint imagePoint && this.Equals(imagePoint);

    public bool Equals(ColorImagePoint imagePoint) => this.X == imagePoint.X && this.Y == imagePoint.Y;

    public static bool operator ==(ColorImagePoint imagePoint1, ColorImagePoint imagePoint2) => imagePoint1.Equals(imagePoint2);

    public static bool operator !=(ColorImagePoint imagePoint1, ColorImagePoint imagePoint2) => !imagePoint1.Equals(imagePoint2);
  }
}
