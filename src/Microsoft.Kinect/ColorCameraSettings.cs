// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.ColorCameraSettings
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System.ComponentModel;

namespace Microsoft.Kinect
{
  public sealed class ColorCameraSettings : INotifyPropertyChanged
  {
    private readonly NuiColorCameraSettings _colorCameraSettings;

    internal ColorCameraSettings(KinectSensor mainNui) => this._colorCameraSettings = mainNui.NuiSensor.NuiGetColorCameraSettings();

    public bool AutoExposure
    {
      get => this._colorCameraSettings.GetAutoExposure();
      set
      {
        if (value == this.AutoExposure)
          return;
        this._colorCameraSettings.SetAutoExposure(value);
        this.NotifyPropertyChanged(nameof (AutoExposure));
      }
    }

    public double ExposureTime
    {
      get => this._colorCameraSettings.GetExposureTime();
      set
      {
        if (value.Equals(this.ExposureTime))
          return;
        this._colorCameraSettings.SetExposureTime(value);
        this.NotifyPropertyChanged(nameof (ExposureTime));
      }
    }

    public double MinExposureTime => this._colorCameraSettings.GetMinExposureTime();

    public double MaxExposureTime => this._colorCameraSettings.GetMaxExposureTime();

    public double FrameInterval
    {
      get => this._colorCameraSettings.GetFrameInterval();
      set
      {
        if (value.Equals(this.FrameInterval))
          return;
        this._colorCameraSettings.SetFrameInterval(value);
        this.NotifyPropertyChanged(nameof (FrameInterval));
      }
    }

    public double MinFrameInterval => this._colorCameraSettings.GetMinFrameInterval();

    public double MaxFrameInterval => this._colorCameraSettings.GetMaxFrameInterval();

    public double Brightness
    {
      get => this._colorCameraSettings.GetBrightness();
      set
      {
        if (value.Equals(this.Brightness))
          return;
        this._colorCameraSettings.SetBrightness(value);
        this.NotifyPropertyChanged(nameof (Brightness));
      }
    }

    public double MinBrightness => this._colorCameraSettings.GetMinBrightness();

    public double MaxBrightness => this._colorCameraSettings.GetMaxBrightness();

    public BacklightCompensationMode BacklightCompensationMode
    {
      get => (BacklightCompensationMode) this._colorCameraSettings.GetBacklightCompensationMode();
      set
      {
        if (value == this.BacklightCompensationMode)
          return;
        this._colorCameraSettings.SetBacklightCompensationMode((_NUI_BACKLIGHT_COMPENSATION_MODE) value);
        this.NotifyPropertyChanged(nameof (BacklightCompensationMode));
      }
    }

    public PowerLineFrequency PowerLineFrequency
    {
      get => (PowerLineFrequency) this._colorCameraSettings.GetPowerLineFrequency();
      set
      {
        if (value == this.PowerLineFrequency)
          return;
        this._colorCameraSettings.SetPowerLineFrequency((_NUI_POWER_LINE_FREQUENCY) value);
        this.NotifyPropertyChanged(nameof (PowerLineFrequency));
      }
    }

    public double Gain
    {
      get => this._colorCameraSettings.GetGain();
      set
      {
        if (value.Equals(this.Gain))
          return;
        this._colorCameraSettings.SetGain(value);
        this.NotifyPropertyChanged(nameof (Gain));
      }
    }

    public double MinGain => this._colorCameraSettings.GetMinGain();

    public double MaxGain => this._colorCameraSettings.GetMaxGain();

    public bool AutoWhiteBalance
    {
      get => this._colorCameraSettings.GetAutoWhiteBalance();
      set
      {
        if (value == this.AutoWhiteBalance)
          return;
        this._colorCameraSettings.SetAutoWhiteBalance(value);
        this.NotifyPropertyChanged(nameof (AutoWhiteBalance));
      }
    }

    public int WhiteBalance
    {
      get => this._colorCameraSettings.GetWhiteBalance();
      set
      {
        if (value == this.WhiteBalance)
          return;
        this._colorCameraSettings.SetWhiteBalance(value);
        this.NotifyPropertyChanged(nameof (WhiteBalance));
      }
    }

    public int MinWhiteBalance => this._colorCameraSettings.GetMinWhiteBalance();

    public int MaxWhiteBalance => this._colorCameraSettings.GetMaxWhiteBalance();

    public double Contrast
    {
      get => this._colorCameraSettings.GetContrast();
      set
      {
        if (value.Equals(this.Contrast))
          return;
        this._colorCameraSettings.SetContrast(value);
        this.NotifyPropertyChanged(nameof (Contrast));
      }
    }

    public double MinContrast => this._colorCameraSettings.GetMinContrast();

    public double MaxContrast => this._colorCameraSettings.GetMaxContrast();

