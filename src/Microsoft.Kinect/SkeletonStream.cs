// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.SkeletonStream
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;
using System.Threading;

namespace Microsoft.Kinect
{
  public sealed class SkeletonStream
  {
    private const uint NUI_SKELETON_FRAME_FLAG_SEATED_SUPPORT_ENABLED = 8;
    private const uint NUI_SKELETON_TRACKING_FLAG_SUPPRESS_NO_FRAME_DATA = 1;
    private const uint NUI_SKELETON_TRACKING_FLAG_TITLE_SETS_TRACKED_SKELETONS = 2;
    private const uint NUI_SKELETON_TRACKING_FLAG_ENABLE_SEATED_SUPPORT = 4;
    private const uint NUI_SKELETON_TRACKING_FLAG_ENABLE_IN_NEAR_RANGE = 8;
    internal const int s_skeletonCount = 6;
    private readonly KinectSensor _nuiSensor;
    private readonly DataPool<int, Tuple<float, float, float, float>, object, Skeleton[]> _skeletonDataPool;
    internal ManualResetEvent NextSkeletonEvent = new ManualResetEvent(false);
    private bool _isEnabled;
    private bool _appChoosesSkeletons;
    private SkeletonTrackingMode _trackingMode;
    private bool _enableTrackingInNearRange;
    private bool _isTracking;

    internal KinectSensor Sensor => this._nuiSensor;

    internal SkeletonStream(KinectSensor mainNui)
    {
      this._nuiSensor = mainNui;
      this._isEnabled = false;
      this._skeletonDataPool = new DataPool<int, Tuple<float, float, float, float>, object, Skeleton[]>(2);
    }

    public bool IsEnabled
    {
      get => this._isEnabled;
      private set
      {
        if (this._isEnabled == value)
          return;
        if (this._nuiSensor.IsRunning && !value)
          this.StopTracking();
        this._isEnabled = value;
      }
    }

    public bool AppChoosesSkeletons
    {
      get => this._appChoosesSkeletons;
      set
      {
        if (this._appChoosesSkeletons == value)
          return;
        this._appChoosesSkeletons = value;
        if (!this._isTracking)
          return;
        this.StartTracking();
      }
    }

    public SkeletonTrackingMode TrackingMode
    {
      get => this._trackingMode;
      set
      {
        if (this._trackingMode == value)
          return;
        this._trackingMode = value;
        if (!this._isTracking)
          return;
        this.StartTracking();
      }
    }

    public bool EnableTrackingInNearRange
    {
      get => this._enableTrackingInNearRange;
      set
      {
        if (this._enableTrackingInNearRange == value)
          return;
        this._enableTrackingInNearRange = value;
        if (!this._isTracking)
          return;
        this.StartTracking();
      }
    }

    public void Enable()
    {
      this.SetSmoothingState(false, new TransformSmoothParameters());
      this.SetEnabledState(true);
    }

    public void Enable(TransformSmoothParameters smoothParameters)
    {
      if ((double) smoothParameters.Smoothing < 0.0 || (double) smoothParameters.Smoothing >= 1.0 || (double) smoothParameters.Correction < 0.0 || (double) smoothParameters.Correction > 1.0 || (double) smoothParameters.Prediction < 0.0 || (double) smoothParameters.JitterRadius < 0.0 || (double) smoothParameters.MaxDeviationRadius < 0.0)
        throw new ArgumentOutOfRangeException(nameof (smoothParameters));
      if (this.IsEnabled && smoothParameters != this.SmoothParameters)
        this.SetEnabledState(false);
      this.SetSmoothingState(true, smoothParameters);
      this.SetEnabledState(true);
    }

    public void Disable()
    {
      this.SetSmoothingState(false, new TransformSmoothParameters());
      this.SetEnabledState(false);
    }

    private void SetEnabledState(bool enable)
    {
      this.IsEnabled = enable;
      this._nuiSensor.RequestSkeletonEngine(enable);
    }

    private void SetSmoothingState(bool enable, TransformSmoothParameters smoothParameters)
    {
      this.IsSmoothingEnabled = enable;
      this.SmoothParameters = smoothParameters;
    }

    public void ChooseSkeletons() => this.ChooseSkeletons(0, 0);

    public void ChooseSkeletons(int trackingId1) => this.ChooseSkeletons(trackingId1, 0);

    public void ChooseSkeletons(int trackingId1, int trackingId2) => this._nuiSensor.NuiSensor.NuiSkeletonSetTrackedSkeletons((uint) trackingId1, (uint) trackingId2);

    private void StartTracking()
    {
      uint dwFlags = 1;
      if (this._trackingMode == SkeletonTrackingMode.Seated)
        dwFlags |= 4U;
      if (this._enableTrackingInNearRange)
        dwFlags |= 8U;
      if (this._appChoosesSkeletons)
        dwFlags |= 2U;
      this._nuiSensor.NuiSensor.NuiSkeletonTrackingEnable(this.NextSkeletonEvent.SafeWaitHandle.DangerousGetHandle(), dwFlags);
      this._isTracking = true;
    }

    private void StopTracking()
    {
      this._nuiSensor.NuiSensor.NuiSkeletonTrackingDisableNoThrow();
      this._isTracking = false;
    }

