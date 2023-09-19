// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.Vector3DF
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System.Diagnostics;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  [DebuggerDisplay("({X},{Y},{Z})")]
  public struct Vector3DF
  {
    public Vector3DF(float x, float y, float z)
      : this()
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
    }

    public static Vector3DF Empty => new Vector3DF(0.0f, 0.0f, 0.0f);

    public float X { get; set; }

    public float Y { get; set; }

    public float Z { get; set; }

    public static bool operator ==(Vector3DF vector1, Vector3DF vector2) => vector1.Equals(vector2);

    public static bool operator !=(Vector3DF vector1, Vector3DF vector2) => !vector1.Equals(vector2);

    public override int GetHashCode()
    {
      float num1 = this.X;
      int hashCode1 = num1.GetHashCode();
      num1 = this.Y;
      int hashCode2 = num1.GetHashCode();
      int num2 = hashCode1 ^ hashCode2;
      num1 = this.Z;
      int hashCode3 = num1.GetHashCode();
      return num2 ^ hashCode3;
    }

    public override bool Equals(object obj) => obj is Vector3DF other && this.Equals(other);

    public bool Equals(Vector3DF other) => (double) this.X == (double) other.X && (double) this.Y == (double) other.Y && (double) this.Z == (double) other.Z;
  }
}
