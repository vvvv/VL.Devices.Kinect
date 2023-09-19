// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Interop.INuiSensor
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect.Interop
{
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [Guid("D3D9AB7B-31BA-44CA-8CC0-D42525BBEA43")]
  [ComImport]
  internal interface INuiSensor
  {
    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiInitialize([In] uint dwFlags);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    void NuiShutdown();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiSetFrameEndEvent([In] IntPtr hEvent, [In] uint dwFrameEventFlag);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageStreamOpen(
      [In] _NUI_IMAGE_TYPE eImageType,
      [In] _NUI_IMAGE_RESOLUTION eResolution,
      [In] uint dwImageFrameFlags,
      [In] uint dwFrameLimit,
      [In] IntPtr hNextFrameEvent,
      out IntPtr phStreamHandle);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageStreamSetImageFrameFlags([In] IntPtr hStream, [In] uint dwImageFrameFlags);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageStreamGetImageFrameFlags([In] IntPtr hStream, out uint pdwImageFrameFlags);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageStreamGetNextFrame(
      [In] IntPtr hStream,
      [In] uint dwMillisecondsToWait,
      out _NUI_IMAGE_FRAME pImageFrame);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageStreamReleaseFrame([In] IntPtr hStream, [In] ref _NUI_IMAGE_FRAME pImageFrame);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageGetColorPixelCoordinatesFromDepthPixel(
      [In] _NUI_IMAGE_RESOLUTION eColorResolution,
      [In] ref _NUI_IMAGE_VIEW_AREA pcViewArea,
      [In] int lDepthX,
      [In] int lDepthY,
      [In] ushort usDepthValue,
      out int plColorX,
      out int plColorY);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageGetColorPixelCoordinatesFromDepthPixelAtResolution(
      [In] _NUI_IMAGE_RESOLUTION eColorResolution,
      [In] _NUI_IMAGE_RESOLUTION eDepthResolution,
      [In] ref _NUI_IMAGE_VIEW_AREA pcViewArea,
      [In] int lDepthX,
      [In] int lDepthY,
      [In] ushort usDepthValue,
      out int plColorX,
      out int plColorY);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageGetColorPixelCoordinateFrameFromDepthPixelFrameAtResolution(
      [In] _NUI_IMAGE_RESOLUTION eColorResolution,
      [In] _NUI_IMAGE_RESOLUTION eDepthResolution,
      [In] uint cDepthValues,
      [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] short[] pDepthValues,
      uint cColorCoordinates,
      [In] IntPtr pColorCoordinates);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiCameraElevationSetAngle([In] int lAngleDegrees);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiCameraElevationGetAngle(out int plAngleDegrees);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiSkeletonTrackingEnable([In] IntPtr hNextFrameEvent, [In] uint dwFlags);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiSkeletonTrackingDisable();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiSkeletonSetTrackedSkeletons([MarshalAs(UnmanagedType.LPArray, SizeConst = 2), In] uint[] TrackingIDs);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiSkeletonGetNextFrame([In] uint dwMillisecondsToWait, [In, Out] ref _NUI_SKELETON_FRAME pSkeletonFrame);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiTransformSmooth(
      ref _NUI_SKELETON_FRAME pSkeletonFrame,
      ref _NUI_TRANSFORM_SMOOTH_PARAMETERS pSmoothingParams);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiGetAudioSource([MarshalAs(UnmanagedType.Interface)] out INuiAudioBeam ppDmo);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    int NuiInstanceIndex();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string NuiDeviceConnectionId();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string NuiUniqueId();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.BStr)]
    string NuiAudioArrayId();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiStatus();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    uint NuiInitializationFlags();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiGetCoordinateMapper([MarshalAs(UnmanagedType.Interface)] out INuiCoordinateMapper pMapping);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiImageFrameGetDepthImagePixelFrameTexture(
      [In] IntPtr hStream,
      [In] ref _NUI_IMAGE_FRAME pImageFrame,
      out int pNearMode,
      [MarshalAs(UnmanagedType.Interface)] out INuiFrameTexture ppFrameTexture);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiGetColorCameraSettings([MarshalAs(UnmanagedType.Interface)] out INuiColorCameraSettings pCameraSettings);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    int NuiGetForceInfraredEmitterOff();

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiSetForceInfraredEmitterOff([In] int fForceInfraredEmitterOff);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiAccelerometerGetCurrentReading(out _Vector4 pReading);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiSetDepthFilter([MarshalAs(UnmanagedType.Interface), In] INuiDepthFilter pDepthFilter);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiGetDepthFilter([MarshalAs(UnmanagedType.Interface)] out INuiDepthFilter ppDepthFilter);

    [MethodImpl(MethodImplOptions.PreserveSig | MethodImplOptions.InternalCall, MethodCodeType = MethodCodeType.Runtime)]
    [return: MarshalAs(UnmanagedType.Error)]
    int NuiGetDepthFilterForTimeStamp([In] _LARGE_INTEGER liTimeStamp, [MarshalAs(UnmanagedType.Interface)] out INuiDepthFilter ppDepthFilter);
  }
}
