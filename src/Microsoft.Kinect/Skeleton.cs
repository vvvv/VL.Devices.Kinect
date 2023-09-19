// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Skeleton
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;
using System.Xml.Serialization;

namespace Microsoft.Kinect
{
  [Serializable]
  public class Skeleton
  {
    internal static readonly Array JointTypeValues = Enum.GetValues(typeof (JointType));
    [XmlIgnore]
    [NonSerialized]
    private BoneOrientationCollection _boneOrientations;

    internal Skeleton(_NUI_SKELETON_DATA nativeSkeleton) => this.CopyFrom(nativeSkeleton);

    internal Skeleton(Skeleton sourceData) => this.CopyFrom(sourceData);

    public Skeleton()
    {
      this.Joints = new JointCollection(this);
      Joint joint = new Joint();
      foreach (JointType jointTypeValue in Skeleton.JointTypeValues)
      {
        joint.JointType = jointTypeValue;
        this.Joints[jointTypeValue] = joint;
      }
    }

    public SkeletonTrackingState TrackingState { get; set; }

    public int TrackingId { get; set; }

    public SkeletonPoint Position { get; set; }

    public JointCollection Joints { get; protected set; }

    public BoneOrientationCollection BoneOrientations
    {
      get
      {
        if (this._boneOrientations == null)
          this._boneOrientations = new BoneOrientationCollection(this);
        this._boneOrientations.Recalculate();
        return this._boneOrientations;
      }
      protected set => this._boneOrientations = value;
    }

    public FrameEdges ClippedEdges { get; set; }

    internal void CopyFrom(_NUI_SKELETON_DATA nativeSkeleton)
    {
      this.TrackingState = (SkeletonTrackingState) nativeSkeleton.eTrackingState;
      this.TrackingId = (int) nativeSkeleton.dwTrackingID;
      this.Position = new SkeletonPoint()
      {
        X = nativeSkeleton.Position.x,
        Y = nativeSkeleton.Position.y,
        Z = nativeSkeleton.Position.z
      };
      this.ClippedEdges = (FrameEdges) nativeSkeleton.dwQualityFlags;
      this.Joints = new JointCollection(this);
      Joint joint = new Joint();
      foreach (JointType jointTypeValue in Skeleton.JointTypeValues)
      {
        joint.JointType = jointTypeValue;
        joint.TrackingState = (JointTrackingState) nativeSkeleton.eSkeletonPositionTrackingState[(int) jointTypeValue];
        _Vector4 skeletonPosition = nativeSkeleton.SkeletonPositions[(int) jointTypeValue];
        joint.Position = new SkeletonPoint()
        {
          X = skeletonPosition.x,
          Y = skeletonPosition.y,
          Z = skeletonPosition.z
        };
        this.Joints[jointTypeValue] = joint;
      }
    }

    internal void CopyFrom(Skeleton sourceData)
    {
      this.TrackingState = sourceData.TrackingState;
      this.TrackingId = sourceData.TrackingId;
      this.Position = sourceData.Position;
      this.ClippedEdges = sourceData.ClippedEdges;
      if (this.Joints == null)
        this.Joints = new JointCollection(this);
      foreach (JointType jointTypeValue in Skeleton.JointTypeValues)
        this.Joints[jointTypeValue] = new Joint()
        {
          Position = sourceData.Joints[jointTypeValue].Position,
          JointType = sourceData.Joints[jointTypeValue].JointType,
          TrackingState = sourceData.Joints[jointTypeValue].TrackingState
        };
      if (sourceData._boneOrientations != null && sourceData._boneOrientations.IsValid)
      {
        if (this._boneOrientations == null)
          this._boneOrientations = new BoneOrientationCollection(this);
        this._boneOrientations.IsValid = true;
        foreach (JointType jointTypeValue in Skeleton.JointTypeValues)
        {
          BoneOrientation boneOrientation = new BoneOrientation(jointTypeValue);
          boneOrientation.CopyFrom(sourceData._boneOrientations[jointTypeValue]);
          this._boneOrientations[jointTypeValue] = boneOrientation;
        }
      }
      else
        this._boneOrientations = (BoneOrientationCollection) null;
    }

    internal void InvalidateBoneOrientations()
    {
      if (this._boneOrientations == null)
        return;
      this._boneOrientations.Invalidate();
    }

    internal bool CalculateBoneOrientations(BoneOrientation[] boneOrientations)
    {
      if (this.TrackingState != SkeletonTrackingState.NotTracked && (this.Joints[JointType.HipCenter].TrackingState != JointTrackingState.NotTracked || this.Joints[JointType.ShoulderCenter].TrackingState != JointTrackingState.NotTracked))
      {
        _NUI_SKELETON_DATA pSkeletonData = new _NUI_SKELETON_DATA();
        pSkeletonData.eTrackingState = _NUI_SKELETON_TRACKING_STATE.NUI_SKELETON_TRACKED;
        pSkeletonData.dwTrackingID = (uint) this.TrackingId;
        pSkeletonData.Position.x = this.Position.X;
        pSkeletonData.Position.y = this.Position.Y;
        pSkeletonData.Position.z = this.Position.Z;
        pSkeletonData.eSkeletonPositionTrackingState = new _NUI_SKELETON_POSITION_TRACKING_STATE[Skeleton.JointTypeValues.Length];
        pSkeletonData.SkeletonPositions = new _Vector4[Skeleton.JointTypeValues.Length];
        foreach (JointType jointTypeValue in Skeleton.JointTypeValues)
        {
          if (this.Joints[jointTypeValue].TrackingState == JointTrackingState.NotTracked)
          {
            pSkeletonData.eSkeletonPositionTrackingState[(int) jointTypeValue] = _NUI_SKELETON_POSITION_TRACKING_STATE.NUI_SKELETON_POSITION_NOT_TRACKED;
          }
          else
          {
            Joint joint = this.Joints[jointTypeValue];
            pSkeletonData.eSkeletonPositionTrackingState[(int) jointTypeValue] = joint.TrackingState != JointTrackingState.Inferred ? _NUI_SKELETON_POSITION_TRACKING_STATE.NUI_SKELETON_POSITION_TRACKED : _NUI_SKELETON_POSITION_TRACKING_STATE.NUI_SKELETON_POSITION_INFERRED;
          }
          pSkeletonData.SkeletonPositions[(int) jointTypeValue].x = this.Joints[jointTypeValue].Position.X;
          pSkeletonData.SkeletonPositions[(int) jointTypeValue].y = this.Joints[jointTypeValue].Position.Y;
          pSkeletonData.SkeletonPositions[(int) jointTypeValue].z = this.Joints[jointTypeValue].Position.Z;
        }
        _NUI_SKELETON_BONE_ORIENTATION[] pBoneOrientations = new _NUI_SKELETON_BONE_ORIENTATION[Skeleton.JointTypeValues.Length];
        int boneOrientations1 = NativeMethods.NuiSkeletonCalculateBoneOrientations(ref pSkeletonData, pBoneOrientations);
        foreach (JointType jointTypeValue in Skeleton.JointTypeValues)
          boneOrientations[(int) jointTypeValue].CopyFrom(ref pBoneOrientations[(int) jointTypeValue]);
        if (boneOrientations1 == 0)
          return true;
      }
      foreach (BoneOrientation boneOrientation in boneOrientations)
        boneOrientation.SetDefaults();
      return false;
    }
  }
}
