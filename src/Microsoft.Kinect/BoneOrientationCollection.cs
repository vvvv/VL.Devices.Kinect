// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.BoneOrientationCollection
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Kinect
{
  public class BoneOrientationCollection : IEnumerable<BoneOrientation>, IEnumerable
  {
    private readonly BoneOrientation[] _boneOrientations;
    private bool _isValid;
    [XmlIgnore]
    [NonSerialized]
    private readonly Skeleton _skeletonBackReference;

    internal BoneOrientationCollection(Skeleton backReference)
    {
      this._boneOrientations = new BoneOrientation[Skeleton.JointTypeValues.Length];
      foreach (JointType jointTypeValue in Skeleton.JointTypeValues)
        this._boneOrientations[(int) jointTypeValue] = new BoneOrientation(jointTypeValue);
      this._skeletonBackReference = backReference;
    }

    public int Count => this._boneOrientations.Length;

    internal bool IsValid
    {
      get => this._isValid;
      set => this._isValid = value;
    }

    public BoneOrientation this[JointType jointType]
    {
      get
      {
        this.Recalculate();
        return this._boneOrientations[(int) jointType];
      }
      set
      {
        if (value == null || value.EndJoint != jointType)
          throw new ArgumentException(Resources.IncorrectJointType);
        this.Recalculate();
        this._isValid = true;
        this._boneOrientations[(int) jointType] = value;
      }
    }

    public IEnumerator GetEnumerator()
    {
      this.Recalculate();
      return this._boneOrientations.GetEnumerator();
    }

    IEnumerator<BoneOrientation> IEnumerable<BoneOrientation>.GetEnumerator()
    {
      this.Recalculate();
      return ((IEnumerable<BoneOrientation>) this._boneOrientations).GetEnumerator();
    }

    internal void Recalculate()
    {
      if (this._isValid || this._skeletonBackReference == null)
        return;
      this._isValid = this._skeletonBackReference.CalculateBoneOrientations(this._boneOrientations);
    }

    internal void Invalidate() => this._isValid = false;
  }
}
