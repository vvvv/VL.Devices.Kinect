// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.FaceTriangle
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System.Diagnostics;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [DebuggerDisplay("({first},{second},{third})")]
  public struct FaceTriangle
  {
    private int first;
    private int second;
    private int third;

    public FaceTriangle(int first, int second, int third)
    {
      this.first = first;
      this.second = second;
      this.third = third;
    }

    public int First
    {
      get => this.first;
      set => this.first = value;
    }

    public int Second
    {
      get => this.second;
      set => this.second = value;
    }

    public int Third
    {
      get => this.third;
      set => this.third = value;
    }

    public static bool operator ==(FaceTriangle triangle1, FaceTriangle triangle2) => triangle1.Equals(triangle2);

    public static bool operator !=(FaceTriangle triangle1, FaceTriangle triangle2) => !triangle1.Equals(triangle2);

    public override int GetHashCode() => this.first ^ this.second ^ this.third;

    public override bool Equals(object obj) => obj is FaceTriangle other && this.Equals(other);

    public bool Equals(FaceTriangle other) => this.first == other.first && this.second == other.second && this.third == other.third;
  }
}
