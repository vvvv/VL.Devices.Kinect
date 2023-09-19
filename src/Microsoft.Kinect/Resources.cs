// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Resources
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Kinect
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [DebuggerNonUserCode]
  [CompilerGenerated]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Microsoft.Kinect.Resources.resourceMan, (object) null))
          Microsoft.Kinect.Resources.resourceMan = new ResourceManager("Microsoft.Kinect.Resources", typeof (Microsoft.Kinect.Resources).Assembly);
        return Microsoft.Kinect.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => Microsoft.Kinect.Resources.resourceCulture;
      set => Microsoft.Kinect.Resources.resourceCulture = value;
    }

    internal static string BadIndex => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (BadIndex), Microsoft.Kinect.Resources.resourceCulture);

    internal static string CannotPollAndUseEvents => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (CannotPollAndUseEvents), Microsoft.Kinect.Resources.resourceCulture);

    internal static string CaptureAlreadyStarted => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (CaptureAlreadyStarted), Microsoft.Kinect.Resources.resourceCulture);

    internal static string CaptureNotStarted => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (CaptureNotStarted), Microsoft.Kinect.Resources.resourceCulture);

    internal static string ColorStreamMustBeEnabled => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (ColorStreamMustBeEnabled), Microsoft.Kinect.Resources.resourceCulture);

    internal static string DepthFilterMustImplementNativeInterface => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (DepthFilterMustImplementNativeInterface), Microsoft.Kinect.Resources.resourceCulture);

    internal static string DepthStreamMustBeEnabled => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (DepthStreamMustBeEnabled), Microsoft.Kinect.Resources.resourceCulture);

    internal static string DeviceNotGenuine => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (DeviceNotGenuine), Microsoft.Kinect.Resources.resourceCulture);

    internal static string DeviceNotSupported => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (DeviceNotSupported), Microsoft.Kinect.Resources.resourceCulture);

    internal static string ElevationIncorrect => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (ElevationIncorrect), Microsoft.Kinect.Resources.resourceCulture);

    internal static string FailedToGetDeviceName => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (FailedToGetDeviceName), Microsoft.Kinect.Resources.resourceCulture);

    internal static string GenericException => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (GenericException), Microsoft.Kinect.Resources.resourceCulture);

    internal static string HardwareFeatureUnavailable => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (HardwareFeatureUnavailable), Microsoft.Kinect.Resources.resourceCulture);

    internal static string ImageFormatNotSupported => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (ImageFormatNotSupported), Microsoft.Kinect.Resources.resourceCulture);

    internal static string ImageFrameNotDisposed => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (ImageFrameNotDisposed), Microsoft.Kinect.Resources.resourceCulture);

    internal static string IncorrectJointType => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (IncorrectJointType), Microsoft.Kinect.Resources.resourceCulture);

    internal static string InvalidBeamAngleMode => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (InvalidBeamAngleMode), Microsoft.Kinect.Resources.resourceCulture);

    internal static string InvalidEchoCancellationMode => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (InvalidEchoCancellationMode), Microsoft.Kinect.Resources.resourceCulture);

    internal static string KinectInUse => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (KinectInUse), Microsoft.Kinect.Resources.resourceCulture);

    internal static string KinectMustBeRunning => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (KinectMustBeRunning), Microsoft.Kinect.Resources.resourceCulture);

    internal static string KinectNotReady => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (KinectNotReady), Microsoft.Kinect.Resources.resourceCulture);

    internal static string KinectNotSupportedDeveloper => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (KinectNotSupportedDeveloper), Microsoft.Kinect.Resources.resourceCulture);

    internal static string KinectNotSupportedNonDeveloper => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (KinectNotSupportedNonDeveloper), Microsoft.Kinect.Resources.resourceCulture);

    internal static string NativeStreamCantBeZero => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (NativeStreamCantBeZero), Microsoft.Kinect.Resources.resourceCulture);

    internal static string NeedFeatureMode => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (NeedFeatureMode), Microsoft.Kinect.Resources.resourceCulture);

    internal static string NoDevicesFound => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (NoDevicesFound), Microsoft.Kinect.Resources.resourceCulture);

    internal static string NotSupported => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (NotSupported), Microsoft.Kinect.Resources.resourceCulture);

    internal static string PixelBufferIncorrectLength => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (PixelBufferIncorrectLength), Microsoft.Kinect.Resources.resourceCulture);

    internal static string SensorMustBeRunning => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (SensorMustBeRunning), Microsoft.Kinect.Resources.resourceCulture);

    internal static string SensorMustBeRunningForAudio => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (SensorMustBeRunningForAudio), Microsoft.Kinect.Resources.resourceCulture);

    internal static string SkeletonBufferIncorrectLength => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (SkeletonBufferIncorrectLength), Microsoft.Kinect.Resources.resourceCulture);

    internal static string SkeletonEngineMustBeEnabled => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (SkeletonEngineMustBeEnabled), Microsoft.Kinect.Resources.resourceCulture);

    internal static string SkeletonFrameNotDisposed => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (SkeletonFrameNotDisposed), Microsoft.Kinect.Resources.resourceCulture);

    internal static string UnauthorizedAccess => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (UnauthorizedAccess), Microsoft.Kinect.Resources.resourceCulture);

    internal static string UnexpectedDepthRange => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (UnexpectedDepthRange), Microsoft.Kinect.Resources.resourceCulture);

    internal static string UnexpectedUniqueDeviceName => Microsoft.Kinect.Resources.ResourceManager.GetString(nameof (UnexpectedUniqueDeviceName), Microsoft.Kinect.Resources.resourceCulture);
  }
}
