// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.EnumIndexableCollection`2
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  public class EnumIndexableCollection<TIndex, TValue> : IEnumerable<TValue>, IEnumerable
  {
    private readonly TValue[] valueArray;

    internal EnumIndexableCollection(TValue[] valueArray) => this.valueArray = valueArray;

    public int Count => this.valueArray == null ? 0 : ((IEnumerable<TValue>) this.valueArray).Count<TValue>();

    public TValue this[int index] => this.valueArray != null ? this.valueArray[index] : throw new InvalidOperationException();

    public TValue this[TIndex index]
    {
      get
      {
        if (this.valueArray == null)
          throw new InvalidOperationException();
        return this.valueArray[(int) Convert.ChangeType((object) index, typeof (int), (IFormatProvider) CultureInfo.InvariantCulture)];
      }
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public IEnumerator<TValue> GetEnumerator() => this.valueArray == null ? Enumerable.Empty<TValue>().GetEnumerator() : ((IEnumerable<TValue>) this.valueArray).AsEnumerable<TValue>().GetEnumerator();
  }
}
