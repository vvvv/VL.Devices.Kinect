// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.KinectEtwProvider
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Diagnostics.Eventing;

namespace Microsoft.Kinect
{
  internal static class KinectEtwProvider
  {
    private static readonly EventProviderVersionTwo Provider = new EventProviderVersionTwo();
    private static EventDescriptor getSegmentationBegin = new EventDescriptor(0, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 1, long.MinValue);
    private static EventDescriptor getSegmentationEnd = new EventDescriptor(1, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 1, long.MinValue);
    private static EventDescriptor processInputBufferBegin = new EventDescriptor(2, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 2, long.MinValue);
    private static EventDescriptor processInputBufferEnd = new EventDescriptor(3, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 2, long.MinValue);
    private static EventDescriptor notifyFrameReadyStart = new EventDescriptor(4, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 3, long.MinValue);
    private static EventDescriptor notifyFrameReadyEnd = new EventDescriptor(5, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 3, long.MinValue);
    private static EventDescriptor processFrameStart = new EventDescriptor(6, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 4, long.MinValue);
    private static EventDescriptor processFrameEnd = new EventDescriptor(7, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 4, long.MinValue);
    private static EventDescriptor getSkeletonsBegin = new EventDescriptor(8, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 5, long.MinValue);
    private static EventDescriptor getSkeletonsEnd = new EventDescriptor(9, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 5, long.MinValue);
    private static EventDescriptor unpackBayerStart = new EventDescriptor(10, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 6, long.MinValue);
    private static EventDescriptor unpackBayerEnd = new EventDescriptor(11, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 6, long.MinValue);
    private static EventDescriptor frameStart = new EventDescriptor(12, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 7, long.MinValue);
    private static EventDescriptor frameEnd = new EventDescriptor(13, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 7, long.MinValue);
    private static EventDescriptor mapDepthFrameToColorFrameStart = new EventDescriptor(14, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 8, long.MinValue);
    private static EventDescriptor mapDepthFrameToColorFrameEnd = new EventDescriptor(15, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 8, long.MinValue);
    private static EventDescriptor mapDepthToColorImagePointStart = new EventDescriptor(16, (byte) 0, (byte) 16, (byte) 5, (byte) 1, 9, long.MinValue);
    private static EventDescriptor mapDepthToColorImagePointEnd = new EventDescriptor(17, (byte) 0, (byte) 16, (byte) 5, (byte) 2, 9, long.MinValue);
    private static EventDescriptor mapSkeletonPointToColorStart = new EventDescriptor(18, (byte) 0, (byte) 16, (byte) 5, (byte) 1, 10, long.MinValue);
    private static EventDescriptor mapSkeletonPointToColorEnd = new EventDescriptor(19, (byte) 0, (byte) 16, (byte) 5, (byte) 2, 10, long.MinValue);
    private static EventDescriptor mapSkeletonPointToDepthStart = new EventDescriptor(20, (byte) 0, (byte) 16, (byte) 5, (byte) 1, 11, long.MinValue);
    private static EventDescriptor mapSkeletonPointToDepthEnd = new EventDescriptor(21, (byte) 0, (byte) 16, (byte) 5, (byte) 2, 11, long.MinValue);
    private static EventDescriptor mapDepthToSkeletonPointStart = new EventDescriptor(22, (byte) 0, (byte) 16, (byte) 5, (byte) 1, 12, long.MinValue);
    private static EventDescriptor mapDepthToSkeletonPointEnd = new EventDescriptor(23, (byte) 0, (byte) 16, (byte) 5, (byte) 2, 12, long.MinValue);
    private static EventDescriptor managedNuiTransformDepthImageToSkeletonStart = new EventDescriptor(24, (byte) 0, (byte) 16, (byte) 5, (byte) 1, 13, long.MinValue);
    private static EventDescriptor managedNuiTransformDepthImageToSkeletonEnd = new EventDescriptor(25, (byte) 0, (byte) 16, (byte) 5, (byte) 2, 13, long.MinValue);
    private static EventDescriptor managedNuiTransformSkeletonToDepthImageStart = new EventDescriptor(26, (byte) 0, (byte) 16, (byte) 5, (byte) 1, 14, long.MinValue);
    private static EventDescriptor managedNuiTransformSkeletonToDepthImageEnd = new EventDescriptor(27, (byte) 0, (byte) 16, (byte) 5, (byte) 2, 14, long.MinValue);
    private static EventDescriptor nuiImageGetColorPixelCoordinatesFromDepthPixelAtResolutionStart = new EventDescriptor(28, (byte) 0, (byte) 16, (byte) 5, (byte) 1, 15, long.MinValue);
    private static EventDescriptor nuiImageGetColorPixelCoordinatesFromDepthPixelAtResolutionEnd = new EventDescriptor(29, (byte) 0, (byte) 16, (byte) 5, (byte) 2, 15, long.MinValue);
    private static EventDescriptor nuiImageGetColorPixelCoordinateFrameFromDepthPixelFrameAtResolutionStart = new EventDescriptor(30, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 16, long.MinValue);
    private static EventDescriptor nuiImageGetColorPixelCoordinateFrameFromDepthPixelFrameAtResolutionEnd = new EventDescriptor(31, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 16, long.MinValue);
    private static EventDescriptor nuiImageStreamOpenStart = new EventDescriptor(32, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 17, long.MinValue);
    private static EventDescriptor nuiImageStreamOpenEnd = new EventDescriptor(33, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 17, long.MinValue);
    private static EventDescriptor nuiImageStreamGetNextFrameStart = new EventDescriptor(34, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 18, long.MinValue);
    private static EventDescriptor nuiImageStreamGetNextFrameEnd = new EventDescriptor(35, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 18, long.MinValue);
    private static EventDescriptor nuiImageStreamReleaseFrameStart = new EventDescriptor(36, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 19, long.MinValue);
    private static EventDescriptor nuiImageStreamReleaseFrameEnd = new EventDescriptor(37, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 19, long.MinValue);
    private static EventDescriptor nuiInitializeStart = new EventDescriptor(38, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 20, long.MinValue);
    private static EventDescriptor nuiInitializeEnd = new EventDescriptor(39, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 20, long.MinValue);
    private static EventDescriptor allFramesReady = new EventDescriptor(40, (byte) 0, (byte) 16, (byte) 4, (byte) 0, 21, long.MinValue);
    private static EventDescriptor colorFrameReady = new EventDescriptor(41, (byte) 0, (byte) 16, (byte) 4, (byte) 0, 22, long.MinValue);
    private static EventDescriptor depthFrameReady = new EventDescriptor(42, (byte) 0, (byte) 16, (byte) 4, (byte) 0, 23, long.MinValue);
    private static EventDescriptor skeletonFrameReady = new EventDescriptor(43, (byte) 0, (byte) 16, (byte) 4, (byte) 0, 24, long.MinValue);
    private static EventDescriptor stopDeviceStart = new EventDescriptor(44, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 25, long.MinValue);
    private static EventDescriptor stopDeviceEnd = new EventDescriptor(45, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 25, long.MinValue);
    private static EventDescriptor singleStreamStopStart = new EventDescriptor(46, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 26, long.MinValue);
    private static EventDescriptor singleStreamStopEnd = new EventDescriptor(47, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 26, long.MinValue);
    private static EventDescriptor detectGenuineDeviceStart = new EventDescriptor(48, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 27, long.MinValue);
    private static EventDescriptor detectGenuineDeviceEnd = new EventDescriptor(49, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 27, long.MinValue);
    private static EventDescriptor createDeviceStart = new EventDescriptor(50, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 28, long.MinValue);
    private static EventDescriptor createDeviceEnd = new EventDescriptor(51, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 28, long.MinValue);
    private static EventDescriptor reEnumerateDevicesStart = new EventDescriptor(52, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 29, long.MinValue);
    private static EventDescriptor reEnumerateDevicesEnd = new EventDescriptor(53, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 29, long.MinValue);
    private static EventDescriptor outputStreamInitStart = new EventDescriptor(54, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 30, long.MinValue);
    private static EventDescriptor outputStreamInitEnd = new EventDescriptor(55, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 30, long.MinValue);
    private static EventDescriptor singleStreamStartStart = new EventDescriptor(56, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 31, long.MinValue);
    private static EventDescriptor singleStreamStartEnd = new EventDescriptor(57, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 31, long.MinValue);
    private static EventDescriptor skeletalTrackerInitializeStart = new EventDescriptor(58, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 32, long.MinValue);
    private static EventDescriptor skeletalTrackerInitializeEnd = new EventDescriptor(59, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 32, long.MinValue);
    private static EventDescriptor kinectSensorInitializeStart = new EventDescriptor(60, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 33, long.MinValue);
    private static EventDescriptor kinectSensorInitializeEnd = new EventDescriptor(61, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 33, long.MinValue);
    private static EventDescriptor skeletonStageBGRStart = new EventDescriptor(62, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 34, long.MinValue);
    private static EventDescriptor skeletonStageBGREnd = new EventDescriptor(63, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 34, long.MinValue);
    private static EventDescriptor skeletonStageCDRPStart = new EventDescriptor(64, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 35, long.MinValue);
    private static EventDescriptor skeletonStageCDRPEnd = new EventDescriptor(65, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 35, long.MinValue);
    private static EventDescriptor skeletonStageHeadDetectorStart = new EventDescriptor(66, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 36, long.MinValue);
    private static EventDescriptor skeletonStageHeadDetectorEnd = new EventDescriptor(67, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 36, long.MinValue);
    private static EventDescriptor skeletonStageExemplarStart = new EventDescriptor(68, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 37, long.MinValue);
    private static EventDescriptor skeletonStageExemplarEnd = new EventDescriptor(69, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 37, long.MinValue);
    private static EventDescriptor skeletonStageCentroidsStart = new EventDescriptor(70, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 38, long.MinValue);
    private static EventDescriptor skeletonStageCentroidsEnd = new EventDescriptor(71, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 38, long.MinValue);
    private static EventDescriptor skeletonStageModelFittingStart = new EventDescriptor(72, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 39, long.MinValue);
    private static EventDescriptor skeletonStageModelFittingEnd = new EventDescriptor(73, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 39, long.MinValue);
    private static EventDescriptor frameRequestDropped = new EventDescriptor(74, (byte) 0, (byte) 16, (byte) 4, (byte) 0, 40, long.MinValue);
    private static EventDescriptor unexpectedFrameRequestError = new EventDescriptor(75, (byte) 0, (byte) 16, (byte) 4, (byte) 0, 41, long.MinValue);
    private static EventDescriptor frameStateChanged = new EventDescriptor(76, (byte) 0, (byte) 16, (byte) 4, (byte) 0, 42, long.MinValue);
    private static EventDescriptor frameDropped = new EventDescriptor(77, (byte) 0, (byte) 16, (byte) 4, (byte) 0, 43, long.MinValue);
    private static EventDescriptor nuiInitializeInfo = new EventDescriptor(85, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 20, -9223372036854775807L);
    private static EventDescriptor nuiImageStreamOpenInfo = new EventDescriptor(86, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 17, -9223372036854775807L);
    private static EventDescriptor nuiShutdownTrace = new EventDescriptor(87, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 44, -9223372036854775807L);
    private static EventDescriptor internalNuiShutdownTrace = new EventDescriptor(88, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 45, -9223372036854775807L);
    private static EventDescriptor nuiSkeletonTrackingEnableTrace = new EventDescriptor(89, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 55, -9223372036854775807L);
    private static EventDescriptor nuiSkeletonTrackingDisableTrace = new EventDescriptor(90, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 56, -9223372036854775807L);
    private static EventDescriptor nuiTransformSmoothInfo = new EventDescriptor(91, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 47, -9223372036854775807L);
    private static EventDescriptor nuiSkeletonSetTrackedSkeletonsInfo = new EventDescriptor(92, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 57, -9223372036854775807L);
    private static EventDescriptor nuiSetFrameEndEventInfo = new EventDescriptor(93, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 48, -9223372036854775807L);
    private static EventDescriptor nuiImageStreamGetNextFrameInfo = new EventDescriptor(94, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 18, -9223372036854775807L);
    private static EventDescriptor nuiSkeletonGetNextFrameInfo = new EventDescriptor(95, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 49, -9223372036854775807L);
    private static EventDescriptor nuiDeviceConnectionIdInfo = new EventDescriptor(97, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 50, -9223372036854775807L);
    private static EventDescriptor nuiUniqueIdInfo = new EventDescriptor(96, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 51, -9223372036854775807L);
    private static EventDescriptor nuiCreateSensorByIndexInfo = new EventDescriptor(98, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 52, -9223372036854775807L);
    private static EventDescriptor nuiCreateSensorByIdInfo = new EventDescriptor(99, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 53, -9223372036854775807L);
    private static EventDescriptor nuiImageStreamSetImageFrameFlagsInfo = new EventDescriptor(100, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 54, -9223372036854775807L);
    private static EventDescriptor nuiAudioBeamGetBeamInfo = new EventDescriptor(101, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 58, -9223372036854775807L);
    private static EventDescriptor nuiAudioBeamGetPositionInfo = new EventDescriptor(102, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 59, -9223372036854775807L);
    private static EventDescriptor nuiAudioBeamSetBeamInfo = new EventDescriptor(103, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 60, -9223372036854775807L);
    private static EventDescriptor nuiGetAudioSourceInfo = new EventDescriptor(104, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 61, -9223372036854775807L);
    private static EventDescriptor audioDmoSetPropertyInfo = new EventDescriptor(105, (byte) 0, (byte) 16, (byte) 4, (byte) 0, 65, -9223372036854775807L);
    private static EventDescriptor managedKinectSensorStartInfo = new EventDescriptor(106, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 66, -9223372036854775807L);
    private static EventDescriptor managedImageStreamDisabledInfo = new EventDescriptor(107, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 67, -9223372036854775807L);
    private static EventDescriptor managedFrameReadyEventAddedInfo = new EventDescriptor(108, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 68, -9223372036854775807L);
    private static EventDescriptor managedFrameReadyEventRemovedInfo = new EventDescriptor(109, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 69, -9223372036854775807L);
    private static EventDescriptor managedOpenNextFrameInfo = new EventDescriptor(110, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 70, -9223372036854775807L);
    private static EventDescriptor managedOpenFrameFromEventInfo = new EventDescriptor(111, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 71, -9223372036854775807L);
    private static EventDescriptor managedOpenFrameFromAllFramesEventInfo = new EventDescriptor(112, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 72, -9223372036854775807L);
    private static EventDescriptor managedAllFramesReadyEventAddedInfo = new EventDescriptor(113, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 73, -9223372036854775807L);
    private static EventDescriptor managedAllFramesReadyEventRemovedInfo = new EventDescriptor(114, (byte) 0, (byte) 16, (byte) 5, (byte) 0, 74, -9223372036854775807L);
    private static EventDescriptor mapColorFrameToDepthFrameStart = new EventDescriptor(115, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 75, long.MinValue);
    private static EventDescriptor mapColorFrameToDepthFrameEnd = new EventDescriptor(116, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 75, long.MinValue);
    private static EventDescriptor mapColorFrameToDepthFrame_InterpolateGapsStart = new EventDescriptor(117, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 76, long.MinValue);
    private static EventDescriptor mapColorFrameToDepthFrame_InterpolateGapsEnd = new EventDescriptor(118, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 76, long.MinValue);
    private static EventDescriptor mapColorFrameToSkeletonFrameStart = new EventDescriptor(119, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 77, long.MinValue);
    private static EventDescriptor mapColorFrameToSkeletonFrameEnd = new EventDescriptor(120, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 77, long.MinValue);
    private static EventDescriptor mapDepthFrameToSkeletonFrameStart = new EventDescriptor(121, (byte) 0, (byte) 16, (byte) 4, (byte) 1, 78, long.MinValue);
    private static EventDescriptor mapDepthFrameToSkeletonFrameEnd = new EventDescriptor(122, (byte) 0, (byte) 16, (byte) 4, (byte) 2, 78, long.MinValue);

