// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.SkeletonFrame
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Microsoft.Kinect
{
  public sealed class SkeletonFrame : IDisposable
  {
    private readonly SkeletonStream _skeletonStream;
    private DataPool<int, Tuple<float, float, float, float>, object, Skeleton[], bool?>.Entry _skeletonData;
    private readonly object _dataAccessLock = new object();
    private int _isDisposed;

    private SkeletonFrame(
      SkeletonStream skeletonStream,
      int frameNumber,
      long timestamp,
      SkeletonTrackingMode trackingMode,
      DataPool<int, Tuple<float, float, float, float>, object, Skeleton[], bool?>.Entry skeletonData)
    {
      this._skeletonStream = skeletonStream;
      this._skeletonData = skeletonData;
      this.FrameNumber = frameNumber;
      this.Timestamp = timestamp;
      this.TrackingMode = trackingMode;
      this.FloorClipPlane = skeletonData.Value1;
      this._skeletonData = skeletonData;
    }

    internal static SkeletonFrame Create(
      SkeletonStream skeletonStream,
      int frameNumber,
      long timestamp,
      SkeletonTrackingMode trackingMode)
    {
      SkeletonFrame skeletonFrame = (SkeletonFrame) null;
      DataPool<int, Tuple<float, float, float, float>, object, Skeleton[], bool?>.Entry skeletonData = skeletonStream.LockSkeletonData(frameNumber);
      if (skeletonData != null)
        skeletonFrame = new SkeletonFrame(skeletonStream, frameNumber, timestamp, trackingMode, skeletonData);
      return skeletonFrame;
    }

    public long Timestamp { get; set; }

    public int FrameNumber { get; set; }

    [Obsolete("SkeletonFrame.TrackingMode property is reserved for future use.  Do not use this property; this value may change in a future release.", false)]
    public SkeletonTrackingMode TrackingMode { get; private set; }

    public Tuple<float, float, float, float> FloorClipPlane { get; set; }

    public int SkeletonArrayLength => 6;

    public IDepthFilter DepthFilter => this._skeletonStream.Sensor.NuiSensor.GetDepthFilterForTimestamp(this.Timestamp);

    public void CopySkeletonDataTo(Skeleton[] skeletonData)
    {
      if (skeletonData == null)
        throw new ArgumentNullException(nameof (skeletonData));
      if (skeletonData.Length != this._skeletonData.Value3.Length)
        throw new ArgumentException(Resources.SkeletonBufferIncorrectLength, nameof (skeletonData));
      lock (this._dataAccessLock)
      {
        if (this._skeletonData == null)
          throw new ObjectDisposedException(nameof (SkeletonFrame));
        for (int index = 0; index < this._skeletonData.Value3.Length; ++index)
        {
          if (skeletonData[index] == null)
            skeletonData[index] = new Skeleton(this._skeletonData.Value3[index]);
          else
            skeletonData[index].CopyFrom(this._skeletonData.Value3[index]);
        }
      }
    }

    public void Dispose()
    {
      if (Interlocked.Exchange(ref this._isDisposed, 1) != 0)
        return;
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    ~SkeletonFrame()
    {
      Debugger.Log(0, "Performance", string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}\n", (object) Resources.SkeletonFrameNotDisposed));
      this.Dispose(false);
    }

    private void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      lock (this._dataAccessLock)
      {
        this._skeletonStream.UnlockSkeletonData(this._skeletonData);
        this._skeletonData = (DataPool<int, Tuple<float, float, float, float>, object, Skeleton[], bool?>.Entry) null;
      }
    }
  }
}
