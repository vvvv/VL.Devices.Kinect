// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Vector4
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;

namespace Microsoft.Kinect
{
  public struct Vector4
  {
    public float X { get; set; }

    public float Y { get; set; }

    public float Z { get; set; }

    public float W { get; set; }

    public override bool Equals(object obj) => obj is Vector4 vector && this.Equals(vector);

    public bool Equals(Vector4 vector) => this.X.Equals(vector.X) && this.Y.Equals(vector.Y) && this.Z.Equals(vector.Z) && this.W.Equals(vector.W);

    public override int GetHashCode() => this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();

    public static bool operator ==(Vector4 vector1, Vector4 vector2) => vector1.Equals(vector2);

    public static bool operator !=(Vector4 vector1, Vector4 vector2) => !vector1.Equals(vector2);

    internal static Vector4 CopyFrom(ref _Vector4 vector) => new Vector4()
    {
      X = vector.x,
      Y = vector.y,
      Z = vector.z,
      W = vector.w
    };
  }
}
