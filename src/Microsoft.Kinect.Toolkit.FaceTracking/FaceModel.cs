// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.FaceModel
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  internal class FaceModel : IDisposable
  {
    private readonly FaceTracker faceTracker;
    private bool disposed;
    private IFTModel faceTrackingModelPtr;

    internal FaceModel(FaceTracker faceTracker, IFTModel faceModelPtr)
    {
      this.faceTrackingModelPtr = faceTracker != null && faceModelPtr != null ? faceModelPtr : throw new InvalidOperationException("Cannot associate face model with null face tracker or native face model reference");
      this.faceTracker = faceTracker;
    }

    private FaceModel()
    {
    }

    ~FaceModel() => this.Dispose(false);

    public uint VertexCount
    {
      get
      {
        this.CheckPtrAndThrow();
        return this.faceTrackingModelPtr.GetVertexCount();
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public Vector3DF[] Get3DShape(FaceTrackFrame faceTrackFrame)
    {
      uint shapeUnitCount = 0;
      IntPtr animUnitCoeffPtr;
      uint animUnitCount;
      faceTrackFrame.ResultPtr.GetAUCoefficients(out animUnitCoeffPtr, out animUnitCount);
      IntPtr shapeUnitCoeffsPtr;
      this.faceTracker.FaceTrackerPtr.GetShapeUnits(out float _, out shapeUnitCoeffsPtr, ref shapeUnitCount, out bool _);
      return this.Get3DShape(shapeUnitCoeffsPtr, shapeUnitCount, animUnitCoeffPtr, animUnitCount, faceTrackFrame.Scale, faceTrackFrame.Rotation, faceTrackFrame.Translation);
    }

    public PointF[] GetProjected3DShape(
      float zoomFactor,
      Point viewOffset,
      FaceTrackFrame faceTrackFrame)
    {
      this.CheckPtrAndThrow();
      uint shapeUnitCount = 0;
      IntPtr animUnitCoeffPtr;
      uint animUnitCount;
      faceTrackFrame.ResultPtr.GetAUCoefficients(out animUnitCoeffPtr, out animUnitCount);
      IntPtr shapeUnitCoeffsPtr;
      this.faceTracker.FaceTrackerPtr.GetShapeUnits(out float _, out shapeUnitCoeffsPtr, ref shapeUnitCount, out bool _);
      return this.GetProjected3DShape(this.faceTracker.ColorCameraConfig, zoomFactor, viewOffset, shapeUnitCoeffsPtr, shapeUnitCount, animUnitCoeffPtr, animUnitCount, faceTrackFrame.Scale, faceTrackFrame.Rotation, faceTrackFrame.Translation);
    }

    public FaceTriangle[] GetTriangles()
    {
      this.CheckPtrAndThrow();
      IntPtr trianglesPtr;
      uint triangleCount;
      this.faceTrackingModelPtr.GetTriangles(out trianglesPtr, out triangleCount);
      FaceTriangle[] triangles = (FaceTriangle[]) null;
      if (triangleCount > 0U)
      {
        triangles = new FaceTriangle[(int) triangleCount];
        for (int index = 0; (long) index < (long) triangleCount; ++index)
        {
          triangles[index] = new FaceTriangle();
          IntPtr ptr = IntPtr.Size != 8 ? new IntPtr(trianglesPtr.ToInt32() + index * Marshal.SizeOf(typeof (FaceTriangle))) : new IntPtr(trianglesPtr.ToInt64() + (long) (index * Marshal.SizeOf(typeof (FaceTriangle))));
          triangles[index] = (FaceTriangle) Marshal.PtrToStructure(ptr, typeof (FaceTriangle));
        }
      }
      return triangles;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
        return;
      if (this.faceTrackingModelPtr != null)
      {
        Marshal.FinalReleaseComObject((object) this.faceTrackingModelPtr);
        this.faceTrackingModelPtr = (IFTModel) null;
      }
      this.disposed = true;
    }

    private void CheckPtrAndThrow()
    {
      if (this.faceTrackingModelPtr == null)
        throw new InvalidOperationException("Native face model pointer in invalid state.");
    }

    private Vector3DF[] Get3DShape(
      IntPtr shapeUnitCoeffPtr,
      uint shapeUnitCoeffCount,
      IntPtr animUnitCoeffPtr,
      uint animUnitCoeffCount,
      float scale,
      Vector3DF rotation,
      Vector3DF translation)
    {
      this.CheckPtrAndThrow();
      Vector3DF[] vector3DfArray = (Vector3DF[]) null;
      uint vertexCount = this.VertexCount;
      IntPtr num = IntPtr.Zero;
      if (shapeUnitCoeffPtr == IntPtr.Zero || shapeUnitCoeffCount == 0U)
        throw new ArgumentException("Invalid shape unit co-efficients", nameof (shapeUnitCoeffPtr));
      if (animUnitCoeffPtr == IntPtr.Zero || animUnitCoeffCount == 0U)
        throw new ArgumentException("Invalid animation unit co-efficients", nameof (animUnitCoeffPtr));
      if (vertexCount > 0U)
      {
        try
        {
          num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (Vector3DF)) * (int) vertexCount);
          this.faceTrackingModelPtr.Get3DShape(shapeUnitCoeffPtr, shapeUnitCoeffCount, animUnitCoeffPtr, animUnitCoeffCount, scale, ref rotation, ref translation, num, vertexCount);
          vector3DfArray = new Vector3DF[(int) vertexCount];
          for (int index = 0; index < (int) vertexCount; ++index)
          {
            IntPtr ptr = IntPtr.Size != 8 ? new IntPtr(num.ToInt32() + index * Marshal.SizeOf(typeof (Vector3DF))) : new IntPtr(num.ToInt64() + (long) (index * Marshal.SizeOf(typeof (Vector3DF))));
            vector3DfArray[index] = (Vector3DF) Marshal.PtrToStructure(ptr, typeof (Vector3DF));
          }
        }
        finally
        {
          if (num != IntPtr.Zero)
            Marshal.FreeHGlobal(num);
        }
      }
      return vector3DfArray;
    }

    private PointF[] GetProjected3DShape(
      CameraConfig videoCameraConfig,
      float zoomFactor,
      Point viewOffset,
      IntPtr shapeUnitCoeffPtr,
      uint shapeUnitCoeffCount,
      IntPtr animUnitCoeffPtr,
      uint animUnitCoeffCount,
      float scale,
      Vector3DF rotation,
      Vector3DF translation)
    {
      this.CheckPtrAndThrow();
      PointF[] projected3Dshape = (PointF[]) null;
      uint vertexCount = this.VertexCount;
      IntPtr num = IntPtr.Zero;
      if (shapeUnitCoeffPtr == IntPtr.Zero || shapeUnitCoeffCount == 0U)
        throw new ArgumentException("Invalid shape unit co-efficients", nameof (shapeUnitCoeffPtr));
      if (animUnitCoeffPtr == IntPtr.Zero || animUnitCoeffCount == 0U)
        throw new ArgumentException("Invalid animation unit co-efficients", nameof (animUnitCoeffPtr));
      if (vertexCount > 0U)
      {
        try
        {
          num = Marshal.AllocHGlobal(Marshal.SizeOf(typeof (Vector3DF)) * (int) vertexCount);
          this.faceTrackingModelPtr.GetProjectedShape(videoCameraConfig, zoomFactor, viewOffset, shapeUnitCoeffPtr, shapeUnitCoeffCount, animUnitCoeffPtr, animUnitCoeffCount, scale, ref rotation, ref translation, num, vertexCount);
          projected3Dshape = new PointF[(int) vertexCount];
          for (int index = 0; index < (int) vertexCount; ++index)
          {
            IntPtr ptr = IntPtr.Size != 8 ? new IntPtr(num.ToInt32() + index * Marshal.SizeOf(typeof (PointF))) : new IntPtr(num.ToInt64() + (long) (index * Marshal.SizeOf(typeof (PointF))));
            projected3Dshape[index] = (PointF) Marshal.PtrToStructure(ptr, typeof (PointF));
          }
        }
        finally
        {
          if (num != IntPtr.Zero)
            Marshal.FreeHGlobal(num);
        }
      }
      return projected3Dshape;
    }
  }
}
