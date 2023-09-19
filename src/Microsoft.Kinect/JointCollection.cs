// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.JointCollection
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.Kinect
{
  [Serializable]
  public class JointCollection : IEnumerable<Joint>, IEnumerable
  {
    private readonly Joint[] _skeletonData;
    [XmlIgnore]
    [NonSerialized]
    private readonly Skeleton _skeletonBackReference;

    internal JointCollection(Skeleton backReference)
    {
      this._skeletonData = new Joint[Skeleton.JointTypeValues.Length];
      this._skeletonBackReference = backReference;
    }

    public int Count => this._skeletonData.Length;

    public Joint this[JointType jointType]
    {
      get => this._skeletonData[(int) jointType];
      set
      {
        if (value.JointType != jointType)
          throw new ArgumentException(Resources.IncorrectJointType);
        if (this._skeletonBackReference != null)
          this._skeletonBackReference.InvalidateBoneOrientations();
        this._skeletonData[(int) jointType] = value;
      }
    }

    public IEnumerator GetEnumerator() => this._skeletonData.GetEnumerator();

    IEnumerator<Joint> IEnumerable<Joint>.GetEnumerator() => ((IEnumerable<Joint>) this._skeletonData).GetEnumerator();
  }
}
