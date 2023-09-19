// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.BoneOrientation
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;

namespace Microsoft.Kinect
{
  public class BoneOrientation
  {
    private static readonly JointType[] c_parents = new JointType[20]
    {
      JointType.HipCenter,
      JointType.HipCenter,
      JointType.Spine,
      JointType.ShoulderCenter,
      JointType.ShoulderCenter,
      JointType.ShoulderLeft,
      JointType.ElbowLeft,
      JointType.WristLeft,
      JointType.ShoulderCenter,
      JointType.ShoulderRight,
      JointType.ElbowRight,
      JointType.WristRight,
      JointType.HipCenter,
      JointType.HipLeft,
      JointType.KneeLeft,
      JointType.AnkleLeft,
      JointType.HipCenter,
      JointType.HipRight,
      JointType.KneeRight,
      JointType.AnkleRight
    };

    public BoneOrientation(JointType jointType)
    {
      this.EndJoint = jointType;
      this.StartJoint = BoneOrientation.c_parents[(int) jointType];
      this.HierarchicalRotation = new BoneRotation();
      this.AbsoluteRotation = new BoneRotation();
      this.SetDefaults();
    }

    internal void SetDefaults()
    {
      this.HierarchicalRotation.Matrix = Matrix4.Identity;
      this.HierarchicalRotation.Quaternion = new Vector4()
      {
        W = 1f
      };
      this.AbsoluteRotation.Matrix = Matrix4.Identity;
      this.AbsoluteRotation.Quaternion = new Vector4()
      {
        W = 1f
      };
    }

    internal void CopyFrom(
      ref _NUI_SKELETON_BONE_ORIENTATION nativeBoneOrientation)
    {
      this.HierarchicalRotation.Matrix = Matrix4.CopyFrom(ref nativeBoneOrientation.hierarchicalRotation.rotationMatrix);
      this.HierarchicalRotation.Quaternion = Vector4.CopyFrom(ref nativeBoneOrientation.hierarchicalRotation.rotationQuaternion);
      this.AbsoluteRotation.Matrix = Matrix4.CopyFrom(ref nativeBoneOrientation.absoluteRotation.rotationMatrix);
      this.AbsoluteRotation.Quaternion = Vector4.CopyFrom(ref nativeBoneOrientation.absoluteRotation.rotationQuaternion);
    }

    internal void CopyFrom(BoneOrientation sourceBoneOrientation)
    {
      this.HierarchicalRotation.Matrix = sourceBoneOrientation.HierarchicalRotation.Matrix;
      this.HierarchicalRotation.Quaternion = sourceBoneOrientation.HierarchicalRotation.Quaternion;
      this.AbsoluteRotation.Matrix = sourceBoneOrientation.AbsoluteRotation.Matrix;
      this.AbsoluteRotation.Quaternion = sourceBoneOrientation.AbsoluteRotation.Quaternion;
    }

    public JointType EndJoint { get; internal set; }

    public JointType StartJoint { get; internal set; }

    public BoneRotation HierarchicalRotation { get; set; }

    public BoneRotation AbsoluteRotation { get; set; }
  }
}
