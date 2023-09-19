// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.DepthImagePoint
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Diagnostics;

namespace Microsoft.Kinect
{
  [DebuggerDisplay("X:{X} Y:{Y} Depth:{Depth} PlayerIndex:{PlayerIndex}")]
  public struct DepthImagePoint
  {
    private int _x;
    private int _y;
    private int _depth;
    private int _playerIndex;

    public int X
    {
      get => this._x;
      set => this._x = value;
    }

    public int Y
    {
      get => this._y;
      set => this._y = value;
    }

    public int Depth
    {
      get => this._depth;
      set => this._depth = value;
    }

    [Obsolete("The player index property is reserved. Do not use.")]
    public int PlayerIndex => 0;

    public override int GetHashCode() => this.X.GetHashCode() + this.Y.GetHashCode() + this.Depth.GetHashCode();

    public override bool Equals(object obj) => obj is DepthImagePoint imagePoint && this.Equals(imagePoint);

    public bool Equals(DepthImagePoint imagePoint) => this.X == imagePoint.X && this.Y == imagePoint.Y && this.Depth == imagePoint.Depth;

    public static bool operator ==(DepthImagePoint imagePoint1, DepthImagePoint imagePoint2) => imagePoint1.Equals(imagePoint2);

    public static bool operator !=(DepthImagePoint imagePoint1, DepthImagePoint imagePoint2) => !imagePoint1.Equals(imagePoint2);
  }
}
