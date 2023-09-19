// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.DepthImagePixel
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

namespace Microsoft.Kinect
{
  public struct DepthImagePixel
  {
    private const int _UnknownDepth = 0;

    public short PlayerIndex { get; set; }

    public short Depth { get; set; }

    public override int GetHashCode() => this.Depth.GetHashCode() + this.PlayerIndex.GetHashCode();

    public override bool Equals(object obj) => obj is DepthImagePixel pixel && this.Equals(pixel);

    public bool Equals(DepthImagePixel pixel) => (int) this.Depth == (int) pixel.Depth && (int) this.PlayerIndex == (int) pixel.PlayerIndex;

    public static bool operator ==(DepthImagePixel pixel1, DepthImagePixel pixel2) => pixel1.Equals(pixel2);

    public static bool operator !=(DepthImagePixel pixel1, DepthImagePixel pixel2) => !pixel1.Equals(pixel2);

    public bool IsKnownDepth => this.Depth != (short) 0;
  }
}
