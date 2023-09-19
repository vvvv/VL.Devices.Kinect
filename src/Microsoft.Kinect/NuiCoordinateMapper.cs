// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.NuiCoordinateMapper
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  internal class NuiCoordinateMapper : INuiCoordinateMapperParametersChangedEvent
  {
    private INuiCoordinateMapper _nuiCoordinateMapper;
    private ContextEventHandler<EventArgs> _relationalParametersChangedHandler;

    public NuiCoordinateMapper(INuiCoordinateMapper nuiCoordinateMapper)
    {
      this._nuiCoordinateMapper = nuiCoordinateMapper;
      this.Init();
    }

    public unsafe NuiCoordinateMapper(byte[] parameters)
    {
      fixed (byte* pMapperParameters = parameters)
        KinectExceptionHelper.CheckHr(NativeMethods.NuiCreateCoordinateMapperFromParameters((uint) parameters.Length, (IntPtr) (void*) pMapperParameters, out this._nuiCoordinateMapper));
      this.Init();
    }

    internal void Init()
    {
      this._relationalParametersChangedHandler = new ContextEventHandler<EventArgs>();
      KinectExceptionHelper.CheckHr(this._nuiCoordinateMapper.NotifyParametersChanged((INuiCoordinateMapperParametersChangedEvent) this));
    }

    public byte[] GetColorToDepthRelationalParameters()
    {
      IntPtr ppData = IntPtr.Zero;
      try
      {
        uint pDataByteCount;
        KinectExceptionHelper.CheckHr(this._nuiCoordinateMapper.GetColorToDepthRelationalParameters(out pDataByteCount, out ppData));
        byte[] destination = new byte[pDataByteCount];
        Marshal.Copy(ppData, destination, 0, (int) pDataByteCount);
        return destination;
      }
      finally
      {
        Marshal.FreeCoTaskMem(ppData);
      }
    }

    public event EventHandler<EventArgs> RelationalParametersChanged
    {
      add => this._relationalParametersChangedHandler.AddHandler(value);
      remove => this._relationalParametersChangedHandler.RemoveHandler(value);
    }

    public void MapDepthPointToColorPoint(
      _NUI_IMAGE_RESOLUTION eDepthResolution,
      ref _NUI_DEPTH_IMAGE_POINT depthPoint,
      _NUI_IMAGE_TYPE eColorType,
      _NUI_IMAGE_RESOLUTION eColorResolution,
      out _NUI_COLOR_IMAGE_POINT colorPoint)
    {
      KinectExceptionHelper.CheckHr(this._nuiCoordinateMapper.MapDepthPointToColorPoint(eDepthResolution, ref depthPoint, eColorType, eColorResolution, out colorPoint));
    }

    public unsafe void MapDepthFrameToColorFrame(
      _NUI_IMAGE_RESOLUTION eDepthResolution,
      DepthImagePixel[] depthPixels,
      _NUI_IMAGE_TYPE eColorType,
      _NUI_IMAGE_RESOLUTION eColorResolution,
      ColorImagePoint[] colorPoints)
    {
      fixed (DepthImagePixel* pDepthPixels = depthPixels)
        fixed (ColorImagePoint* pColorPoints = colorPoints)
          KinectExceptionHelper.CheckHr(this._nuiCoordinateMapper.MapDepthFrameToColorFrame(eDepthResolution, (uint) depthPixels.Length, (IntPtr) (void*) pDepthPixels, eColorType, eColorResolution, (uint) colorPoints.Length, (IntPtr) (void*) pColorPoints));
    }

    public unsafe void MapColorFrameToSkeletonFrame(
      _NUI_IMAGE_TYPE eColorType,
      _NUI_IMAGE_RESOLUTION eColorResolution,
      _NUI_IMAGE_RESOLUTION eDepthResolution,
      DepthImagePixel[] depthPixels,
      SkeletonPoint[] skeletonPoints)
    {
      fixed (DepthImagePixel* pDepthPixels = depthPixels)
        fixed (SkeletonPoint* pSkeletonPoints = skeletonPoints)
          KinectExceptionHelper.CheckHr(this._nuiCoordinateMapper.MapColorFrameToSkeletonFrame(eColorType, eColorResolution, eDepthResolution, (uint) depthPixels.Length, (IntPtr) (void*) pDepthPixels, (uint) skeletonPoints.Length, (IntPtr) (void*) pSkeletonPoints));
    }

    public unsafe void MapDepthFrameToSkeletonFrame(
      _NUI_IMAGE_RESOLUTION eDepthResolution,
      DepthImagePixel[] depthPixels,
      SkeletonPoint[] skeletonPoints)
    {
      fixed (DepthImagePixel* pDepthPixels = depthPixels)
        fixed (SkeletonPoint* pSkeletonPoints = skeletonPoints)
          KinectExceptionHelper.CheckHr(this._nuiCoordinateMapper.MapDepthFrameToSkeletonFrame(eDepthResolution, (uint) depthPixels.Length, (IntPtr) (void*) pDepthPixels, (uint) skeletonPoints.Length, (IntPtr) (void*) pSkeletonPoints));
    }

    public unsafe void MapColorFrameToDepthFrame(
      _NUI_IMAGE_TYPE eColorType,
      _NUI_IMAGE_RESOLUTION eColorResolution,
      _NUI_IMAGE_RESOLUTION eDepthResolution,
      DepthImagePixel[] depthPixels,
      DepthImagePoint[] depthPoints)
    {
      fixed (DepthImagePixel* pDepthPixels = depthPixels)
        fixed (DepthImagePoint* pDepthPoints = depthPoints)
          KinectExceptionHelper.CheckHr(this._nuiCoordinateMapper.MapColorFrameToDepthFrame(eColorType, eColorResolution, eDepthResolution, (uint) depthPixels.Length, (IntPtr) (void*) pDepthPixels, (uint) depthPoints.Length, (IntPtr) (void*) pDepthPoints));
    }

    int INuiCoordinateMapperParametersChangedEvent.Invoke()
    {
      this._relationalParametersChangedHandler.Invoke((object) this, new EventArgs());
      return 0;
    }
  }
}
