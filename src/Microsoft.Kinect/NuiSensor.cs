// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.NuiSensor
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
    internal class NuiSensor
    {
        private readonly INuiSensor _nuiSensor;
        private IDepthFilter _depthFilter;
        private List<IDepthFilter> _depthFilters = new List<IDepthFilter>();
        private INuiAudioBeam _nuiAudioSource;
        private NuiCoordinateMapper _nuiCoordinateMapper;

        public NuiSensor(INuiSensor wrapped) => this._nuiSensor = wrapped;

        public int NuiInstanceIndex() => this._nuiSensor.NuiInstanceIndex();

        public void NuiInitialize(uint dwFlags) => KinectExceptionHelper.CheckHr(this._nuiSensor.NuiInitialize(dwFlags));

        public INuiAudioBeam NuiGetAudioSource()
        {
            if (this._nuiAudioSource == null)
            {
                Microsoft.Kinect.Interop.INuiAudioBeam ppDmo;
                KinectExceptionHelper.CheckHr(this._nuiSensor.NuiGetAudioSource(out ppDmo));
                this._nuiAudioSource = (INuiAudioBeam)new NuiAudioBeam(ppDmo);
            }
            return this._nuiAudioSource;
        }

        public NuiCoordinateMapper NuiGetCoordinateMapper()
        {
            if (this._nuiCoordinateMapper == null)
            {
                INuiCoordinateMapper pMapping;
                KinectExceptionHelper.CheckHr(this._nuiSensor.NuiGetCoordinateMapper(out pMapping));
                this._nuiCoordinateMapper = new NuiCoordinateMapper(pMapping);
            }
            return this._nuiCoordinateMapper;
        }

        public void NuiShutdown()
        {
            this._nuiAudioSource = (INuiAudioBeam)null;
            this._nuiSensor.NuiShutdown();
        }

        internal bool IsSupported
        {
            get
            {
                object obj = this[NuiSensorProperty.IsSupported];
                return obj != null && obj.GetType() == typeof(bool) && (bool)obj;
            }
        }

        public object this[NuiSensorProperty index]
        {
            get
            {
                try
                {
                    IPropertyStore propertyStore = (IPropertyStore)this._nuiSensor;
                    PROPERTYKEY propertykey;
                    propertykey.fmtid = new Guid("1f5e088c-a8c7-41d3-9957-209677a13e85");
                    propertykey.pid = (int)index;
                    propertyStore.GetValue(ref propertykey, out var result);
                    return result;
                }
                catch (COMException ex)
                {
                    return (object)null;
                }
            }
        }

        public void NuiSetFrameEndEvent(IntPtr hEvent, uint dwFrameEventFlag) => KinectExceptionHelper.CheckHr(this._nuiSensor.NuiSetFrameEndEvent(hEvent, dwFrameEventFlag));

        public void NuiImageStreamOpen(
          _NUI_IMAGE_TYPE eImageType,
          _NUI_IMAGE_RESOLUTION eResolution,
          uint dwImageFrameFlags,
          uint dwFrameLimit,
          IntPtr hNextFrameEvent,
          out IntPtr phStreamHandle)
        {
            KinectExceptionHelper.CheckHr(this._nuiSensor.NuiImageStreamOpen(eImageType, eResolution, dwImageFrameFlags, dwFrameLimit, hNextFrameEvent, out phStreamHandle));
        }

        public void NuiImageStreamSetImageFrameFlags(IntPtr hStream, uint dwImageFrameFlags) => KinectExceptionHelper.CheckHr(this._nuiSensor.NuiImageStreamSetImageFrameFlags(hStream, dwImageFrameFlags));

        public uint NuiImageStreamGetImageFrameFlags(IntPtr hStream)
        {
            uint pdwImageFrameFlags;
            KinectExceptionHelper.CheckHr(this._nuiSensor.NuiImageStreamGetImageFrameFlags(hStream, out pdwImageFrameFlags));
            return pdwImageFrameFlags;
        }

        public int NuiImageStreamGetNextFrameNoThrow(
          IntPtr hStream,
          uint dwMillisecondsToWait,
          out _NUI_IMAGE_FRAME pImageFrame)
        {
            return this._nuiSensor.NuiImageStreamGetNextFrame(hStream, dwMillisecondsToWait, out pImageFrame);
        }

        public void NuiImageStreamReleaseFrame(IntPtr hStream, ref _NUI_IMAGE_FRAME pImageFrame)
        {
            try
            {
                INuiSensor nuiSensor = this._nuiSensor;
                IntPtr hStream1 = hStream;
                _NUI_IMAGE_FRAME nuiImageFrame = pImageFrame;
                ref _NUI_IMAGE_FRAME local = ref nuiImageFrame;
                nuiSensor.NuiImageStreamReleaseFrame(hStream1, ref local);
                Marshal.FinalReleaseComObject((object)pImageFrame.pFrameTexture);
            }
            catch (COMException ex)
            {
            }
        }

        public void NuiImageFrameGetDepthImagePixelFrameTexture(
          IntPtr hStream,
          ref _NUI_IMAGE_FRAME pImageFrame,
          out bool nearMode,
          out INuiFrameTexture ppTexture)
        {
            int pNearMode;
            KinectExceptionHelper.CheckHr(this._nuiSensor.NuiImageFrameGetDepthImagePixelFrameTexture(hStream, ref pImageFrame, out pNearMode, out ppTexture));
            nearMode = pNearMode != 0;
        }

        public void NuiImageGetColorPixelCoordinatesFromDepthPixelAtResolution(
          _NUI_IMAGE_RESOLUTION eColorResolution,
          _NUI_IMAGE_RESOLUTION eDepthResolution,
          ref _NUI_IMAGE_VIEW_AREA pcViewArea,
          int lDepthX,
          int lDepthY,
          ushort usDepthValue,
          out int plColorX,
          out int plColorY)
        {
            KinectExceptionHelper.CheckHr(this._nuiSensor.NuiImageGetColorPixelCoordinatesFromDepthPixelAtResolution(eColorResolution, eDepthResolution, ref pcViewArea, lDepthX, lDepthY, usDepthValue, out plColorX, out plColorY));
        }

        public unsafe void NuiImageGetColorPixelCoordinateFrameFromDepthPixelFrameAtResolution(
          _NUI_IMAGE_RESOLUTION eColorResolution,
          _NUI_IMAGE_RESOLUTION eDepthResolution,
          short[] depthValues,
          ColorImagePoint[] colorCoordinates)
        {
            fixed (ColorImagePoint* pColorCoordinates = colorCoordinates)
                KinectExceptionHelper.CheckHr(this._nuiSensor.NuiImageGetColorPixelCoordinateFrameFromDepthPixelFrameAtResolution(eColorResolution, eDepthResolution, (uint)depthValues.Length, depthValues, (uint)(colorCoordinates.Length * 2), (IntPtr)(void*)pColorCoordinates));
        }

        public void NuiCameraElevationSetAngle(int lAngleDegrees) => KinectExceptionHelper.CheckHr(this._nuiSensor.NuiCameraElevationSetAngle(lAngleDegrees));

        public int NuiCameraElevationGetAngle()
        {
            int plAngleDegrees;
            KinectExceptionHelper.CheckHr(this._nuiSensor.NuiCameraElevationGetAngle(out plAngleDegrees));
            return plAngleDegrees;
        }

        public Vector4 NuiAccelerometerGetCurrentReading()
        {
            _Vector4 pReading;
            KinectExceptionHelper.CheckHr(this._nuiSensor.NuiAccelerometerGetCurrentReading(out pReading));
            return Vector4.CopyFrom(ref pReading);
        }

        public void NuiSkeletonTrackingEnable(IntPtr hNextFrameEvent, uint dwFlags) => KinectExceptionHelper.CheckHr(this._nuiSensor.NuiSkeletonTrackingEnable(hNextFrameEvent, dwFlags));

        public int NuiSkeletonTrackingDisableNoThrow() => this._nuiSensor.NuiSkeletonTrackingDisable();

        public int NuiSkeletonGetNextFrameNoThrow(
          uint dwMillisecondsToWait,
          ref _NUI_SKELETON_FRAME pSkeletonFrame)
        {
            return this._nuiSensor.NuiSkeletonGetNextFrame(dwMillisecondsToWait, ref pSkeletonFrame);
        }

        public void NuiTransformSmooth(
          ref _NUI_SKELETON_FRAME pSkeletonFrame,
          ref _NUI_TRANSFORM_SMOOTH_PARAMETERS pSmoothingParams)
        {
            KinectExceptionHelper.CheckHr(this._nuiSensor.NuiTransformSmooth(ref pSkeletonFrame, ref pSmoothingParams));
        }

        public string NuiDeviceConnectionId() => this._nuiSensor.NuiDeviceConnectionId();

        public string NuiUniqueId() => this._nuiSensor.NuiUniqueId();

        public string NuiAudioArrayId() => this._nuiSensor.NuiAudioArrayId();

        public uint NuiStatus() => (uint)this._nuiSensor.NuiStatus();

        public uint NuiInitializationFlags() => this._nuiSensor.NuiInitializationFlags();

        public void NuiSkeletonSetTrackedSkeletons(uint trackingId1, uint trackingId2) => KinectExceptionHelper.CheckHr(this._nuiSensor.NuiSkeletonSetTrackedSkeletons(new uint[2]
        {
      trackingId1,
      trackingId2
        }));

        public NuiColorCameraSettings NuiGetColorCameraSettings()
        {
            INuiColorCameraSettings pCameraSettings;
            KinectExceptionHelper.CheckHr(this._nuiSensor.NuiGetColorCameraSettings(out pCameraSettings));
            return new NuiColorCameraSettings(pCameraSettings);
        }

        public bool ForceInfraredEmitterOff
        {
            get => this._nuiSensor.NuiGetForceInfraredEmitterOff() != 0;
            set => KinectExceptionHelper.CheckHr(this._nuiSensor.NuiSetForceInfraredEmitterOff(value ? 1 : 0));
        }

        public IDepthFilter DepthFilter
        {
            get => this._depthFilter;
            set
            {
                if (value?.NativeObject is not INuiDepthFilter pDepthFilter)
                    throw new ArgumentException(Resources.DepthFilterMustImplementNativeInterface, nameof(value));
                KinectExceptionHelper.CheckHr(this._nuiSensor.NuiSetDepthFilter(pDepthFilter));
                this._depthFilter = value;
                if (value == null || this._depthFilters.Contains(value))
                    return;
                this._depthFilters.Add(value);
            }
        }

        public IDepthFilter GetDepthFilterForTimestamp(long timestamp)
        {
            INuiDepthFilter ppDepthFilter;
            if (this._nuiSensor.NuiGetDepthFilterForTimeStamp(new _LARGE_INTEGER()
            {
                QuadPart = timestamp
            }, out ppDepthFilter) == 0 && ppDepthFilter != null)
            {
                foreach (IDepthFilter depthFilter in this._depthFilters)
                {
                    if (depthFilter.NativeObject as INuiDepthFilter == ppDepthFilter)
                        return depthFilter;
                }
            }
            return (IDepthFilter)null;
        }
    }
}
