// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Joint
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Diagnostics;

namespace Microsoft.Kinect
{
  [DebuggerDisplay("Position:{Position} JointType:{JointType} TrackingState:{TrackingState}")]
  [Serializable]
  public struct Joint
  {
    public SkeletonPoint Position { get; set; }

    public JointType JointType { get; internal set; }

    public JointTrackingState TrackingState { get; set; }

    public override int GetHashCode() => this.Position.GetHashCode() + this.JointType.GetHashCode() + this.TrackingState.GetHashCode();

    public override bool Equals(object obj) => obj is Joint joint && this.Equals(joint);

    public bool Equals(Joint joint) => this.JointType == joint.JointType && this.Position == joint.Position && this.TrackingState == joint.TrackingState;

    public static bool operator ==(Joint joint1, Joint joint2) => joint1.Equals(joint2);

    public static bool operator !=(Joint joint1, Joint joint2) => !joint1.Equals(joint2);
  }
}
