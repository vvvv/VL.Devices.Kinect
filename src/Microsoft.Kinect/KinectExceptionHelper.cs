// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.KinectExceptionHelper
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  internal static class KinectExceptionHelper
  {
    private const int E_NUI_HARDWARE_FEATURE_UNAVAILABLE = -2097086449;
    private const int E_NUI_NOTGENUINE = -2097086458;
    private const int E_NUI_NOTSUPPORTED = -2097086456;
    private const int E_NUI_BADINDEX = -2097085051;
    private const int E_WIN32_ERROR_REQUEST_ABORTED = -2147023661;
    private const int E_NUI_DEVICE_IN_USE = -2097086455;

    internal static void CheckHr(int hr)
    {
      if (hr >= 0)
        return;
      switch (hr)
      {
        case -2147023661:
          throw new UnauthorizedAccessException(Resources.UnauthorizedAccess);
        case -2097086458:
          throw new InvalidOperationException(Resources.DeviceNotGenuine);
        case -2097086456:
          throw new InvalidOperationException(Resources.DeviceNotSupported);
        case -2097086455:
          throw new IOException(Resources.KinectInUse);
        case -2097086449:
          throw new InvalidOperationException(Resources.HardwareFeatureUnavailable);
        case -2097085051:
          throw new InvalidOperationException(Resources.BadIndex);
        default:
          Exception exceptionForHr = Marshal.GetExceptionForHR(hr);
          if (exceptionForHr == null)
            break;
          throw new InvalidOperationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Resources.GenericException, (object) hr), exceptionForHr);
      }
    }
  }
}