    public double Hue
    {
      get => this._colorCameraSettings.GetHue();
      set
      {
        if (value.Equals(this.Hue))
          return;
        this._colorCameraSettings.SetHue(value);
        this.NotifyPropertyChanged(nameof (Hue));
      }
    }

    public double MinHue => this._colorCameraSettings.GetMinHue();

    public double MaxHue => this._colorCameraSettings.GetMaxHue();

    public double Saturation
    {
      get => this._colorCameraSettings.GetSaturation();
      set
      {
        if (value.Equals(this.Saturation))
          return;
        this._colorCameraSettings.SetSaturation(value);
        this.NotifyPropertyChanged(nameof (Saturation));
      }
    }

    public double MinSaturation => this._colorCameraSettings.GetMinSaturation();

    public double MaxSaturation => this._colorCameraSettings.GetMaxSaturation();

    public double Gamma
    {
      get => this._colorCameraSettings.GetGamma();
      set
      {
        if (value.Equals(this.Gamma))
          return;
        this._colorCameraSettings.SetGamma(value);
        this.NotifyPropertyChanged(nameof (Gamma));
      }
    }

    public double MinGamma => this._colorCameraSettings.GetMinGamma();

    public double MaxGamma => this._colorCameraSettings.GetMaxGamma();

    public double Sharpness
    {
      get => this._colorCameraSettings.GetSharpness();
      set
      {
        if (value.Equals(this.Sharpness))
          return;
        this._colorCameraSettings.SetSharpness(value);
        this.NotifyPropertyChanged(nameof (Sharpness));
      }
    }

    public double MinSharpness => this._colorCameraSettings.GetMinSharpness();

    public double MaxSharpness => this._colorCameraSettings.GetMaxSharpness();

    public void ResetToDefault()
    {
      bool autoExposure = this.AutoExposure;
      bool autoWhiteBalance = this.AutoWhiteBalance;
      BacklightCompensationMode compensationMode = this.BacklightCompensationMode;
      double brightness = this.Brightness;
      double contrast = this.Contrast;
      double exposureTime = this.ExposureTime;
      double frameInterval = this.FrameInterval;
      double gain = this.Gain;
      double gamma = this.Gamma;
      double hue = this.Hue;
      PowerLineFrequency powerLineFrequency = this.PowerLineFrequency;
      double saturation = this.Saturation;
      double sharpness = this.Sharpness;
      int whiteBalance = this.WhiteBalance;
      this._colorCameraSettings.ResetCameraSettingsToDefault();
      this.CheckAndTriggerIfChanged(autoExposure, this.AutoExposure, "AutoExposure");
      this.CheckAndTriggerIfChanged(autoWhiteBalance, this.AutoWhiteBalance, "AutoWhiteBalance");
      this.CheckAndTriggerIfChanged(compensationMode, this.BacklightCompensationMode, "BacklightCompensationMode");
      this.CheckAndTriggerIfChanged(brightness, this.Brightness, "Brightness");
      this.CheckAndTriggerIfChanged(contrast, this.Contrast, "Contrast");
      this.CheckAndTriggerIfChanged(exposureTime, this.ExposureTime, "ExposureTime");
      this.CheckAndTriggerIfChanged(frameInterval, this.FrameInterval, "FrameInterval");
      this.CheckAndTriggerIfChanged(gain, this.Gain, "Gain");
      this.CheckAndTriggerIfChanged(gamma, this.Gamma, "Gamma");
      this.CheckAndTriggerIfChanged(hue, this.Hue, "Hue");
      this.CheckAndTriggerIfChanged(powerLineFrequency, this.PowerLineFrequency, "PowerLineFrequency");
      this.CheckAndTriggerIfChanged(saturation, this.Saturation, "Saturation");
      this.CheckAndTriggerIfChanged(sharpness, this.Sharpness, "Sharpness");
      this.CheckAndTriggerIfChanged((double) whiteBalance, (double) this.WhiteBalance, "WhiteBalance");
    }

    private void CheckAndTriggerIfChanged(double oldValue, double newValue, string propertyName)
    {
      if (oldValue.Equals(newValue))
        return;
      this.NotifyPropertyChanged(propertyName);
    }

    private void CheckAndTriggerIfChanged(bool oldValue, bool newValue, string propertyName)
    {
      if (oldValue == newValue)
        return;
      this.NotifyPropertyChanged(propertyName);
    }

    private void CheckAndTriggerIfChanged(
      PowerLineFrequency oldValue,
      PowerLineFrequency newValue,
      string propertyName)
    {
      if (oldValue == newValue)
        return;
      this.NotifyPropertyChanged(propertyName);
    }

    private void CheckAndTriggerIfChanged(
      BacklightCompensationMode oldValue,
      BacklightCompensationMode newValue,
      string propertyName)
    {
      if (oldValue == newValue)
        return;
      this.NotifyPropertyChanged(propertyName);
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
