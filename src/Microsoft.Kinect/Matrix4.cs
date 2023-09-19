// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Matrix4
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;

namespace Microsoft.Kinect
{
  public struct Matrix4
  {
    public float M11 { get; set; }

    public float M12 { get; set; }

    public float M13 { get; set; }

    public float M14 { get; set; }

    public float M21 { get; set; }

    public float M22 { get; set; }

    public float M23 { get; set; }

    public float M24 { get; set; }

    public float M31 { get; set; }

    public float M32 { get; set; }

    public float M33 { get; set; }

    public float M34 { get; set; }

    public float M41 { get; set; }

    public float M42 { get; set; }

    public float M43 { get; set; }

    public float M44 { get; set; }

    public static Matrix4 Identity => new Matrix4()
    {
      M11 = 1f,
      M22 = 1f,
      M33 = 1f,
      M44 = 1f
    };

    public override bool Equals(object obj) => obj is Matrix4 mat && this.Equals(mat);

    public bool Equals(Matrix4 mat) => this.M11.Equals(mat.M11) && this.M12.Equals(mat.M12) && this.M13.Equals(mat.M13) && this.M14.Equals(mat.M14) && this.M21.Equals(mat.M21) && this.M22.Equals(mat.M22) && this.M23.Equals(mat.M23) && this.M24.Equals(mat.M24) && this.M31.Equals(mat.M31) && this.M32.Equals(mat.M32) && this.M33.Equals(mat.M33) && this.M34.Equals(mat.M34) && this.M41.Equals(mat.M41) && this.M42.Equals(mat.M42) && this.M43.Equals(mat.M43) && this.M44.Equals(mat.M44);

    public override int GetHashCode() => this.M11.GetHashCode() + this.M12.GetHashCode() + this.M13.GetHashCode() + this.M14.GetHashCode() + this.M21.GetHashCode() + this.M22.GetHashCode() + this.M23.GetHashCode() + this.M24.GetHashCode() + this.M31.GetHashCode() + this.M32.GetHashCode() + this.M33.GetHashCode() + this.M34.GetHashCode() + this.M41.GetHashCode() + this.M42.GetHashCode() + this.M43.GetHashCode() + this.M44.GetHashCode();

    public static bool operator ==(Matrix4 mat1, Matrix4 mat2) => mat1.Equals(mat2);

    public static bool operator !=(Matrix4 mat1, Matrix4 mat2) => !mat1.Equals(mat2);

    internal static Matrix4 CopyFrom(ref _Matrix4 mat) => new Matrix4()
    {
      M11 = mat.M11,
      M12 = mat.M12,
      M13 = mat.M13,
      M14 = mat.M14,
      M21 = mat.M21,
      M22 = mat.M22,
      M23 = mat.M23,
      M24 = mat.M24,
      M31 = mat.M31,
      M32 = mat.M32,
      M33 = mat.M33,
      M34 = mat.M34,
      M41 = mat.M41,
      M42 = mat.M42,
      M43 = mat.M43,
      M44 = mat.M44
    };
  }
}
