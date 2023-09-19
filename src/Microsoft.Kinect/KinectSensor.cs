// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.KinectSensor
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.Kinect
{
    public sealed class KinectSensor : IDisposable
    {
        private const int ColorHelperIndex = 0;
        private const int DepthHelperIndex = 1;
        private const int SkeletonHelperIndex = 2;
        private static ulong failureStackPointer = NativeMethods.NuiDebugGetFailureStack();
        private readonly ReaderWriterLockSlim _initLock = new ReaderWriterLockSlim();
        private readonly object _disposeLock = new object();
        private KinectAudioSource _audioSource;
        private static bool _unsupportedMessageShown = false;
        private KinectStatus _lastStatus = KinectStatus.NotReady;
        private readonly ContextEventHandler<DepthImageFrameReadyEventArgs> _depthFrameReadyContextHandler;
        private readonly ContextEventHandler<ColorImageFrameReadyEventArgs> _colorFrameReadyContextHandler;
        private readonly ContextEventHandler<SkeletonFrameReadyEventArgs> _skeletonFrameReadyContextHandler;
        private readonly ContextEventHandler<AllFramesReadyEventArgs> _allFramesReadyContextHandler;
        private CoordinateMapper _coordinateMapper;
        private ManualResetEvent _hKillThread;
        private readonly KinectSensor.FrameStateHelper[] _frameHelpers;
        private KinectSensor.SensorOptions _requestedOptions;
        private KinectSensor.SensorOptions _currentOptions;
        private Thread _thread;

        internal int InstanceIndex => this.NuiSensor != null ? this.NuiSensor.NuiInstanceIndex() : int.MinValue;

        public static KinectSensorCollection KinectSensors => KinectSensorCollection.Instance;

        internal NuiSensor NuiSensor { get; private set; }

        internal KinectSensor(INuiSensor nuiSensor)
        {
            this._allFramesReadyContextHandler = new ContextEventHandler<AllFramesReadyEventArgs>();
            this._depthFrameReadyContextHandler = new ContextEventHandler<DepthImageFrameReadyEventArgs>();
            this._colorFrameReadyContextHandler = new ContextEventHandler<ColorImageFrameReadyEventArgs>();
            this._skeletonFrameReadyContextHandler = new ContextEventHandler<SkeletonFrameReadyEventArgs>();
            this._frameHelpers = new KinectSensor.FrameStateHelper[3]
            {
        (KinectSensor.FrameStateHelper) new KinectSensor.FrameStateHelper<ColorImageFrameReadyEventArgs>(KinectSensor.FrameStateType.Color, this._colorFrameReadyContextHandler),
        (KinectSensor.FrameStateHelper) new KinectSensor.FrameStateHelper<DepthImageFrameReadyEventArgs>(KinectSensor.FrameStateType.Depth, this._depthFrameReadyContextHandler),
        (KinectSensor.FrameStateHelper) new KinectSensor.FrameStateHelper<SkeletonFrameReadyEventArgs>(KinectSensor.FrameStateType.Skeleton, this._skeletonFrameReadyContextHandler)
            };
            this.NuiSensor = new NuiSensor(nuiSensor);
            this._requestedOptions = KinectSensor.SensorOptions.None;
            this.LastStatus = this.GetStatus();
            if (KinectSensor._unsupportedMessageShown || this.NuiSensor.IsSupported)
                return;
            if (this.LastStatus == KinectStatus.DeviceNotSupported)
                Debugger.Log(0, "Warning", Resources.KinectNotSupportedNonDeveloper);
            else
                Debugger.Log(0, "Warning", Resources.KinectNotSupportedDeveloper);
            KinectSensor._unsupportedMessageShown = true;
        }

        internal void UpdateNuiSensor(INuiSensor nuiSensor) => this.NuiSensor = new NuiSensor(nuiSensor);

        private void CreateStreamObjects()
        {
            this.DepthStream = new DepthImageStream(this);
            this.ColorStream = new ColorImageStream(this);
            this.SkeletonStream = new SkeletonStream(this);
            this._requestedOptions = KinectSensor.SensorOptions.UseColor | KinectSensor.SensorOptions.UseDepth;
        }

        public string UniqueKinectId
        {
            get
            {
                using (this.EnterReadLock())
                    return this.GetUniqueKinectId();
            }
        }

        internal string GetUniqueKinectId()
        {
            if (this.LastStatus != KinectStatus.Connected)
                return (string)null;
            string str = this.NuiSensor.NuiUniqueId();
            return !string.IsNullOrEmpty(str) ? str : throw new InvalidOperationException(Resources.FailedToGetDeviceName);
        }

        public string DeviceConnectionId
        {
            get
            {
                using (this.EnterReadLock())
                    return this.GetDeviceConnectionId();
            }
        }

        internal string GetDeviceConnectionId() => this.NuiSensor.NuiDeviceConnectionId();

        public KinectStatus Status
        {
            get
            {
                using (this.EnterReadLock())
                    return this.LastStatus = this.GetStatus();
            }
        }

        internal KinectStatus GetStatus() => KinectSensor.MapStatus(this.NuiSensor.NuiStatus());

        internal static KinectStatus MapStatus(uint code)
        {
            switch (code)
            {
                case 0:
                    return KinectStatus.Connected;
                case 50397185:
                    return KinectStatus.Initializing;
                case 2147483648:
                    return KinectStatus.Error;
                case 2197880838:
                    return KinectStatus.DeviceNotGenuine;
                case 2197880839:
                    return KinectStatus.InsufficientBandwidth;
                case 2197880840:
                    return KinectStatus.DeviceNotSupported;
                case 2197880852:
                    return KinectStatus.Disconnected;
                case 2197880853:
                    return KinectStatus.NotReady;
                case 2197881471:
                    return KinectStatus.NotPowered;
                default:
                    return KinectStatus.Error;
            }
        }

        internal KinectStatus LastStatus
        {
            get => this._lastStatus;
            set
            {
                if (value == KinectStatus.Connected && this._requestedOptions == KinectSensor.SensorOptions.None)
                    this.CreateStreamObjects();
                this._lastStatus = value;
            }
        }

        internal bool WasRunning { get; set; }

        public event EventHandler<DepthImageFrameReadyEventArgs> DepthFrameReady
        {
            add
            {
                KinectEtwProvider.EventWriteManagedFrameReadyEventAddedInfo(1);
                this._depthFrameReadyContextHandler.AddHandler(value);
            }
            remove
            {
                KinectEtwProvider.EventWriteManagedFrameReadyEventRemovedInfo(1);
                this._depthFrameReadyContextHandler.RemoveHandler(value);
            }
        }

        public event EventHandler<ColorImageFrameReadyEventArgs> ColorFrameReady
        {
            add
            {
                KinectEtwProvider.EventWriteManagedFrameReadyEventAddedInfo(0);
                this._colorFrameReadyContextHandler.AddHandler(value);
            }
            remove
            {
                KinectEtwProvider.EventWriteManagedFrameReadyEventRemovedInfo(0);
                this._colorFrameReadyContextHandler.RemoveHandler(value);
            }
        }

        public event EventHandler<SkeletonFrameReadyEventArgs> SkeletonFrameReady
        {
            add
            {
                KinectEtwProvider.EventWriteManagedFrameReadyEventAddedInfo(2);
                this._skeletonFrameReadyContextHandler.AddHandler(value);
            }
            remove
            {
                KinectEtwProvider.EventWriteManagedFrameReadyEventRemovedInfo(2);
                this._skeletonFrameReadyContextHandler.RemoveHandler(value);
            }
        }

        public event EventHandler<AllFramesReadyEventArgs> AllFramesReady
        {
            add
            {
                KinectEtwProvider.EventWriteManagedAllFramesReadyEventAddedInfo();
                this._allFramesReadyContextHandler.AddHandler(value);
            }
            remove
            {
                KinectEtwProvider.EventWriteManagedAllFramesReadyEventRemovedInfo();
                this._allFramesReadyContextHandler.RemoveHandler(value);
            }
        }

        internal bool HasDepthInvocations => this._depthFrameReadyContextHandler.HasHandlers;

        internal bool HasColorInvocations => this._colorFrameReadyContextHandler.HasHandlers;

        internal bool HasSkeletonInvocations => this._skeletonFrameReadyContextHandler.HasHandlers;

        internal bool HasAllFramesInvocations => this._allFramesReadyContextHandler.HasHandlers;

        public CoordinateMapper CoordinateMapper => this._coordinateMapper ?? (this._coordinateMapper = new CoordinateMapper(this));

        public static bool IsKnownPoint(DepthImagePixel depthImagePixel) => depthImagePixel.Depth != (short)0;

        public static bool IsKnownPoint(ColorImagePoint colorImagePoint) => colorImagePoint.X != int.MinValue || colorImagePoint.Y != int.MinValue;

        public static bool IsKnownPoint(DepthImagePoint depthImagePoint) => depthImagePoint.Depth != 0;

        public static bool IsKnownPoint(SkeletonPoint skeletonPoint) => (double)skeletonPoint.Z > 0.0;

        public DepthImageStream DepthStream { get; private set; }

        public ColorImageStream ColorStream { get; private set; }

        public SkeletonStream SkeletonStream { get; private set; }

        public KinectAudioSource AudioSource
        {
            get
            {
                using (this.EnterReadLock())
                {
                    if (this._audioSource == null)
                        this._audioSource = this.LastStatus != KinectStatus.Connected ? (KinectAudioSource)null : new KinectAudioSource(this);
                    return this._audioSource;
                }
            }
        }

        public int MaxElevationAngle => 27;

        public int MinElevationAngle => -27;

        public int ElevationAngle
        {
            get
            {
                using (this.EnterReadLock())
                {
                    this.CheckNotRunning();
                    return this.NuiSensor.NuiCameraElevationGetAngle();
                }
            }
            set
            {
                using (this.EnterReadLock())
                {
                    this.CheckNotRunning();
                    if (value < this.MinElevationAngle || value > this.MaxElevationAngle)
                        throw new ArgumentOutOfRangeException(Resources.ElevationIncorrect);
                    this.NuiSensor.NuiCameraElevationSetAngle(value);
                }
            }
        }

        public bool ForceInfraredEmitterOff
        {
            get => this.NuiSensor.ForceInfraredEmitterOff;
            set => this.NuiSensor.ForceInfraredEmitterOff = value;
        }

        public IDepthFilter DepthFilter
        {
            get => this.NuiSensor.DepthFilter;
            set => this.NuiSensor.DepthFilter = value;
        }

        public Vector4 AccelerometerGetCurrentReading()
        {
            using (this.EnterReadLock())
                return this.NuiSensor.NuiAccelerometerGetCurrentReading();
        }

        [Obsolete("This method is replaced by Microsoft.Kinect.CoordinateMapper.MapDepthPointToColorPoint", false)]
        public ColorImagePoint MapDepthToColorImagePoint(
          DepthImageFormat depthImageFormat,
          int depthX,
          int depthY,
          short depthPixelValue,
          ColorImageFormat colorImageFormat)
        {
            if (depthImageFormat == DepthImageFormat.Undefined)
                throw new ArgumentException(Resources.ImageFormatNotSupported, nameof(depthImageFormat));
            if (colorImageFormat == ColorImageFormat.Undefined)
                throw new ArgumentException(Resources.ImageFormatNotSupported, nameof(colorImageFormat));
            KinectEtwProvider.EventWriteMapDepthToColorImagePointStart();
            int plColorX;
            int plColorY;
            var viewArea = new _NUI_IMAGE_VIEW_AREA()
            {
                eDigitalZoom = 0,
                lCenterX = 0,
                lCenterY = 0
            };
            this.NuiSensor.NuiImageGetColorPixelCoordinatesFromDepthPixelAtResolution((_NUI_IMAGE_RESOLUTION)ColorImageStream.LookUpImageResolution(colorImageFormat), (_NUI_IMAGE_RESOLUTION)DepthImageStream.LookUpImageResolution(depthImageFormat), ref viewArea, depthX, depthY, (ushort)depthPixelValue, out plColorX, out plColorY);
            KinectEtwProvider.EventWriteMapDepthToColorImagePointEnd();
            return new ColorImagePoint()
            {
                X = plColorX,
                Y = plColorY
            };
        }

        [Obsolete("This method is replaced by Microsoft.Kinect.CoordinateMapper.MapDepthFrameToColorFrame", false)]
        public void MapDepthFrameToColorFrame(
          DepthImageFormat depthImageFormat,
          short[] depthPixelData,
          ColorImageFormat colorImageFormat,
          ColorImagePoint[] colorCoordinates)
        {
            if (depthImageFormat == DepthImageFormat.Undefined)
                throw new ArgumentException(Resources.ImageFormatNotSupported, nameof(depthImageFormat));
            if (depthPixelData == null)
                throw new ArgumentNullException(nameof(depthPixelData));
            if (depthPixelData.Length != DepthImageStream.LookUpPixelDataLength(depthImageFormat))
                throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof(depthPixelData));
            if (colorImageFormat == ColorImageFormat.Undefined)
                throw new ArgumentException(Resources.ImageFormatNotSupported, nameof(colorImageFormat));
            if (colorCoordinates == null)
                throw new ArgumentNullException(nameof(colorCoordinates));
            if (colorCoordinates.Length != depthPixelData.Length)
                throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof(colorCoordinates));
            KinectEtwProvider.EventWriteMapDepthFrameToColorFrameStart();
            this.NuiSensor.NuiImageGetColorPixelCoordinateFrameFromDepthPixelFrameAtResolution((_NUI_IMAGE_RESOLUTION)ColorImageStream.LookUpImageResolution(colorImageFormat), (_NUI_IMAGE_RESOLUTION)DepthImageStream.LookUpImageResolution(depthImageFormat), depthPixelData, colorCoordinates);
            KinectEtwProvider.EventWriteMapDepthFrameToColorFrameEnd();
        }

        [Obsolete("This method is replaced by Microsoft.Kinect.CoordinateMapper.MapSkeletonPointToColorPoint", false)]
        public ColorImagePoint MapSkeletonPointToColor(
          SkeletonPoint skeletonPoint,
          ColorImageFormat colorImageFormat)
        {
            if (colorImageFormat == ColorImageFormat.Undefined)
                throw new ArgumentException(Resources.ImageFormatNotSupported, nameof(colorImageFormat));
            KinectEtwProvider.EventWriteMapSkeletonPointToColorStart();
            _Vector4 vPoint;
            vPoint.x = skeletonPoint.X;
            vPoint.y = skeletonPoint.Y;
            vPoint.z = skeletonPoint.Z;
            vPoint.w = 1f;
            float pfDepthX;
            float pfDepthY;
            float pfDepthValue;
            KinectSensor.ManagedNuiTransformSkeletonToDepthImage(vPoint, out pfDepthX, out pfDepthY, out pfDepthValue);
            ColorImagePoint colorImagePoint = this.MapDepthToColorImagePoint(DepthImageFormat.Resolution640x480Fps30, (int)((double)pfDepthX * 640.0), (int)((double)pfDepthY * 480.0), (short)((int)pfDepthValue << 3), colorImageFormat);
            KinectEtwProvider.EventWriteMapSkeletonPointToColorEnd();
            return colorImagePoint;
        }

        [Obsolete("This method is replaced by Microsoft.Kinect.CoordinateMapper.MapSkeletonPointToDepthPoint", false)]
        public DepthImagePoint MapSkeletonPointToDepth(
          SkeletonPoint skeletonPoint,
          DepthImageFormat depthImageFormat)
        {
            if (depthImageFormat == DepthImageFormat.Undefined)
                throw new ArgumentException(Resources.ImageFormatNotSupported, nameof(depthImageFormat));
            KinectEtwProvider.EventWriteMapSkeletonPointToDepthStart();
            _Vector4 vPoint;
            vPoint.x = skeletonPoint.X;
            vPoint.y = skeletonPoint.Y;
            vPoint.z = skeletonPoint.Z;
            vPoint.w = 1f;
            float pfDepthX;
            float pfDepthY;
            float pfDepthValue;
            KinectSensor.ManagedNuiTransformSkeletonToDepthImage(vPoint, out pfDepthX, out pfDepthY, out pfDepthValue);
            int width;
            int height;
            ImageStream.ResolutionToHeightWidth(DepthImageStream.LookUpImageResolution(depthImageFormat), out width, out height);
            DepthImagePoint depth = new DepthImagePoint()
            {
                X = (int)((double)pfDepthX * (double)width + 0.5),
                Y = (int)((double)pfDepthY * (double)height + 0.5),
                Depth = (int)((double)pfDepthValue + 0.5)
            };
            KinectEtwProvider.EventWriteMapSkeletonPointToDepthEnd();
            return depth;
        }

        [Obsolete("This method is replaced by Microsoft.Kinect.CoordinateMapper.MapDepthPointToSkeletonPoint", false)]
        public SkeletonPoint MapDepthToSkeletonPoint(
          DepthImageFormat depthImageFormat,
          int depthX,
          int depthY,
          short depthPixelValue)
        {
            if (depthImageFormat == DepthImageFormat.Undefined)
                throw new ArgumentException(Resources.ImageFormatNotSupported, nameof(depthImageFormat));
            KinectEtwProvider.EventWriteMapDepthToSkeletonPointStart();
            int width;
            int height;
            ImageStream.ResolutionToHeightWidth(DepthImageStream.LookUpImageResolution(depthImageFormat), out width, out height);
            _Vector4 skeleton = KinectSensor.ManagedNuiTransformDepthImageToSkeleton((float)depthX / (float)width, (float)depthY / (float)height, depthPixelValue);
            SkeletonPoint skeletonPoint = new SkeletonPoint()
            {
                X = skeleton.x,
                Y = skeleton.y,
                Z = skeleton.z
            };
            KinectEtwProvider.EventWriteMapDepthToSkeletonPointEnd();
            return skeletonPoint;
        }

        private static _Vector4 ManagedNuiTransformDepthImageToSkeleton(
          float fDepthX,
          float fDepthY,
          short depthPixelDatum)
        {
            KinectEtwProvider.EventWriteManagedNuiTransformDepthImageToSkeletonStart();
            float num1 = (float)((int)depthPixelDatum >> 3) / 1000f;
            float num2 = (float)((double)(fDepthX - 0.5f) * (0.003501000115647912 * (double)num1) * 320.0);
            float num3 = (float)((double)(0.5f - fDepthY) * (0.003501000115647912 * (double)num1) * 240.0);
            _Vector4 skeleton;
            skeleton.x = num2;
            skeleton.y = num3;
            skeleton.z = num1;
            skeleton.w = 1f;
            KinectEtwProvider.EventWriteManagedNuiTransformDepthImageToSkeletonEnd();
            return skeleton;
        }

        internal static void ManagedNuiTransformSkeletonToDepthImage(
          _Vector4 vPoint,
          out float pfDepthX,
          out float pfDepthY,
          out float pfDepthValue)
        {
            KinectEtwProvider.EventWriteManagedNuiTransformSkeletonToDepthImageStart();
            if ((double)vPoint.z > 1.4012984643248171E-45)
            {
                pfDepthX = (float)(0.5 + (double)vPoint.x * 285.6300048828125 / ((double)vPoint.z * 320.0));
                pfDepthY = (float)(0.5 - (double)vPoint.y * 285.6300048828125 / ((double)vPoint.z * 240.0));
                pfDepthValue = vPoint.z * 1000f;
            }
            else
            {
                pfDepthX = 0.0f;
                pfDepthY = 0.0f;
                pfDepthValue = 0.0f;
            }
            KinectEtwProvider.EventWriteManagedNuiTransformSkeletonToDepthImageEnd();
        }

        public void Start()
        {
            using (AutoLock autoLock = this.EnterUpgradeableReadLock())
            {
                if (this.LastStatus != KinectStatus.Connected)
                    throw new InvalidOperationException(Resources.KinectNotReady);
                if (!this.IsRunningInternal)
                {
                    using (autoLock.UpgradeToWrite())
                        this.Initialize(this._requestedOptions);
                }
                KinectEtwProvider.EventWriteManagedKinectSensorStartInfo(this.GetDeviceConnectionId());
            }
        }

        public void Stop()
        {
            using (AutoLock autoLock = this.EnterUpgradeableReadLock())
            {
                if (!this.IsRunningInternal)
                    return;
                using (autoLock.UpgradeToWrite())
                    this.Uninitialize();
            }
        }

        public bool IsRunning
        {
            get
            {
                using (this.EnterReadLock())
                    return this.IsRunningInternal;
            }
        }

        private bool IsRunningInternal => this._currentOptions != KinectSensor.SensorOptions.None;

        internal void RequestSkeletonEngine(bool enable)
        {
            using (AutoLock autoLock = this.EnterUpgradeableReadLock())
            {
                this._requestedOptions = !enable ? KinectSensor.SensorOptions.UseColor | KinectSensor.SensorOptions.UseDepth : KinectSensor.SensorOptions.UseDepthAndPlayerIndex | KinectSensor.SensorOptions.UseColor | KinectSensor.SensorOptions.UseSkeletalTracking | KinectSensor.SensorOptions.UseDepth;
                if (this.LastStatus != KinectStatus.Connected || !this.IsRunningInternal || this._currentOptions == (this._requestedOptions | KinectSensor.SensorOptions.UseAudio))
                    return;
                using (autoLock.UpgradeToWrite())
                {
                    this.Uninitialize();
                    this.Initialize(this._requestedOptions);
                }
            }
        }

        private void Initialize(KinectSensor.SensorOptions options)
        {
            KinectEtwProvider.EventWriteKinectSensorInitializeStart();
            options |= KinectSensor.SensorOptions.UseAudio;
            this.NuiSensor.NuiInitialize((uint)options);
            this._currentOptions = options;
            try
            {
                this.ColorStream.Start();
                this.DepthStream.Start();
                this.SkeletonStream.Start();
            }
            catch (InvalidOperationException ex)
            {
                this.Uninitialize();
                throw;
            }
            this._hKillThread = new ManualResetEvent(false);
            this._hKillThread.Reset();
            this._thread = new Thread(new ParameterizedThreadStart(this.MainBackgroundThreadProc));
            this._thread.Name = "Background M-NUI Thread";
            this._thread.IsBackground = true;
            this._thread.Start((object)this._hKillThread);
            KinectEtwProvider.EventWriteKinectSensorInitializeEnd();
        }

        private void Uninitialize()
        {
            this.ColorStream.Stop();
            this.DepthStream.Stop();
            this.SkeletonStream.Stop();
            if (this._audioSource != null)
                this._audioSource.Stop();
            if (this._hKillThread != null)
            {
                this._hKillThread.Set();
                this._hKillThread = (ManualResetEvent)null;
            }
            if (this._thread != null)
            {
                this._thread.Join();
                this._thread = (Thread)null;
            }
            this.NuiSensor.NuiShutdown();
            this._currentOptions = KinectSensor.SensorOptions.None;
        }

        private void MainBackgroundThreadProc(object data)
        {
            using (ManualResetEvent manualResetEvent = (ManualResetEvent)data)
            {
                List<WaitHandle> waitHandleList = new List<WaitHandle>(4)
                {
                    (WaitHandle) manualResetEvent
                };
                while (true)
                {
                    bool flag1 = default;
                    do
                    {
                        if (waitHandleList.Count > 1)
                            waitHandleList.RemoveRange(1, waitHandleList.Count - 1);
                        bool flag2 = false;
                        bool flag3 = false;
                        bool flag4 = false;
                        if (this.ColorStream != null && this.ColorStream.IsEnabled && (this.HasColorInvocations || this.HasAllFramesInvocations))
                        {
                            flag2 = true;
                            waitHandleList.Add((WaitHandle)this.ColorStream.NextFrameEvent);
                        }
                        if (this.DepthStream != null && this.DepthStream.IsEnabled && (this.HasDepthInvocations || this.HasAllFramesInvocations))
                        {
                            flag3 = true;
                            waitHandleList.Add((WaitHandle)this.DepthStream.NextFrameEvent);
                        }
                        if (this.SkeletonStream != null && this.SkeletonStream.IsEnabled && (this.HasSkeletonInvocations || this.HasAllFramesInvocations))
                        {
                            flag4 = true;
                            waitHandleList.Add((WaitHandle)this.SkeletonStream.NextSkeletonEvent);
                        }
                        switch (WaitHandle.WaitAny(waitHandleList.ToArray(), 250))
                        {
                            case 0:
                                goto label_41;
                            case 258:
                                continue;
                            default:
                                bool flag5 = true;
                                while (flag5)
                                {
                                    if (flag2 && this._frameHelpers[0].IsReadyForSetArgs && this.ColorStream.NextFrameEvent.WaitOne(0))
                                        this.GetColorFrame();
                                    if (flag3 && this._frameHelpers[1].IsReadyForSetArgs && this.DepthStream.NextFrameEvent.WaitOne(0))
                                        this.GetDepthFrame();
                                    if (flag4 && this._frameHelpers[2].IsReadyForSetArgs && this.SkeletonStream.NextSkeletonEvent.WaitOne(0))
                                        this.GetSkeletonFrame();
                                    long earliestTimestampSoFar = long.MaxValue;
                                    int earliestFramenumberSoFar = int.MaxValue;
                                    KinectSensor.FrameStateHelper frameStateHelper = (KinectSensor.FrameStateHelper)null;
                                    foreach (KinectSensor.FrameStateHelper frameHelper in this._frameHelpers)
                                    {
                                        if (frameHelper.IsCandidateForInvoke(earliestTimestampSoFar, earliestFramenumberSoFar))
                                        {
                                            earliestTimestampSoFar = frameHelper.MostRecentTimestamp;
                                            earliestFramenumberSoFar = frameHelper.MostRecentFramenumber;
                                            frameStateHelper = frameHelper;
                                        }
                                    }
                                    if (frameStateHelper != null)
                                        frameStateHelper.Invoke(this);
                                    else
                                        flag5 = false;
                                }
                                flag1 = (!flag2 || this._frameHelpers[0].HasArgs) && (!flag3 || this._frameHelpers[1].HasArgs) && (!flag4 || this._frameHelpers[2].HasArgs);
                                if (flag1 && flag3 && flag4)
                                {
                                    if (this._frameHelpers[1].MostRecentTimestamp < this._frameHelpers[2].MostRecentTimestamp)
                                    {
                                        this._frameHelpers[1].ResetAndReturnArgs();
                                        flag1 = false;
                                    }
                                    else if (this._frameHelpers[1].MostRecentTimestamp > this._frameHelpers[2].MostRecentTimestamp)
                                    {
                                        this._frameHelpers[2].ResetAndReturnArgs();
                                        flag1 = false;
                                    }
                                }
                                if (flag1 && flag2 && (flag3 || flag4))
                                {
                                    long num = this._frameHelpers[0].MostRecentTimestamp - (flag3 ? this._frameHelpers[1].MostRecentTimestamp : this._frameHelpers[2].MostRecentTimestamp);
                                    if (num > 16L && num <= 33L)
                                    {
                                        if (flag3)
                                            this._frameHelpers[1].ResetAndReturnArgs();
                                        if (flag4)
                                            this._frameHelpers[2].ResetAndReturnArgs();
                                        flag1 = false;
                                    }
                                }
                                continue;
                        }
                    }
                    while (!flag1);
                    KinectEtwProvider.EventWriteAllFramesReady(this._frameHelpers[0].MostRecentTimestamp, this._frameHelpers[1].MostRecentTimestamp, this._frameHelpers[2].MostRecentTimestamp);
                    this._allFramesReadyContextHandler.Invoke((object)this, new AllFramesReadyEventArgs((ColorImageFrameReadyEventArgs)this._frameHelpers[0].ResetAndReturnArgs(), (DepthImageFrameReadyEventArgs)this._frameHelpers[1].ResetAndReturnArgs(), (SkeletonFrameReadyEventArgs)this._frameHelpers[2].ResetAndReturnArgs()));
                }
            label_41:;
            }
        }

        private void GetDepthFrame()
        {
            int frameNumber;
            long timestamp;
            ImageFrameFlags frameFlags;
            if (this.DepthStream == null || !this.DepthStream.TryGetNextFrameInternal(0, out frameNumber, out timestamp, out frameFlags))
                return;
            this._frameHelpers[1].SetArgs((EventArgs)DepthImageFrameReadyEventArgs.Create(this.DepthStream, frameNumber, timestamp, frameFlags), timestamp, frameNumber);
        }

        private void GetColorFrame()
        {
            int frameNumber;
            long timestamp;
            ImageFrameFlags frameFlags;
            if (this.ColorStream == null || !this.ColorStream.TryGetNextFrameInternal(0, out frameNumber, out timestamp, out frameFlags))
                return;
            this._frameHelpers[0].SetArgs((EventArgs)ColorImageFrameReadyEventArgs.Create(this.ColorStream, frameNumber, timestamp, frameFlags), timestamp, frameNumber);
        }

        private void GetSkeletonFrame()
        {
            int frameNumber;
            long timestamp;
            SkeletonTrackingMode trackingMode;
            if (this.SkeletonStream == null || !this.SkeletonStream.TryGetNextFrameInternal(0, out frameNumber, out timestamp, out trackingMode))
                return;
            this._frameHelpers[2].SetArgs((EventArgs)SkeletonFrameReadyEventArgs.Create(this.SkeletonStream, frameNumber, timestamp, trackingMode), timestamp, frameNumber);
        }

        internal bool IsDisposed { get; private set; }

        public void Dispose()
        {
            lock (this._disposeLock)
            {
                if (this.IsDisposed)
                    return;
                using (this.EnterWriteLock())
                {
                    this._allFramesReadyContextHandler.Dispose();
                    this._depthFrameReadyContextHandler.Dispose();
                    this._colorFrameReadyContextHandler.Dispose();
                    this._skeletonFrameReadyContextHandler.Dispose();
                    if (this.IsRunningInternal)
                        this.Uninitialize();
                    string name = (string)null;
                    if (this.LastStatus == KinectStatus.Connected)
                        name = this.GetDeviceConnectionId();
                    if (this.DepthStream != null)
                    {
                        this.DepthStream.Dispose();
                        this.DepthStream = (DepthImageStream)null;
                    }
                    if (this.ColorStream != null)
                    {
                        this.ColorStream.Dispose();
                        this.ColorStream = (ColorImageStream)null;
                    }
                    if (this.SkeletonStream != null)
                    {
                        this.SkeletonStream.Dispose();
                        this.SkeletonStream = (SkeletonStream)null;
                    }
                    if (this._audioSource != null)
                    {
                        this._audioSource.Dispose();
                        this._audioSource = (KinectAudioSource)null;
                    }
                    this.NuiSensor = (NuiSensor)null;
                    KinectSensorCollection.Instance.RemakeSensor(this, name);
                    this.IsDisposed = true;
                }
                this._initLock.Dispose();
            }
        }

        private AutoLock EnterReadLock()
        {
            lock (this._disposeLock)
            {
                if (this.IsDisposed)
                    throw new ObjectDisposedException(nameof(KinectSensor));
                return this._initLock.EnterReadLockAuto();
            }
        }

        private AutoLock EnterUpgradeableReadLock()
        {
            lock (this._disposeLock)
            {
                if (this.IsDisposed)
                    throw new ObjectDisposedException(nameof(KinectSensor));
                return this._initLock.EnterUpgradeableReadLockAuto();
            }
        }

        private AutoLock EnterWriteLock()
        {
            lock (this._disposeLock)
            {
                if (this.IsDisposed)
                    throw new ObjectDisposedException(nameof(KinectSensor));
                return this._initLock.EnterWriteLockAuto();
            }
        }

        private void CheckNotRunning()
        {
            if (!this.IsRunningInternal)
                throw new InvalidOperationException(Resources.KinectMustBeRunning);
        }

        private abstract class FrameStateHelper
        {
            public long MostRecentTimestamp { get; protected set; }

            public int MostRecentFramenumber { get; protected set; }

            public bool IsReadyForSetArgs => !this.HasArgs || this.HaveDispatchedEvent;

            public abstract bool HasArgs { get; }

            public abstract void SetArgs(EventArgs args, long timestamp, int framenumber);

            public abstract void Invoke(KinectSensor sender);

            public abstract EventArgs ResetAndReturnArgs();

            public bool IsCandidateForInvoke(long earliestTimestampSoFar, int earliestFramenumberSoFar)
            {
                if (!this.HasArgs || this.HaveDispatchedEvent)
                    return false;
                if (this.MostRecentTimestamp < earliestTimestampSoFar)
                    return true;
                return this.MostRecentTimestamp == earliestTimestampSoFar && this.MostRecentFramenumber < earliestFramenumberSoFar;
            }

            protected bool HaveDispatchedEvent { get; set; }

            protected abstract bool HasListeners { get; }
        }

        internal enum FrameStateType
        {
            Color,
            Depth,
            Skeleton,
        }

        private class FrameStateHelper<TArgs> : KinectSensor.FrameStateHelper where TArgs : EventArgs
        {
            private readonly ContextEventHandler<TArgs> _handler;
            private readonly KinectSensor.FrameStateType _type;
            private TArgs _args;

            public FrameStateHelper(
              KinectSensor.FrameStateType type,
              ContextEventHandler<TArgs> contextHandler)
            {
                this._type = type;
                this._handler = contextHandler;
            }

            public override bool HasArgs => (object)this._args != null;

            public override void SetArgs(EventArgs args, long timestamp, int framenumber)
            {
                this._args = (TArgs)args;
                this.MostRecentTimestamp = timestamp;
                this.MostRecentFramenumber = framenumber;
                this.HaveDispatchedEvent = false;
            }

            public override void Invoke(KinectSensor sender)
            {
                switch (this._type)
                {
                    case KinectSensor.FrameStateType.Color:
                        KinectEtwProvider.EventWriteColorFrameReady(this.MostRecentTimestamp);
                        break;
                    case KinectSensor.FrameStateType.Depth:
                        KinectEtwProvider.EventWriteDepthFrameReady(this.MostRecentTimestamp);
                        break;
                    case KinectSensor.FrameStateType.Skeleton:
                        KinectEtwProvider.EventWriteSkeletonFrameReady(this.MostRecentTimestamp);
                        break;
                }
                if (this.HasListeners)
                    this._handler.Invoke((object)sender, this._args);
                this.HaveDispatchedEvent = true;
            }

            public override EventArgs ResetAndReturnArgs()
            {
                TArgs args = this._args;
                this._args = default(TArgs);
                this.HaveDispatchedEvent = false;
                this.MostRecentTimestamp = 0L;
                return (EventArgs)args;
            }

            protected override bool HasListeners => this._handler.HasHandlers;
        }

        [Flags]
        private enum SensorOptions
        {
            None = 0,
            UseDepthAndPlayerIndex = 1,
            UseColor = 2,
            UseSkeletalTracking = 8,
            UseDepth = 32, // 0x00000020
            UseAudio = 268435456, // 0x10000000
        }
    }
}
