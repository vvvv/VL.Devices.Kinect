// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.TransformSmoothParameters
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  [DebuggerDisplay("Smoothing:{Smoothing} Correction:{Correction} Prediction:{Prediction} JitterRadius:{JitterRadius} MaxDeviationRadius:{MaxDeviationRadius}")]
  [StructLayout(LayoutKind.Sequential, Size = 24, Pack = 8)]
  public struct TransformSmoothParameters
  {
    public float Smoothing { get; set; }

    public float Correction { get; set; }

    public float Prediction { get; set; }

    public float JitterRadius { get; set; }

    public float MaxDeviationRadius { get; set; }

    public override int GetHashCode() => this.Smoothing.GetHashCode() + this.Correction.GetHashCode() + this.Prediction.GetHashCode() + this.JitterRadius.GetHashCode() + this.MaxDeviationRadius.GetHashCode();

    public override bool Equals(object obj) => obj is TransformSmoothParameters smoothParameters && this.Equals(smoothParameters);

    public bool Equals(TransformSmoothParameters smoothParameters) => this.Smoothing.Equals(smoothParameters.Smoothing) && this.Correction.Equals(smoothParameters.Correction) && this.Prediction.Equals(smoothParameters.Prediction) && this.JitterRadius.Equals(smoothParameters.JitterRadius) && this.MaxDeviationRadius.Equals(smoothParameters.MaxDeviationRadius);

    public static bool operator ==(
      TransformSmoothParameters smoothParameters1,
      TransformSmoothParameters smoothParameters2)
    {
      return smoothParameters1.Equals(smoothParameters2);
    }

    public static bool operator !=(
      TransformSmoothParameters smoothParameters1,
      TransformSmoothParameters smoothParameters2)
    {
      return !smoothParameters1.Equals(smoothParameters2);
    }
  }
}