    internal void Start()
    {
      if (!this._isEnabled)
        return;
      this.StartTracking();
    }

    internal void Stop()
    {
      if (!this._isTracking)
        return;
      this.StopTracking();
    }

    public TransformSmoothParameters SmoothParameters { get; private set; }

    public bool IsSmoothingEnabled { get; private set; }

    public int FrameSkeletonArrayLength => 6;

    public SkeletonFrame OpenNextFrame(int millisecondsWait)
    {
      SkeletonFrame skeletonFrame = (SkeletonFrame) null;
      if (!this._nuiSensor.IsRunning)
        throw new InvalidOperationException(Resources.SensorMustBeRunning);
      if (!this._isEnabled)
        throw new InvalidOperationException(Resources.SkeletonEngineMustBeEnabled);
      if (this._nuiSensor.HasSkeletonInvocations)
        throw new InvalidOperationException(Resources.CannotPollAndUseEvents);
      KinectEtwProvider.EventWriteManagedOpenNextFrameInfo(2, millisecondsWait);
      int frameNumber;
      long timestamp;
      SkeletonTrackingMode trackingMode;
      if (this.TryGetNextFrameInternal(millisecondsWait, out frameNumber, out timestamp, out trackingMode))
        skeletonFrame = SkeletonFrame.Create(this, frameNumber, timestamp, trackingMode);
      return skeletonFrame;
    }

    internal void StoreSkeletonData(
      int frameNumber,
      _NUI_SKELETON_FRAME skeletonData,
      Tuple<float, float, float, float> floorClipPlane)
    {
      DataPool<int, Tuple<float, float, float, float>, object, Skeleton[], bool?>.Entry entry = this._skeletonDataPool.CheckOutFreeEntryForUpdate();
      entry.Key = frameNumber;
      entry.Value1 = floorClipPlane;
      if (entry.Value3 == null || entry.Value3.Length != skeletonData.SkeletonData.Length)
        entry.Value3 = new Skeleton[skeletonData.SkeletonData.Length];
      for (int index = 0; index < entry.Value3.Length; ++index)
      {
        if (entry.Value3[index] == null)
          entry.Value3[index] = new Skeleton(skeletonData.SkeletonData[index]);
        else
          entry.Value3[index].CopyFrom(skeletonData.SkeletonData[index]);
      }
      this._skeletonDataPool.CheckInEntryForUpdate(entry);
    }

    internal bool TryGetNextFrameInternal(
      int millisecondsWait,
      out int frameNumber,
      out long timestamp,
      out SkeletonTrackingMode trackingMode)
    {
      frameNumber = 0;
      timestamp = 0L;
      trackingMode = SkeletonTrackingMode.Default;
      _NUI_SKELETON_FRAME pSkeletonFrame = new _NUI_SKELETON_FRAME();
      int nextFrameNoThrow = this._nuiSensor.NuiSensor.NuiSkeletonGetNextFrameNoThrow((uint) millisecondsWait, ref pSkeletonFrame);
      if (nextFrameNoThrow < 0)
      {
        this.NextSkeletonEvent.Reset();
        return false;
      }
      if (nextFrameNoThrow != 0)
        return false;
      if (this.IsSmoothingEnabled)
      {
        _NUI_TRANSFORM_SMOOTH_PARAMETERS pSmoothingParams = new _NUI_TRANSFORM_SMOOTH_PARAMETERS()
        {
          fSmoothing = this.SmoothParameters.Smoothing,
          fCorrection = this.SmoothParameters.Correction,
          fPrediction = this.SmoothParameters.Prediction,
          fJitterRadius = this.SmoothParameters.JitterRadius,
          fMaxDeviationRadius = this.SmoothParameters.MaxDeviationRadius
        };
        this._nuiSensor.NuiSensor.NuiTransformSmooth(ref pSkeletonFrame, ref pSmoothingParams);
      }
      frameNumber = (int) pSkeletonFrame.dwFrameNumber;
      timestamp = pSkeletonFrame.liTimeStamp.QuadPart;
      if (((int) pSkeletonFrame.dwFlags & 8) != 0)
        trackingMode = SkeletonTrackingMode.Seated;
      Tuple<float, float, float, float> floorClipPlane = new Tuple<float, float, float, float>(pSkeletonFrame.vFloorClipPlane.x, pSkeletonFrame.vFloorClipPlane.y, pSkeletonFrame.vFloorClipPlane.z, pSkeletonFrame.vFloorClipPlane.w);
      this.StoreSkeletonData(frameNumber, pSkeletonFrame, floorClipPlane);
      return true;
    }

    internal DataPool<int, Tuple<float, float, float, float>, object, Skeleton[], bool?>.Entry LockSkeletonData(
      int frameNumber)
    {
      DataPool<int, Tuple<float, float, float, float>, object, Skeleton[], bool?>.Entry entry;
      this._skeletonDataPool.TryLockEntry(frameNumber, out entry);
      return entry;
    }

    internal void UnlockSkeletonData(
      DataPool<int, Tuple<float, float, float, float>, object, Skeleton[], bool?>.Entry entry)
    {
      this._skeletonDataPool.UnlockEntry(entry);
    }

    internal void Dispose()
    {
      if (this.NextSkeletonEvent == null)
        return;
      this.NextSkeletonEvent.Close();
    }
  }
}
