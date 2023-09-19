// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.Rect
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System.Diagnostics;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [DebuggerDisplay("(l={Left},t={Top},r={Right},b={Bottom})")]
  public struct Rect
  {
    public Rect(int left, int top, int right, int bottom)
      : this()
    {
      this.Left = left;
      this.Top = top;
      this.Right = right;
      this.Bottom = bottom;
    }

    public static Rect Empty => new Rect(0, 0, 0, 0);

    public int Left { get; set; }

    public int Top { get; set; }

    public int Right { get; set; }

    public int Bottom { get; set; }

    public int Width => this.Right - this.Left;

    public int Height => this.Bottom - this.Top;

    public static bool operator ==(Rect point1, Rect point2) => point1.Equals(point2);

    public static bool operator !=(Rect point1, Rect point2) => !point1.Equals(point2);

    public override int GetHashCode() => this.Left ^ this.Right ^ this.Top ^ this.Bottom;

    public override bool Equals(object obj) => obj is Rect other && this.Equals(other);

    public bool Equals(Rect other) => this.Left == other.Left && this.Top == other.Top && this.Right == other.Right && this.Bottom == other.Bottom;
  }
}
