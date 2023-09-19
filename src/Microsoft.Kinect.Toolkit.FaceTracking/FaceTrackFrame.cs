// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.FaceTrackFrame
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  public sealed class FaceTrackFrame : IDisposable, ICloneable
  {
    private bool disposed;
    private IFTResult faceTrackingResultPtr;
    private WeakReference parentFaceTracker;

    internal FaceTrackFrame(IFTResult faceTrackResultPtr, FaceTracker parentTracker)
    {
      this.faceTrackingResultPtr = faceTrackResultPtr != null ? faceTrackResultPtr : throw new InvalidOperationException("Cannot associate with a null native frame pointer");
      this.parentFaceTracker = new WeakReference((object) parentTracker, false);
    }

    private FaceTrackFrame()
    {
    }

    ~FaceTrackFrame() => this.InternalDispose();

    public Rect FaceRect
    {
      get
      {
        this.CheckPtrAndThrow();
        Rect rect;
        this.faceTrackingResultPtr.GetFaceRect(out rect);
        return rect;
      }
    }

    public Vector3DF Rotation
    {
      get
      {
        this.CheckPtrAndThrow();
        Vector3DF rotationXYZ;
        this.faceTrackingResultPtr.Get3DPose(out float _, out rotationXYZ, out Vector3DF _);
        return rotationXYZ;
      }
    }

    public bool TrackSuccessful => this.Status == ErrorCode.Success;

    public Vector3DF Translation
    {
      get
      {
        this.CheckPtrAndThrow();
        Vector3DF translationXYZ;
        this.faceTrackingResultPtr.Get3DPose(out float _, out Vector3DF _, out translationXYZ);
        return translationXYZ;
      }
    }

    internal IFTResult ResultPtr => this.faceTrackingResultPtr;

    internal float Scale
    {
      get
      {
        this.CheckPtrAndThrow();
        float scale;
        this.faceTrackingResultPtr.Get3DPose(out scale, out Vector3DF _, out Vector3DF _);
        return scale;
      }
    }

    internal ErrorCode Status
    {
      get
      {
        this.CheckPtrAndThrow();
        return (ErrorCode) this.faceTrackingResultPtr.GetStatus();
      }
    }

    public object Clone()
    {
      this.CheckPtrAndThrow();
      if (!(this.parentFaceTracker.Target is FaceTracker target))
        throw new ObjectDisposedException("FaceTracker", "Underlying face object has been garbage collected. Cannot clone.");
      int hr;
      FaceTrackFrame result = target.CreateResult(out hr);
      if (result == null || hr != 0)
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Failed to create face tracking frame. Error code from native=0x{0:X}", (object) hr));
      int num = this.faceTrackingResultPtr.CopyTo(result.ResultPtr);
      if (num != 0)
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Failed to clone the source face tracking frame. Error code from native=0x{0:X}", (object) num));
      return (object) result;
    }

    public void Dispose()
    {
      this.InternalDispose();
      GC.SuppressFinalize((object) this);
    }

    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Analysis doesn't see these as arrays.  If this returned an actual array, we wouldn't see this warning.")]
    public EnumIndexableCollection<FeaturePoint, Vector3DF> Get3DShape()
    {
      if (!(this.parentFaceTracker.Target is FaceTracker target))
        throw new ObjectDisposedException("FaceTracker", "Underlying face object has been garbage collected. Cannot copy.");
      return new EnumIndexableCollection<FeaturePoint, Vector3DF>(target.FaceModel.Get3DShape(this));
    }

    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Analysis doesn't see these as arrays.  If this returned an actual array, we wouldn't see this warning.")]
    public EnumIndexableCollection<AnimationUnit, float> GetAnimationUnitCoefficients()
    {
      this.CheckPtrAndThrow();
      IntPtr animUnitCoeffPtr;
      uint animUnitCount;
      this.faceTrackingResultPtr.GetAUCoefficients(out animUnitCoeffPtr, out animUnitCount);
      float[] numArray = (float[]) null;
      if (animUnitCount > 0U)
      {
        numArray = new float[(int) animUnitCount];
        Marshal.Copy(animUnitCoeffPtr, numArray, 0, numArray.Length);
      }
      return new EnumIndexableCollection<AnimationUnit, float>(numArray);
    }

    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Analysis doesn't see these as arrays.  If this returned an actual array, we wouldn't see this warning.")]
    public EnumIndexableCollection<FeaturePoint, PointF> GetProjected3DShape()
    {
      if (!(this.parentFaceTracker.Target is FaceTracker target))
        throw new ObjectDisposedException("FaceTracker", "Underlying face object has been garbage collected. Cannot copy.");
      return new EnumIndexableCollection<FeaturePoint, PointF>(target.FaceModel.GetProjected3DShape(1f, Point.Empty, this));
    }

    public FaceTriangle[] GetTriangles()
    {
      if (!(this.parentFaceTracker.Target is FaceTracker target))
        throw new ObjectDisposedException("FaceTracker", "Underlying face object has been garbage collected. Cannot copy.");
      return target.FaceModel.GetTriangles();
    }

    private void CheckPtrAndThrow()
    {
      if (this.faceTrackingResultPtr == null)
        throw new InvalidOperationException("Native frame pointer in invalid state.");
    }

    private void InternalDispose()
    {
      if (this.disposed)
        return;
      if (this.faceTrackingResultPtr != null)
      {
        Marshal.FinalReleaseComObject((object) this.faceTrackingResultPtr);
        this.faceTrackingResultPtr = (IFTResult) null;
      }
      this.parentFaceTracker = (WeakReference) null;
      this.disposed = true;
    }
  }
}
