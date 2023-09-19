// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.NuiColorCameraSettings
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;

namespace Microsoft.Kinect
{
  internal class NuiColorCameraSettings
  {
    private readonly INuiColorCameraSettings _nuiColorCameraSettings;

    public NuiColorCameraSettings(INuiColorCameraSettings nuiColorCameraSettings) => this._nuiColorCameraSettings = nuiColorCameraSettings;

    public void SetAutoWhiteBalance(bool bAutoWhiteBalanceEnabled) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetAutoWhiteBalance(bAutoWhiteBalanceEnabled ? 1 : 0));

    public bool GetAutoWhiteBalance()
    {
      int pAutoWhiteBalanceEnabled;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetAutoWhiteBalance(out pAutoWhiteBalanceEnabled));
      return pAutoWhiteBalanceEnabled != 0;
    }

    public void SetWhiteBalance(int whiteBalance) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetWhiteBalance(whiteBalance));

    public int GetWhiteBalance()
    {
      int pWhiteBalance;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetWhiteBalance(out pWhiteBalance));
      return pWhiteBalance;
    }

    public int GetMinWhiteBalance()
    {
      int pWhiteBalance;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMinWhiteBalance(out pWhiteBalance));
      return pWhiteBalance;
    }

    public int GetMaxWhiteBalance()
    {
      int pWhiteBalance;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMaxWhiteBalance(out pWhiteBalance));
      return pWhiteBalance;
    }

    public void SetContrast(double contrast) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetContrast(contrast));

    public double GetContrast()
    {
      double pContrast;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetContrast(out pContrast));
      return pContrast;
    }

    public double GetMinContrast()
    {
      double pContrast;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMinContrast(out pContrast));
      return pContrast;
    }

    public double GetMaxContrast()
    {
      double pContrast;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMaxContrast(out pContrast));
      return pContrast;
    }

    public void SetHue(double hue) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetHue(hue));

    public double GetHue()
    {
      double pHue;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetHue(out pHue));
      return pHue;
    }

    public double GetMinHue()
    {
      double pHue;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMinHue(out pHue));
      return pHue;
    }

    public double GetMaxHue()
    {
      double pHue;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMaxHue(out pHue));
      return pHue;
    }

    public void SetSaturation(double saturation) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetSaturation(saturation));

    public double GetSaturation()
    {
      double pSaturation;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetSaturation(out pSaturation));
      return pSaturation;
    }

    public double GetMinSaturation()
    {
      double pSaturation;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMinSaturation(out pSaturation));
      return pSaturation;
    }

    public double GetMaxSaturation()
    {
      double pSaturation;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMaxSaturation(out pSaturation));
      return pSaturation;
    }

    public void SetGamma(double gamma) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetGamma(gamma));

    public double GetGamma()
    {
      double pGamma;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetGamma(out pGamma));
      return pGamma;
    }

    public double GetMinGamma()
    {
      double pGamma;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMinGamma(out pGamma));
      return pGamma;
    }

    public double GetMaxGamma()
    {
      double pGamma;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMaxGamma(out pGamma));
      return pGamma;
    }

    public void SetSharpness(double sharpness) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetSharpness(sharpness));

    public double GetSharpness()
    {
      double pSharpness;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetSharpness(out pSharpness));
      return pSharpness;
    }

    public double GetMinSharpness()
    {
      double pSharpness;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMinSharpness(out pSharpness));
      return pSharpness;
    }

    public double GetMaxSharpness()
    {
      double pSharpness;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMaxSharpness(out pSharpness));
      return pSharpness;
    }

    public void SetAutoExposure(bool bAutoExposureEnabled) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetAutoExposure(bAutoExposureEnabled ? 1 : 0));

    public bool GetAutoExposure()
    {
      int pAutoExposureEnabled;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetAutoExposure(out pAutoExposureEnabled));
      return pAutoExposureEnabled != 0;
    }

    public void SetExposureTime(double exposureTime) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetExposureTime(exposureTime));

    public double GetExposureTime()
    {
      double pExposureTime;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetExposureTime(out pExposureTime));
      return pExposureTime;
    }

    public double GetMinExposureTime()
    {
      double pExposureTime;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMinExposureTime(out pExposureTime));
      return pExposureTime;
    }

    public double GetMaxExposureTime()
    {
      double pExposureTime;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMaxExposureTime(out pExposureTime));
      return pExposureTime;
    }

    public void SetFrameInterval(double frameInterval) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetFrameInterval(frameInterval));

    public double GetFrameInterval()
    {
      double pFrameInterval;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetFrameInterval(out pFrameInterval));
      return pFrameInterval;
    }

    public double GetMinFrameInterval()
    {
      double pFrameInterval;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMinFrameInterval(out pFrameInterval));
      return pFrameInterval;
    }

    public double GetMaxFrameInterval()
    {
      double pFrameInterval;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMaxFrameInterval(out pFrameInterval));
      return pFrameInterval;
    }

    public void SetBrightness(double brightness) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetBrightness(brightness));

    public double GetBrightness()
    {
      double pBrightness;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetBrightness(out pBrightness));
      return pBrightness;
    }

    public double GetMinBrightness()
    {
      double pBrightness;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMinBrightness(out pBrightness));
      return pBrightness;
    }

    public double GetMaxBrightness()
    {
      double pBrightness;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMaxBrightness(out pBrightness));
      return pBrightness;
    }

    public void SetPowerLineFrequency(_NUI_POWER_LINE_FREQUENCY powerLineFrequency) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetPowerLineFrequency(powerLineFrequency));

    public _NUI_POWER_LINE_FREQUENCY GetPowerLineFrequency()
    {
      _NUI_POWER_LINE_FREQUENCY pPowerLineFrequency;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetPowerLineFrequency(out pPowerLineFrequency));
      return pPowerLineFrequency;
    }

    public void SetBacklightCompensationMode(
      _NUI_BACKLIGHT_COMPENSATION_MODE backlightCompensationMode)
    {
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetBacklightCompensationMode(backlightCompensationMode));
    }

    public _NUI_BACKLIGHT_COMPENSATION_MODE GetBacklightCompensationMode()
    {
      _NUI_BACKLIGHT_COMPENSATION_MODE pBacklightCompensationMode;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetBacklightCompensationMode(out pBacklightCompensationMode));
      return pBacklightCompensationMode;
    }

    public void SetGain(double gain) => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.SetGain(gain));

    public double GetGain()
    {
      double pGain;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetGain(out pGain));
      return pGain;
    }

    public double GetMinGain()
    {
      double pGain;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMinGain(out pGain));
      return pGain;
    }

    public double GetMaxGain()
    {
      double pGain;
      KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.GetMaxGain(out pGain));
      return pGain;
    }

    public void ResetCameraSettingsToDefault() => KinectExceptionHelper.CheckHr(this._nuiColorCameraSettings.ResetCameraSettingsToDefault());
  }
}