    public static bool EventWriteGetSegmentationBegin(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.getSegmentationBegin, streamId, width, height, frameNumber);
    }

    public static bool EventWriteGetSegmentationEnd(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.getSegmentationEnd, streamId, width, height, frameNumber);
    }

    public static bool EventWriteProcessInputBufferBegin() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.processInputBufferBegin);

    public static bool EventWriteProcessInputBufferEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.processInputBufferEnd);

    public static bool EventWriteNotifyFrameReadyStart(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.notifyFrameReadyStart, streamId, width, height, frameNumber);
    }

    public static bool EventWriteNotifyFrameReadyEnd(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.notifyFrameReadyEnd, streamId, width, height, frameNumber);
    }

    public static bool EventWriteProcessFrameStart(IntPtr frameOverlapId, uint streamId) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateProcessFrameData(ref KinectEtwProvider.processFrameStart, frameOverlapId, streamId);

    public static bool EventWriteProcessFrameEnd(IntPtr frameOverlapId, uint streamId) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateProcessFrameData(ref KinectEtwProvider.processFrameEnd, frameOverlapId, streamId);

    public static bool EventWriteGetSkeletonsBegin(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.getSkeletonsBegin, streamId, width, height, frameNumber);
    }

    public static bool EventWriteGetSkeletonsEnd(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.getSkeletonsEnd, streamId, width, height, frameNumber);
    }

    public static bool EventWriteUnpackBayerStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.unpackBayerStart);

    public static bool EventWriteUnpackBayerEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.unpackBayerEnd);

    public static bool EventWriteFrameStart(uint streamType, int bufferIndex, int frameNumber) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameLifecycleData(ref KinectEtwProvider.frameStart, streamType, bufferIndex, frameNumber);

    public static bool EventWriteFrameEnd(uint streamType, int bufferIndex, int frameNumber) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameLifecycleData(ref KinectEtwProvider.frameEnd, streamType, bufferIndex, frameNumber);

    public static bool EventWriteMapDepthFrameToColorFrameStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapDepthFrameToColorFrameStart);

    public static bool EventWriteMapDepthFrameToColorFrameEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapDepthFrameToColorFrameEnd);

    public static bool EventWriteMapDepthToColorImagePointStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapDepthToColorImagePointStart);

    public static bool EventWriteMapDepthToColorImagePointEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapDepthToColorImagePointEnd);

    public static bool EventWriteMapSkeletonPointToColorStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapSkeletonPointToColorStart);

    public static bool EventWriteMapSkeletonPointToColorEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapSkeletonPointToColorEnd);

    public static bool EventWriteMapSkeletonPointToDepthStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapSkeletonPointToDepthStart);

    public static bool EventWriteMapSkeletonPointToDepthEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapSkeletonPointToDepthEnd);

    public static bool EventWriteMapDepthToSkeletonPointStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapDepthToSkeletonPointStart);

    public static bool EventWriteMapDepthToSkeletonPointEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapDepthToSkeletonPointEnd);

    public static bool EventWriteManagedNuiTransformDepthImageToSkeletonStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.managedNuiTransformDepthImageToSkeletonStart);

    public static bool EventWriteManagedNuiTransformDepthImageToSkeletonEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.managedNuiTransformDepthImageToSkeletonEnd);

    public static bool EventWriteManagedNuiTransformSkeletonToDepthImageStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.managedNuiTransformSkeletonToDepthImageStart);

    public static bool EventWriteManagedNuiTransformSkeletonToDepthImageEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.managedNuiTransformSkeletonToDepthImageEnd);

    public static bool EventWriteNuiImageGetColorPixelCoordinatesFromDepthPixelAtResolutionStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiImageGetColorPixelCoordinatesFromDepthPixelAtResolutionStart);

    public static bool EventWriteNuiImageGetColorPixelCoordinatesFromDepthPixelAtResolutionEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiImageGetColorPixelCoordinatesFromDepthPixelAtResolutionEnd);

    public static bool EventWriteNuiImageGetColorPixelCoordinateFrameFromDepthPixelFrameAtResolutionStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiImageGetColorPixelCoordinateFrameFromDepthPixelFrameAtResolutionStart);

    public static bool EventWriteNuiImageGetColorPixelCoordinateFrameFromDepthPixelFrameAtResolutionEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiImageGetColorPixelCoordinateFrameFromDepthPixelFrameAtResolutionEnd);

    public static bool EventWriteNuiImageStreamOpenStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiImageStreamOpenStart);

    public static bool EventWriteNuiImageStreamOpenEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiImageStreamOpenEnd);

    public static bool EventWriteNuiImageStreamGetNextFrameStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiImageStreamGetNextFrameStart);

    public static bool EventWriteNuiImageStreamGetNextFrameEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiImageStreamGetNextFrameEnd);

    public static bool EventWriteNuiImageStreamReleaseFrameStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiImageStreamReleaseFrameStart);

    public static bool EventWriteNuiImageStreamReleaseFrameEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiImageStreamReleaseFrameEnd);

    public static bool EventWriteNuiInitializeStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiInitializeStart);

    public static bool EventWriteNuiInitializeEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiInitializeEnd);

    public static bool EventWriteAllFramesReady(
      long colorFrameTimestamp,
      long depthFrameTimestamp,
      long skeletonFrameTimestamp)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateAllFramesReadyData(ref KinectEtwProvider.allFramesReady, colorFrameTimestamp, depthFrameTimestamp, skeletonFrameTimestamp);
    }

    public static bool EventWriteColorFrameReady(long timestamp) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameReadyData(ref KinectEtwProvider.colorFrameReady, timestamp);

    public static bool EventWriteDepthFrameReady(long timestamp) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameReadyData(ref KinectEtwProvider.depthFrameReady, timestamp);

    public static bool EventWriteSkeletonFrameReady(long timestamp) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameReadyData(ref KinectEtwProvider.skeletonFrameReady, timestamp);

    public static bool EventWriteStopDeviceStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.stopDeviceStart);

    public static bool EventWriteStopDeviceEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.stopDeviceEnd);

    public static bool EventWriteSingleStreamStopStart(uint streamType) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateStreamTypeOnly(ref KinectEtwProvider.singleStreamStopStart, streamType);

    public static bool EventWriteSingleStreamStopEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.singleStreamStopEnd);

    public static bool EventWriteDetectGenuineDeviceStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.detectGenuineDeviceStart);

    public static bool EventWriteDetectGenuineDeviceEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.detectGenuineDeviceEnd);

    public static bool EventWriteCreateDeviceStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.createDeviceStart);

    public static bool EventWriteCreateDeviceEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.createDeviceEnd);

    public static bool EventWriteReEnumerateDevicesStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.reEnumerateDevicesStart);

    public static bool EventWriteReEnumerateDevicesEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.reEnumerateDevicesEnd);

    public static bool EventWriteOutputStreamInitStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.outputStreamInitStart);

    public static bool EventWriteOutputStreamInitEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.outputStreamInitEnd);

    public static bool EventWriteSingleStreamStartStart(uint streamType) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateStreamTypeOnly(ref KinectEtwProvider.singleStreamStartStart, streamType);

    public static bool EventWriteSingleStreamStartEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.singleStreamStartEnd);

    public static bool EventWriteSkeletalTrackerInitializeStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.skeletalTrackerInitializeStart);

    public static bool EventWriteSkeletalTrackerInitializeEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.skeletalTrackerInitializeEnd);

    public static bool EventWriteKinectSensorInitializeStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.kinectSensorInitializeStart);

    public static bool EventWriteKinectSensorInitializeEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.kinectSensorInitializeEnd);

    public static bool EventWriteSkeletonStageBGRStart(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageBGRStart, streamId, width, height, frameNumber);
    }

    public static bool EventWriteSkeletonStageBGREnd(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageBGREnd, streamId, width, height, frameNumber);
    }

    public static bool EventWriteSkeletonStageCDRPStart(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageCDRPStart, streamId, width, height, frameNumber);
    }

    public static bool EventWriteSkeletonStageCDRPEnd(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageCDRPEnd, streamId, width, height, frameNumber);
    }

    public static bool EventWriteSkeletonStageHeadDetectorStart(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageHeadDetectorStart, streamId, width, height, frameNumber);
    }

    public static bool EventWriteSkeletonStageHeadDetectorEnd(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageHeadDetectorEnd, streamId, width, height, frameNumber);
    }

    public static bool EventWriteSkeletonStageExemplarStart(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageExemplarStart, streamId, width, height, frameNumber);
    }

    public static bool EventWriteSkeletonStageExemplarEnd(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageExemplarEnd, streamId, width, height, frameNumber);
    }

    public static bool EventWriteSkeletonStageCentroidsStart(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageCentroidsStart, streamId, width, height, frameNumber);
    }

    public static bool EventWriteSkeletonStageCentroidsEnd(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageCentroidsEnd, streamId, width, height, frameNumber);
    }

    public static bool EventWriteSkeletonStageModelFittingStart(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageModelFittingStart, streamId, width, height, frameNumber);
    }

    public static bool EventWriteSkeletonStageModelFittingEnd(
      uint streamId,
      ulong width,
      ulong height,
      ulong frameNumber)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameData(ref KinectEtwProvider.skeletonStageModelFittingEnd, streamId, width, height, frameNumber);
    }

    public static bool EventWriteFrameRequestDropped() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.frameRequestDropped);

    public static bool EventWriteUnexpectedFrameRequestError() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.unexpectedFrameRequestError);

    public static bool EventWriteFrameStateChanged(uint streamType, int frameNumber, uint state) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameStateData(ref KinectEtwProvider.frameStateChanged, streamType, frameNumber, state);

    public static bool EventWriteFrameDropped(uint streamType, int frameNumber) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateFrameDroppedData(ref KinectEtwProvider.frameDropped, streamType, frameNumber);

    public static bool EventWriteNuiInitializeInfo(string deviceId, string processName, uint flags) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateNuiInitializeData(ref KinectEtwProvider.nuiInitializeInfo, deviceId, processName, flags);

    public static bool EventWriteNuiImageStreamOpenInfo(
      IntPtr hStream,
      uint imageType,
      uint imageResolution,
      uint imageFrameFlags,
      IntPtr hNextFrameEvent)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateNuiImageStreamOpenData(ref KinectEtwProvider.nuiImageStreamOpenInfo, hStream, imageType, imageResolution, imageFrameFlags, hNextFrameEvent);
    }

    public static bool EventWriteNuiShutdownTrace() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiShutdownTrace);

    public static bool EventWriteInternalNuiShutdownTrace() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.internalNuiShutdownTrace);

    public static bool EventWriteNuiSkeletonTrackingEnableTrace(
      IntPtr hNextFrameEvent,
      uint dwFlags)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateNuiSkeletonTrackingData(ref KinectEtwProvider.nuiSkeletonTrackingEnableTrace, hNextFrameEvent, dwFlags);
    }

    public static bool EventWriteNuiSkeletonTrackingDisableTrace() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiSkeletonTrackingDisableTrace);

    public static bool EventWriteNuiTransformSmoothInfo(
      float smoothing,
      float correction,
      float prediction,
      float jitterRadius,
      float maxDeviationRadius)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateNuiTransformSmoothParams(ref KinectEtwProvider.nuiTransformSmoothInfo, smoothing, correction, prediction, jitterRadius, maxDeviationRadius);
    }

    public static bool EventWriteNuiSkeletonSetTrackedSkeletonsInfo(uint skeleton1, uint skeleton2) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateNuiSkeletonSetTrackedSkeletonsData(ref KinectEtwProvider.nuiSkeletonSetTrackedSkeletonsInfo, skeleton1, skeleton2);

    public static bool EventWriteNuiSetFrameEndEventInfo(IntPtr hEvent, uint dwFrameEventFlag) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateNuiSetFrameEndEventData(ref KinectEtwProvider.nuiSetFrameEndEventInfo, hEvent, dwFrameEventFlag);

    public static bool EventWriteNuiImageStreamGetNextFrameInfo(
      IntPtr hStream,
      uint dwMillisecondsToWait)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateNuiImageStreamGetNextFrameData(ref KinectEtwProvider.nuiImageStreamGetNextFrameInfo, hStream, dwMillisecondsToWait);
    }

    public static bool EventWriteNuiSkeletonGetNextFrameInfo(uint dwMillisecondsToWait) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateNuiSkeletonGetNextFrameData(ref KinectEtwProvider.nuiSkeletonGetNextFrameInfo, dwMillisecondsToWait);

    public static bool EventWriteNuiDeviceConnectionIdInfo(string kinectId) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateKinectIdStringData(ref KinectEtwProvider.nuiDeviceConnectionIdInfo, kinectId);

    public static bool EventWriteNuiUniqueIdInfo(string kinectId) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateKinectIdStringData(ref KinectEtwProvider.nuiUniqueIdInfo, kinectId);

    public static bool EventWriteNuiCreateSensorByIndexInfo(int index) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateNuiCreateSensorByIndexData(ref KinectEtwProvider.nuiCreateSensorByIndexInfo, index);

    public static bool EventWriteNuiCreateSensorByIdInfo(string strInstanceId) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateNuiCreateSensorByIdData(ref KinectEtwProvider.nuiCreateSensorByIdInfo, strInstanceId);

    public static bool EventWriteNuiImageStreamSetImageFrameFlagsInfo(
      IntPtr hStream,
      uint dwImageFrameFlags)
    {
      return !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateNuiImageStreamSetImageFrameFlagsData(ref KinectEtwProvider.nuiImageStreamSetImageFrameFlagsInfo, hStream, dwImageFrameFlags);
    }

    public static bool EventWriteNuiAudioBeamGetBeamInfo(double dAngle) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateAudioAngleData(ref KinectEtwProvider.nuiAudioBeamGetBeamInfo, dAngle);

    public static bool EventWriteNuiAudioBeamGetPositionInfo(double dAngle) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateAudioAngleData(ref KinectEtwProvider.nuiAudioBeamGetPositionInfo, dAngle);

    public static bool EventWriteNuiAudioBeamSetBeamInfo(double dAngle) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateAudioAngleData(ref KinectEtwProvider.nuiAudioBeamSetBeamInfo, dAngle);

    public static bool EventWriteNuiGetAudioSourceInfo() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.nuiGetAudioSourceInfo);

    public static bool EventWriteAudioDmoSetPropertyInfo(uint pid, uint value) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateAudioPropertyData(ref KinectEtwProvider.audioDmoSetPropertyInfo, pid, value);

    public static bool EventWriteManagedKinectSensorStartInfo(string deviceID) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateKinectSensorStartData(ref KinectEtwProvider.managedKinectSensorStartInfo, deviceID);

    public static bool EventWriteManagedImageStreamDisabledInfo(int type) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateManagedImageStreamData(ref KinectEtwProvider.managedImageStreamDisabledInfo, type);

    public static bool EventWriteManagedFrameReadyEventAddedInfo(int type) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateManagedFramesReadyRegistrationData(ref KinectEtwProvider.managedFrameReadyEventAddedInfo, type);

    public static bool EventWriteManagedFrameReadyEventRemovedInfo(int type) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateManagedFramesReadyRegistrationData(ref KinectEtwProvider.managedFrameReadyEventRemovedInfo, type);

    public static bool EventWriteManagedOpenNextFrameInfo(int streamType, int millisecondsWait) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateManagedOpenNextFrameData(ref KinectEtwProvider.managedOpenNextFrameInfo, streamType, millisecondsWait);

    public static bool EventWriteManagedOpenFrameFromEventInfo(int type) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateManagedImageStreamData(ref KinectEtwProvider.managedOpenFrameFromEventInfo, type);

    public static bool EventWriteManagedOpenFrameFromAllFramesEventInfo(int type) => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateManagedImageStreamData(ref KinectEtwProvider.managedOpenFrameFromAllFramesEventInfo, type);

    public static bool EventWriteManagedAllFramesReadyEventAddedInfo() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.managedAllFramesReadyEventAddedInfo);

    public static bool EventWriteManagedAllFramesReadyEventRemovedInfo() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.managedAllFramesReadyEventRemovedInfo);

    public static bool EventWriteMapColorFrameToDepthFrameStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapColorFrameToDepthFrameStart);

    public static bool EventWriteMapColorFrameToDepthFrameEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapColorFrameToDepthFrameEnd);

    public static bool EventWriteMapColorFrameToDepthFrame_InterpolateGapsStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapColorFrameToDepthFrame_InterpolateGapsStart);

    public static bool EventWriteMapColorFrameToDepthFrame_InterpolateGapsEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapColorFrameToDepthFrame_InterpolateGapsEnd);

    public static bool EventWriteMapColorFrameToSkeletonFrameStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapColorFrameToSkeletonFrameStart);

    public static bool EventWriteMapColorFrameToSkeletonFrameEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapColorFrameToSkeletonFrameEnd);

    public static bool EventWriteMapDepthFrameToSkeletonFrameStart() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapDepthFrameToSkeletonFrameStart);

    public static bool EventWriteMapDepthFrameToSkeletonFrameEnd() => !KinectEtwProvider.Provider.IsEnabled() || KinectEtwProvider.Provider.TemplateEventDescriptor(ref KinectEtwProvider.mapDepthFrameToSkeletonFrameEnd);
  }
}
