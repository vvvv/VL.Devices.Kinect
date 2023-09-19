// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.CoordinateMapper
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Microsoft.Kinect
{
  public sealed class CoordinateMapper : INotifyPropertyChanged
  {
    private readonly NuiCoordinateMapper _coordinateMapper;

    public CoordinateMapper(IEnumerable<byte> parameters)
    {
      this._coordinateMapper = new NuiCoordinateMapper(parameters.ToArray<byte>());
      this._coordinateMapper.RelationalParametersChanged += new EventHandler<EventArgs>(this.CoordinateMapper_RelationalParametersChanged);
    }

    private void CoordinateMapper_RelationalParametersChanged(object sender, EventArgs e)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs("ColorToDepthRelationalParameters"));
    }

    public ReadOnlyCollection<byte> ColorToDepthRelationalParameters => new ReadOnlyCollection<byte>((IList<byte>) ((IEnumerable<byte>) this._coordinateMapper.GetColorToDepthRelationalParameters()).ToList<byte>());

    public event PropertyChangedEventHandler PropertyChanged;

    public CoordinateMapper(KinectSensor sensor)
    {
      this._coordinateMapper = sensor != null ? sensor.NuiSensor.NuiGetCoordinateMapper() : throw new ArgumentNullException(nameof (sensor));
      this._coordinateMapper.RelationalParametersChanged += new EventHandler<EventArgs>(this.CoordinateMapper_RelationalParametersChanged);
    }

    private static _Vector4 ManagedNuiTransformDepthImageToSkeleton(
      float fDepthX,
      float fDepthY,
      int depthPixelDatum)
    {
      KinectEtwProvider.EventWriteManagedNuiTransformDepthImageToSkeletonStart();
      float num1 = (float) depthPixelDatum / 1000f;
      float num2 = (float) ((double) (fDepthX - 0.5f) * (0.003501000115647912 * (double) num1) * 320.0);
      float num3 = (float) ((double) (0.5f - fDepthY) * (0.003501000115647912 * (double) num1) * 240.0);
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
      if ((double) vPoint.z > 1.4012984643248171E-45)
      {
        pfDepthX = (float) (0.5 + (double) vPoint.x * 285.6300048828125 / ((double) vPoint.z * 320.0));
        pfDepthY = (float) (0.5 - (double) vPoint.y * 285.6300048828125 / ((double) vPoint.z * 240.0));
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

    internal static ImageType ImageTypeFromColorImageFormat(ColorImageFormat colorImageFormat) => colorImageFormat != ColorImageFormat.InfraredResolution640x480Fps30 ? ImageType.Color : ImageType.ColorInfrared;

    public void MapColorFrameToDepthFrame(
      ColorImageFormat colorImageFormat,
      DepthImageFormat depthImageFormat,
      DepthImagePixel[] depthPixels,
      DepthImagePoint[] depthPoints)
    {
      ImageType eColorType = CoordinateMapper.ImageTypeFromColorImageFormat(colorImageFormat);
      if (colorImageFormat == ColorImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (colorImageFormat));
      if (depthImageFormat == DepthImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (depthImageFormat));
      if (depthPixels == null)
        throw new ArgumentNullException(nameof (depthPixels));
      if (depthPixels.Length != DepthImageStream.LookUpPixelDataLength(depthImageFormat))
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (depthPixels));
      if (depthPoints == null)
        throw new ArgumentNullException(nameof (depthPoints));
      if (depthPoints.Length != ColorImageStream.LookUpPixelDataLength(colorImageFormat))
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (depthPoints));
      this._coordinateMapper.MapColorFrameToDepthFrame((_NUI_IMAGE_TYPE) eColorType, (_NUI_IMAGE_RESOLUTION) ColorImageStream.LookUpImageResolution(colorImageFormat), (_NUI_IMAGE_RESOLUTION) DepthImageStream.LookUpImageResolution(depthImageFormat), depthPixels, depthPoints);
    }

    public void MapColorFrameToSkeletonFrame(
      ColorImageFormat colorImageFormat,
      DepthImageFormat depthImageFormat,
      DepthImagePixel[] depthPixels,
      SkeletonPoint[] skeletonPoints)
    {
      ImageType eColorType = CoordinateMapper.ImageTypeFromColorImageFormat(colorImageFormat);
      if (colorImageFormat == ColorImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (colorImageFormat));
      if (depthImageFormat == DepthImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (depthImageFormat));
      if (depthPixels == null)
        throw new ArgumentNullException(nameof (depthPixels));
      if (depthPixels.Length != DepthImageStream.LookUpPixelDataLength(depthImageFormat))
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (depthPixels));
      if (skeletonPoints == null)
        throw new ArgumentNullException(nameof (skeletonPoints));
      if (skeletonPoints.Length != ColorImageStream.LookUpPixelDataLength(colorImageFormat))
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (skeletonPoints));
      this._coordinateMapper.MapColorFrameToSkeletonFrame((_NUI_IMAGE_TYPE) eColorType, (_NUI_IMAGE_RESOLUTION) ColorImageStream.LookUpImageResolution(colorImageFormat), (_NUI_IMAGE_RESOLUTION) DepthImageStream.LookUpImageResolution(depthImageFormat), depthPixels, skeletonPoints);
    }

    public void MapDepthFrameToColorFrame(
      DepthImageFormat depthImageFormat,
      DepthImagePixel[] depthPixels,
      ColorImageFormat colorImageFormat,
      ColorImagePoint[] colorPoints)
    {
      ImageType eColorType = CoordinateMapper.ImageTypeFromColorImageFormat(colorImageFormat);
      if (depthImageFormat == DepthImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (depthImageFormat));
      if (depthPixels == null)
        throw new ArgumentNullException(nameof (depthPixels));
      if (depthPixels.Length != DepthImageStream.LookUpPixelDataLength(depthImageFormat))
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (depthPixels));
      if (colorImageFormat == ColorImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (colorImageFormat));
      if (colorPoints == null)
        throw new ArgumentNullException(nameof (colorPoints));
      if (colorPoints.Length != depthPixels.Length)
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (colorPoints));
      KinectEtwProvider.EventWriteMapDepthFrameToColorFrameStart();
      this._coordinateMapper.MapDepthFrameToColorFrame((_NUI_IMAGE_RESOLUTION) DepthImageStream.LookUpImageResolution(depthImageFormat), depthPixels, (_NUI_IMAGE_TYPE) eColorType, (_NUI_IMAGE_RESOLUTION) ColorImageStream.LookUpImageResolution(colorImageFormat), colorPoints);
      KinectEtwProvider.EventWriteMapDepthFrameToColorFrameEnd();
    }

    public void MapDepthFrameToSkeletonFrame(
      DepthImageFormat depthImageFormat,
      DepthImagePixel[] depthPixels,
      SkeletonPoint[] skeletonPoints)
    {
      if (depthImageFormat == DepthImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (depthImageFormat));
      if (depthPixels == null)
        throw new ArgumentNullException(nameof (depthPixels));
      if (depthPixels.Length != DepthImageStream.LookUpPixelDataLength(depthImageFormat))
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (depthPixels));
      if (skeletonPoints == null)
        throw new ArgumentNullException(nameof (skeletonPoints));
      if (depthPixels.Length != skeletonPoints.Length)
        throw new ArgumentException(Resources.PixelBufferIncorrectLength, nameof (skeletonPoints));
      this._coordinateMapper.MapDepthFrameToSkeletonFrame((_NUI_IMAGE_RESOLUTION) DepthImageStream.LookUpImageResolution(depthImageFormat), depthPixels, skeletonPoints);
    }

    public ColorImagePoint MapDepthPointToColorPoint(
      DepthImageFormat depthImageFormat,
      DepthImagePoint depthPoint,
      ColorImageFormat colorImageFormat)
    {
      ImageType eColorType = CoordinateMapper.ImageTypeFromColorImageFormat(colorImageFormat);
      if (depthImageFormat == DepthImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (depthImageFormat));
      if (colorImageFormat == ColorImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (colorImageFormat));
      KinectEtwProvider.EventWriteMapDepthToColorImagePointStart();
      _NUI_DEPTH_IMAGE_POINT depthPoint1 = new _NUI_DEPTH_IMAGE_POINT()
      {
        x = depthPoint.X,
        y = depthPoint.Y,
        depth = depthPoint.Depth
      };
      _NUI_COLOR_IMAGE_POINT colorPoint;
      this._coordinateMapper.MapDepthPointToColorPoint((_NUI_IMAGE_RESOLUTION) DepthImageStream.LookUpImageResolution(depthImageFormat), ref depthPoint1, (_NUI_IMAGE_TYPE) eColorType, (_NUI_IMAGE_RESOLUTION) ColorImageStream.LookUpImageResolution(colorImageFormat), out colorPoint);
      KinectEtwProvider.EventWriteMapDepthToColorImagePointEnd();
      return new ColorImagePoint()
      {
        X = colorPoint.x,
        Y = colorPoint.y
      };
    }

    public SkeletonPoint MapDepthPointToSkeletonPoint(
      DepthImageFormat depthImageFormat,
      DepthImagePoint depthImagePoint)
    {
      if (depthImageFormat == DepthImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (depthImageFormat));
      KinectEtwProvider.EventWriteMapDepthToSkeletonPointStart();
      int width;
      int height;
      ImageStream.ResolutionToHeightWidth(DepthImageStream.LookUpImageResolution(depthImageFormat), out width, out height);
      _Vector4 skeleton = CoordinateMapper.ManagedNuiTransformDepthImageToSkeleton((float) depthImagePoint.X / (float) width, (float) depthImagePoint.Y / (float) height, depthImagePoint.Depth);
      SkeletonPoint skeletonPoint = new SkeletonPoint()
      {
        X = skeleton.x,
        Y = skeleton.y,
        Z = skeleton.z
      };
      KinectEtwProvider.EventWriteMapDepthToSkeletonPointEnd();
      return skeletonPoint;
    }

    public ColorImagePoint MapSkeletonPointToColorPoint(
      SkeletonPoint skeletonPoint,
      ColorImageFormat colorImageFormat)
    {
      if (colorImageFormat == ColorImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (colorImageFormat));
      KinectEtwProvider.EventWriteMapSkeletonPointToColorStart();
      _Vector4 vPoint;
      vPoint.x = skeletonPoint.X;
      vPoint.y = skeletonPoint.Y;
      vPoint.z = skeletonPoint.Z;
      vPoint.w = 1f;
      float pfDepthX;
      float pfDepthY;
      float pfDepthValue;
      CoordinateMapper.ManagedNuiTransformSkeletonToDepthImage(vPoint, out pfDepthX, out pfDepthY, out pfDepthValue);
      ColorImagePoint colorPoint = this.MapDepthPointToColorPoint(DepthImageFormat.Resolution640x480Fps30, new DepthImagePoint()
      {
        X = (int) ((double) pfDepthX * 640.0),
        Y = (int) ((double) pfDepthY * 480.0),
        Depth = (int) pfDepthValue
      }, colorImageFormat);
      KinectEtwProvider.EventWriteMapSkeletonPointToColorEnd();
      return colorPoint;
    }

    public DepthImagePoint MapSkeletonPointToDepthPoint(
      SkeletonPoint skeletonPoint,
      DepthImageFormat depthImageFormat)
    {
      if (depthImageFormat == DepthImageFormat.Undefined)
        throw new ArgumentException(Resources.ImageFormatNotSupported, nameof (depthImageFormat));
      KinectEtwProvider.EventWriteMapSkeletonPointToDepthStart();
      _Vector4 vPoint;
      vPoint.x = skeletonPoint.X;
      vPoint.y = skeletonPoint.Y;
      vPoint.z = skeletonPoint.Z;
      vPoint.w = 1f;
      float pfDepthX;
      float pfDepthY;
      float pfDepthValue;
      CoordinateMapper.ManagedNuiTransformSkeletonToDepthImage(vPoint, out pfDepthX, out pfDepthY, out pfDepthValue);
      int width;
      int height;
      ImageStream.ResolutionToHeightWidth(DepthImageStream.LookUpImageResolution(depthImageFormat), out width, out height);
      DepthImagePoint depthPoint = new DepthImagePoint()
      {
        X = (int) ((double) pfDepthX * (double) width + 0.5),
        Y = (int) ((double) pfDepthY * (double) height + 0.5),
        Depth = (int) ((double) pfDepthValue + 0.5)
      };
      KinectEtwProvider.EventWriteMapSkeletonPointToDepthEnd();
      return depthPoint;
    }
  }
}
