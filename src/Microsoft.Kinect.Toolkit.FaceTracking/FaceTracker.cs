// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.FaceTracking.FaceTracker
// Assembly: Microsoft.Kinect.Toolkit.FaceTracking, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1A78CF7A-6101-44D2-89EE-184B8BDF2A78
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.FaceTracking.dll

using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Toolkit.FaceTracking
{
  public class FaceTracker : IDisposable
  {
    internal const float DefaultZoomFactor = 1f;
    private const string FaceTrackTraceSwitchName = "KinectForWindowsFaceTracking";
    private const string TraceCategory = "FTR";
    private const string TraceLogFileName = "TraceLogFile";
    private readonly Stopwatch copyStopwatch = new Stopwatch();
    private readonly ColorImageFormat initializationColorImageFormat;
    private readonly DepthImageFormat initializationDepthImageFormat;
    private readonly OperationMode operationMode;
    private readonly KinectSensor sensor;
    private readonly Stopwatch startOrContinueTrackingStopwatch = new Stopwatch();
    private readonly Stopwatch trackStopwatch = new Stopwatch();
    private Image colorFaceTrackingImage;
    private CameraConfig depthCameraConfig;
    private Image depthFaceTrackingImage;
    private bool disposed;
    private FaceModel faceModel;
    private IFTFaceTracker faceTrackerInteropPtr;
    private FaceTrackFrame frame;
    private long lastSuccessTrackElapsedMs;
    private FaceTrackingRegisterDepthToColor registerDepthToColorDelegate;
    private long totalSuccessTrackMs;
    private int totalSuccessTracks;
    private int totalTracks;
    private TraceLevel traceLevel;
    private bool trackSucceeded;
    private CameraConfig videoCameraConfig;

    static FaceTracker()
    {
      try
      {
        string appSetting = ConfigurationManager.AppSettings["TraceLogFile"];
        if (string.IsNullOrEmpty(appSetting))
          return;
        foreach (TraceListener listener in Trace.Listeners)
        {
          if (listener is DefaultTraceListener defaultTraceListener)
          {
            defaultTraceListener.LogFileName = appSetting;
            break;
          }
        }
        DateTime now = DateTime.Now;
        Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "---------------------------------------------------------------------------"));
        Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Starting Trace. Time={0} {1}, Machine={2}, Processor={3}, OS={4}", (object) now.ToShortDateString(), (object) now.ToLongTimeString(), (object) Environment.MachineName, Environment.Is64BitProcess ? (object) "64bit" : (object) "32bit", (object) Environment.OSVersion));
        Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "---------------------------------------------------------------------------"));
      }
      catch (Exception ex)
      {
        Trace.WriteLine(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Failed to set logfile for logging trace output. Exception={0}", (object) ex));
        throw;
      }
    }

    public FaceTracker(KinectSensor sensor)
    {
      if (sensor == null)
        throw new ArgumentNullException(nameof (sensor));
      if (!sensor.ColorStream.IsEnabled)
        throw new InvalidOperationException("Color stream is not enabled yet.");
      if (!sensor.DepthStream.IsEnabled)
        throw new InvalidOperationException("Depth stream is not enabled yet.");
      this.operationMode = OperationMode.Kinect;
      this.sensor = sensor;
      this.initializationColorImageFormat = sensor.ColorStream.Format;
      this.initializationDepthImageFormat = sensor.DepthStream.Format;
      this.Initialize(new CameraConfig((uint) sensor.ColorStream.FrameWidth, (uint) sensor.ColorStream.FrameHeight, sensor.ColorStream.NominalFocalLengthInPixels, FaceTrackingImageFormat.FTIMAGEFORMAT_UINT8_B8G8R8X8), new CameraConfig((uint) sensor.DepthStream.FrameWidth, (uint) sensor.DepthStream.FrameHeight, sensor.DepthStream.NominalFocalLengthInPixels, FaceTrackingImageFormat.FTIMAGEFORMAT_UINT16_D13P3), IntPtr.Zero, IntPtr.Zero, new FaceTrackingRegisterDepthToColor(this.DepthToColorCallback));
    }

    ~FaceTracker() => this.Dispose(false);

    internal CameraConfig ColorCameraConfig => this.videoCameraConfig;

    internal FaceModel FaceModel
    {
      get
      {
        this.CheckPtrAndThrow();
        if (this.faceModel == null)
        {
          IFTModel model;
          this.faceTrackerInteropPtr.GetFaceModel(out model);
          this.faceModel = new FaceModel(this, model);
        }
        return this.faceModel;
      }
    }

    internal IFTFaceTracker FaceTrackerPtr => this.faceTrackerInteropPtr;

    internal Stopwatch Stopwatch => this.trackStopwatch;

    internal int TotalTracks => this.totalTracks;

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    public void ResetTracking()
    {
      this.CheckPtrAndThrow();
      this.trackSucceeded = false;
      this.faceTrackerInteropPtr.Reset();
    }

    public FaceTrackFrame Track(
      ColorImageFormat colorImageFormat,
      byte[] colorImage,
      DepthImageFormat depthImageFormat,
      short[] depthImage,
      Skeleton skeletonOfInterest)
    {
      return this.Track(colorImageFormat, colorImage, depthImageFormat, depthImage, skeletonOfInterest, Rect.Empty);
    }

    public FaceTrackFrame Track(
      ColorImageFormat colorImageFormat,
      byte[] colorImage,
      DepthImageFormat depthImageFormat,
      short[] depthImage,
      Rect regionOfInterest)
    {
      return this.Track(colorImageFormat, colorImage, depthImageFormat, depthImage, (Skeleton) null, regionOfInterest);
    }

    public FaceTrackFrame Track(
      ColorImageFormat colorImageFormat,
      byte[] colorImage,
      DepthImageFormat depthImageFormat,
      short[] depthImage)
    {
      return this.Track(colorImageFormat, colorImage, depthImageFormat, depthImage, (Skeleton) null, Rect.Empty);
    }

    internal FaceTrackFrame CreateResult(out int hr)
    {
      FaceTrackFrame result = (FaceTrackFrame) null;
      this.CheckPtrAndThrow();
      IFTResult faceTrackResult;
      hr = this.faceTrackerInteropPtr.CreateFTResult(out faceTrackResult);
      if (faceTrackResult != null)
        result = new FaceTrackFrame(faceTrackResult, this);
      return result;
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposed)
        return;
      Trace.WriteLineIf(this.traceLevel >= TraceLevel.Info, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "FaceTracker::Dispose() - TotalTracks={0}, TotalSuccessTracks={1}, TimePerTrack={2:F3}ms, TimePerSuccessTrack={3:F3}ms, TimePerDataCopy={4:F3}ms, TimePerStartOrContinueTracking={5:F3}ms", (object) this.totalTracks, (object) this.totalSuccessTracks, (object) (this.totalTracks > 0 ? (double) this.trackStopwatch.ElapsedMilliseconds / (double) this.totalTracks : 0.0), (object) (this.totalSuccessTracks > 0 ? (double) this.totalSuccessTrackMs / (double) this.totalSuccessTracks : 0.0), (object) (this.totalTracks > 0 ? (double) this.copyStopwatch.ElapsedMilliseconds / (double) this.totalTracks : 0.0), (object) (this.totalTracks > 0 ? (double) this.startOrContinueTrackingStopwatch.ElapsedMilliseconds / (double) this.totalTracks : 0.0)));
      if (this.faceModel != null)
      {
        this.faceModel.Dispose();
        this.faceModel = (FaceModel) null;
      }
      if (this.frame != null)
      {
        this.frame.Dispose();
        this.frame = (FaceTrackFrame) null;
      }
      if (this.colorFaceTrackingImage != null)
      {
        this.colorFaceTrackingImage.Dispose();
        this.colorFaceTrackingImage = (Image) null;
      }
      if (this.depthFaceTrackingImage != null)
      {
        this.depthFaceTrackingImage.Dispose();
        this.depthFaceTrackingImage = (Image) null;
      }
      if (this.faceTrackerInteropPtr != null)
      {
        Marshal.FinalReleaseComObject((object) this.faceTrackerInteropPtr);
        this.faceTrackerInteropPtr = (IFTFaceTracker) null;
      }
      this.disposed = true;
    }

    private static Vector3DF[] GetHeadPointsFromSkeleton(Skeleton skeletonOfInterest)
    {
      Vector3DF[] pointsFromSkeleton = (Vector3DF[]) null;
      if (skeletonOfInterest != null && skeletonOfInterest.TrackingState == SkeletonTrackingState.Tracked)
      {
        pointsFromSkeleton = new Vector3DF[2];
        SkeletonPoint position1 = skeletonOfInterest.Joints[JointType.ShoulderCenter].Position;
        pointsFromSkeleton[0] = new Vector3DF(position1.X, position1.Y, position1.Z);
        SkeletonPoint position2 = skeletonOfInterest.Joints[JointType.Head].Position;
        pointsFromSkeleton[1] = new Vector3DF(position2.X, position2.Y, position2.Z);
      }
      return pointsFromSkeleton;
    }

    private void CheckPtrAndThrow()
    {
      if (this.faceTrackerInteropPtr == null)
        throw new InvalidOperationException("Native face tracker pointer in invalid state.");
    }

    private int DepthToColorCallback(
      uint depthFrameWidth,
      uint depthFrameHeight,
      uint colorFrameWidth,
      uint colorFrameHeight,
      float zoomFactor,
      Point viewOffset,
      int depthX,
      int depthY,
      ushort depthZ,
      out int colorX,
      out int colorY)
    {
      int colorCallback = 0;
      colorX = 0;
      colorY = 0;
      if (this.sensor != null)
      {
        ColorImagePoint colorImagePoint = new ColorImagePoint();
        try
        {
          colorImagePoint = this.sensor.CoordinateMapper.MapDepthPointToColorPoint(this.sensor.DepthStream.Format, new DepthImagePoint()
          {
            X = depthX,
            Y = depthY,
            Depth = (int) depthZ
          }, this.sensor.ColorStream.Format);
        }
        catch (InvalidOperationException ex)
        {
          Trace.WriteLineIf(this.traceLevel >= TraceLevel.Error, string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Exception on MapDepthToColorImagePoint while translating depth point({0},{1},{2}). Exception={3}", (object) depthX, (object) depthY, (object) depthZ, (object) ex.Message), "FTR");
          colorCallback = -1;
        }
        colorX = colorImagePoint.X;
        colorY = colorImagePoint.Y;
      }
      else
        colorCallback = -1;
      return colorCallback;
    }

    private void Initialize(
      CameraConfig newColorCameraConfig,
      CameraConfig newDepthCameraConfig,
      IntPtr colorImagePtr,
      IntPtr depthImagePtr,
      FaceTrackingRegisterDepthToColor newRegisterDepthToColorDelegate)
    {
      if (newColorCameraConfig == null)
        throw new ArgumentNullException(nameof (newColorCameraConfig));
      if (newDepthCameraConfig == null)
        throw new ArgumentNullException(nameof (newDepthCameraConfig));
      if (newRegisterDepthToColorDelegate == null)
        throw new ArgumentNullException(nameof (newRegisterDepthToColorDelegate));
      this.totalTracks = 0;
      this.trackStopwatch.Reset();
      this.traceLevel = new TraceSwitch("KinectForWindowsFaceTracking", "KinectForWindowsFaceTracking").Level;
      this.videoCameraConfig = newColorCameraConfig;
      this.depthCameraConfig = newDepthCameraConfig;
      this.registerDepthToColorDelegate = newRegisterDepthToColorDelegate;
      this.faceTrackerInteropPtr = NativeMethods.FTCreateFaceTracker(IntPtr.Zero);
      if (this.faceTrackerInteropPtr == null)
        throw new InsufficientMemoryException("Cannot create face tracker.");
      IntPtr pointerForDelegate = Marshal.GetFunctionPointerForDelegate<FaceTrackingRegisterDepthToColor>(this.registerDepthToColorDelegate);
      if (pointerForDelegate == IntPtr.Zero)
        throw new InsufficientMemoryException("Cannot setup callback for retrieving color to depth pixel mapping");
      int hr = this.faceTrackerInteropPtr.Initialize(this.videoCameraConfig, this.depthCameraConfig, pointerForDelegate, (string) null);
      this.frame = hr == 0 ? this.CreateResult(out hr) : throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Failed to initialize face tracker - Error code from native=0x{0:X}", (object) hr));
      if (this.frame == null || hr != 0)
        throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, "Failed to create face tracking result. Error code from native=0x{0:X}", (object) hr));
      this.colorFaceTrackingImage = new Image();
      if (colorImagePtr == IntPtr.Zero)
        this.colorFaceTrackingImage.Allocate(this.videoCameraConfig.Width, this.videoCameraConfig.Height, this.videoCameraConfig.ImageFormat);
      else
        this.colorFaceTrackingImage.Attach(this.videoCameraConfig.Width, this.videoCameraConfig.Height, colorImagePtr, this.videoCameraConfig.ImageFormat, this.videoCameraConfig.Stride);
      this.depthFaceTrackingImage = new Image();
      if (depthImagePtr == IntPtr.Zero)
        this.depthFaceTrackingImage.Allocate(this.depthCameraConfig.Width, this.depthCameraConfig.Height, this.depthCameraConfig.ImageFormat);
      else
        this.depthFaceTrackingImage.Attach(this.depthCameraConfig.Width, this.depthCameraConfig.Height, depthImagePtr, this.depthCameraConfig.ImageFormat, this.depthCameraConfig.Stride);
    }

    private FaceTrackFrame Track(
      ColorImageFormat colorImageFormat,
      byte[] colorImage,
      DepthImageFormat depthImageFormat,
      short[] depthImage,
      Skeleton skeletonOfInterest,
      Rect regionOfInterest)
    {
      ++this.totalTracks;
      this.trackStopwatch.Start();
      if (this.operationMode != OperationMode.Kinect)
        throw new InvalidOperationException("Cannot use Track with Kinect input types when face tracker is initialized for tracking videos/images");
      if (colorImage == null)
        throw new ArgumentNullException(nameof (colorImage));
      if (depthImage == null)
        throw new ArgumentNullException(nameof (depthImage));
      if (colorImageFormat != this.initializationColorImageFormat)
        throw new InvalidOperationException("Color image frame format different from initialization");
      if (depthImageFormat != this.initializationDepthImageFormat)
        throw new InvalidOperationException("Depth image frame format different from initialization");
      if ((long) colorImage.Length != (long) this.videoCameraConfig.FrameBufferLength)
        throw new ArgumentOutOfRangeException(nameof (colorImage), "Color image data size is needs to match initialization configuration.");
      if ((long) depthImage.Length != (long) this.depthCameraConfig.FrameBufferLength)
        throw new ArgumentOutOfRangeException(nameof (depthImage), "Depth image data size is needs to match initialization configuration.");
      HeadPoints headPoints = (HeadPoints) null;
      Vector3DF[] pointsFromSkeleton = FaceTracker.GetHeadPointsFromSkeleton(skeletonOfInterest);
      if (pointsFromSkeleton != null && pointsFromSkeleton.Length == 2)
        headPoints = new HeadPoints()
        {
          Points = pointsFromSkeleton
        };
      this.copyStopwatch.Start();
      this.colorFaceTrackingImage.CopyFrom<byte>(colorImage);
      this.depthFaceTrackingImage.CopyFrom<short>(depthImage);
      this.copyStopwatch.Stop();
      FaceTrackingSensorData trackingSensorData = new SensorData(this.colorFaceTrackingImage, this.depthFaceTrackingImage, 1f, Point.Empty).FaceTrackingSensorData;
      this.startOrContinueTrackingStopwatch.Start();
      int num = !this.trackSucceeded ? this.faceTrackerInteropPtr.StartTracking(ref trackingSensorData, ref regionOfInterest, headPoints, this.frame.ResultPtr) : this.faceTrackerInteropPtr.ContinueTracking(ref trackingSensorData, headPoints, this.frame.ResultPtr);
      this.startOrContinueTrackingStopwatch.Stop();
      this.trackSucceeded = num == 0 && this.frame.Status == ErrorCode.Success;
      this.trackStopwatch.Stop();
      if (this.trackSucceeded)
      {
        ++this.totalSuccessTracks;
        this.totalSuccessTrackMs += this.trackStopwatch.ElapsedMilliseconds - this.lastSuccessTrackElapsedMs;
        this.lastSuccessTrackElapsedMs = this.trackStopwatch.ElapsedMilliseconds;
      }
      return this.frame;
    }
  }
}
