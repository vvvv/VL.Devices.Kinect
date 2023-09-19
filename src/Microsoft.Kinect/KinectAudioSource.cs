// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.KinectAudioSource
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Kinect
{
  public sealed class KinectAudioSource
  {
    internal const int PID_FIRST_USABLE = 2;
    internal const int MFPKEY_WMAAECMA_SYSTEM_MODE = 2;
    internal const int MFPKEY_WMAAECMA_DMO_SOURCE_MODE = 3;
    internal const int MFPKEY_WMAAECMA_DEVICE_INDEXES = 4;
    internal const int MFPKEY_WMAAECMA_FEATURE_MODE = 5;
    internal const int MFPKEY_WMAAECMA_FEATR_NS = 8;
    internal const int MFPKEY_WMAAECMA_FEATR_AGC = 9;
    internal const int MFPKEY_WMAAECMA_FEATR_AES = 10;
    internal const int MFPKEY_WMAAECMA_FEATR_MICARR_MODE = 18;
    private const double s_minBeamAngle = -50.0;
    private const double s_maxBeamAngle = 50.0;
    private const double s_minSoundSourceAngle = -50.0;
    private const double s_maxSoundSourceAngle = 50.0;
    internal const string AUDIO_CAPTURE_THREAD_NAME = "AudioCaptureThread";
    internal static readonly Guid MFPKEY_WMAAECMA_KEY_BASE = new Guid(1867695463, (short) 864, (short) 19410, new byte[8]
    {
      (byte) 150,
      (byte) 23,
      (byte) 204,
      (byte) 191,
      (byte) 20,
      (byte) 33,
      (byte) 201,
      (byte) 57
    });
    internal static readonly TimeSpan DefaultReadStaleThreshold = TimeSpan.FromMilliseconds(500.0);
    private readonly Dictionary<PROPERTYKEY, object> _dmoProperties = new Dictionary<PROPERTYKEY, object>();
    private bool _applyProperties;
    private readonly INativeAudioWrapper _nativeAudioWrapper;
    private readonly object _propertyLock = new object();
    private readonly object _initLock = new object();
    private KinectAudioStream _stream;
    private double _soundSourceAngle;
    private double _soundSourceAngleConfidence;
    private double _beamAngle;
    private double _manualBeamAngle;
    private double _beamAngleSetValue = double.MinValue;
    private KinectSensor _sensor;
    private readonly ContextEventHandler<BeamAngleChangedEventArgs> _BeamAngleChangedContextHandler;
    private readonly ContextEventHandler<SoundSourceAngleChangedEventArgs> _SoundSourceAngleChangedContextHandler;
    private EchoCancellationMode _echoCancellationMode;
    private BeamAngleMode _beamAngleMode;

    internal INativeAudioWrapper NativeWrapper => this._nativeAudioWrapper;

    internal static PROPERTYKEY PropertyKeyFromId(int pid) => new PROPERTYKEY()
    {
      pid = pid,
      fmtid = KinectAudioSource.MFPKEY_WMAAECMA_KEY_BASE
    };

    private void SetProperty(int pid, object value)
    {
      PROPERTYKEY key = KinectAudioSource.PropertyKeyFromId(pid);
      lock (this._propertyLock)
      {
        this._dmoProperties[key] = value;
        this._applyProperties = true;
      }
    }

    private object GetProperty(int pid)
    {
      object property = (object) null;
      PROPERTYKEY key = new PROPERTYKEY()
      {
        pid = pid,
        fmtid = KinectAudioSource.MFPKEY_WMAAECMA_KEY_BASE
      };
      lock (this._propertyLock)
      {
        if (this._dmoProperties.ContainsKey(key))
          property = this._dmoProperties[key];
      }
      return property;
    }

    internal void ApplyPendingProperties(IPropertyStore propertyStore)
    {
      lock (this._propertyLock)
      {
        if (!this._applyProperties)
          return;
        foreach (PROPERTYKEY key in this._dmoProperties.Keys)
        {
          IPropertyStore propertyStore1 = propertyStore;
          PROPERTYKEY propertykey = key;
          ref PROPERTYKEY local1 = ref propertykey;
          object dmoProperty = this._dmoProperties[key];
          ref object local2 = ref dmoProperty;
          propertyStore1.SetValue(ref local1, ref local2);
        }
        this._applyProperties = false;
      }
    }

    private void VerifyFeatureMode()
    {
      if (!this.FeatureMode)
        throw new InvalidOperationException(Resources.NeedFeatureMode);
    }

    private int DeviceIndexes
    {
      get
      {
        object property = this.GetProperty(4);
        return property == null ? 0 : (int) property;
      }
      set => this.SetProperty(4, (object) value);
    }

    internal ushort MicrophoneIndex
    {
      set => this.DeviceIndexes = (int) ((long) this.DeviceIndexes & 4294901760L) | (int) value;
    }

    private bool FeatureMode
    {
      get
      {
        object property = this.GetProperty(5);
        return property != null && (bool) property;
      }
      set => this.SetProperty(5, (object) value);
    }

    private KinectAudioSource._MIC_ARRAY_MODE MicArrayMode
    {
      set
      {
        this.VerifyFeatureMode();
        this.SetProperty(18, (object) (int) value);
      }
    }

    public event EventHandler<BeamAngleChangedEventArgs> BeamAngleChanged
    {
      add => this._BeamAngleChangedContextHandler.AddHandler(value);
      remove => this._BeamAngleChangedContextHandler.RemoveHandler(value);
    }

    public event EventHandler<SoundSourceAngleChangedEventArgs> SoundSourceAngleChanged
    {
      add => this._SoundSourceAngleChangedContextHandler.AddHandler(value);
      remove => this._SoundSourceAngleChangedContextHandler.RemoveHandler(value);
    }

    internal KinectAudioSource(KinectSensor sensor, INativeAudioWrapper audioWrapper)
    {
      this._BeamAngleChangedContextHandler = new ContextEventHandler<BeamAngleChangedEventArgs>();
      this._SoundSourceAngleChangedContextHandler = new ContextEventHandler<SoundSourceAngleChangedEventArgs>();
      this._sensor = sensor;
      this._nativeAudioWrapper = audioWrapper;
      this._beamAngle = 0.0;
      this._manualBeamAngle = 0.0;
      this._soundSourceAngle = 0.0;
      this._soundSourceAngleConfidence = 0.0;
      this.SetProperty(3, (object) true);
      this.FeatureMode = true;
      this.EchoCancellationMode = EchoCancellationMode.None;
      this.EchoCancellationSpeakerIndex = this.GetDefaultSpeakerIndex();
      this.BeamAngleMode = BeamAngleMode.Automatic;
      this.NoiseSuppression = true;
      this.AutomaticGainControlEnabled = false;
    }

    internal KinectAudioSource(KinectSensor sensor)
      : this(sensor, (INativeAudioWrapper) new DmoAudioWrapper(sensor))
    {
    }

    public Stream Start(TimeSpan readStaleThreshold)
    {
      lock (this._initLock)
      {
        if (this._sensor == null || !this._sensor.IsRunning)
          throw new InvalidOperationException(Resources.SensorMustBeRunningForAudio);
        if (this._stream != null)
          this.Stop();
        if (this._stream != null)
          throw new InvalidOperationException(Resources.CaptureAlreadyStarted);
        this._applyProperties = true;
        this._stream = new KinectAudioStream(this, readStaleThreshold);
        this._stream.Start();
      }
      return (Stream) this._stream;
    }

    public Stream Start() => this.Start(KinectAudioSource.DefaultReadStaleThreshold);

    public void Stop()
    {
      lock (this._initLock)
      {
        if (this._stream == null)
          return;
        this._stream.Stop();
        this._stream.Dispose();
        this._stream = (KinectAudioStream) null;
      }
    }

    public static double MinBeamAngle => -50.0;

    public static double MaxBeamAngle => 50.0;

    public static double MinSoundSourceAngle => -50.0;

    public static double MaxSoundSourceAngle => 50.0;

    public EchoCancellationMode EchoCancellationMode
    {
      get => this._echoCancellationMode;
      set
      {
        switch (value)
        {
          case EchoCancellationMode.None:
            this.SetEchoCancellationMode(0, KinectAudioSource._AEC_SYSTEM_MODE.ArrayOnly);
            break;
          case EchoCancellationMode.CancellationOnly:
            this.SetEchoCancellationMode(0, KinectAudioSource._AEC_SYSTEM_MODE.ArrayAndEchoCancellation);
            break;
          case EchoCancellationMode.CancellationAndSuppression:
            this.SetEchoCancellationMode(1, KinectAudioSource._AEC_SYSTEM_MODE.ArrayAndEchoCancellation);
            break;
          default:
            throw new InvalidOperationException(Resources.InvalidEchoCancellationMode);
        }
        this._echoCancellationMode = value;
      }
    }

    private void SetEchoCancellationMode(
      int echoSuppression,
      KinectAudioSource._AEC_SYSTEM_MODE sysMode)
    {
      this.VerifyFeatureMode();
      this.SetProperty(10, (object) echoSuppression);
      this.SetProperty(2, (object) (int) sysMode);
    }

    public BeamAngleMode BeamAngleMode
    {
      get => this._beamAngleMode;
      set
      {
        switch (value)
        {
          case BeamAngleMode.Automatic:
            this.MicArrayMode = KinectAudioSource._MIC_ARRAY_MODE.SingleBeam;
            break;
          case BeamAngleMode.Adaptive:
            this.MicArrayMode = KinectAudioSource._MIC_ARRAY_MODE.AdaptiveBeam;
            break;
          case BeamAngleMode.Manual:
            this.MicArrayMode = KinectAudioSource._MIC_ARRAY_MODE.ExternalBeam;
            break;
          default:
            throw new InvalidOperationException(Resources.InvalidBeamAngleMode);
        }
        this._beamAngleMode = value;
      }
    }

    public double SoundSourceAngle => this._soundSourceAngle * 180.0 / Math.PI;

    public double SoundSourceAngleConfidence => this._soundSourceAngleConfidence;

    public int EchoCancellationSpeakerIndex
    {
      get => (int) (((long) this.DeviceIndexes & 4294901760L) >> 16);
      set => this.DeviceIndexes = this.DeviceIndexes & (int) ushort.MaxValue | value << 16;
    }

    public bool NoiseSuppression
    {
      get
      {
        object property = this.GetProperty(8);
        return property != null && 0 != (int) property;
      }
      set
      {
        this.VerifyFeatureMode();
        this.SetProperty(8, (object) (value ? 1 : 0));
      }
    }

    public bool AutomaticGainControlEnabled
    {
      get
      {
        object property = this.GetProperty(9);
        return property != null && (bool) property;
      }
      set
      {
        this.VerifyFeatureMode();
        this.SetProperty(9, (object) value);
      }
    }

    public double BeamAngle => this._beamAngle * 180.0 / Math.PI;

    public double ManualBeamAngle
    {
      get => this._manualBeamAngle;
      set
      {
        this._manualBeamAngle = value;
        this._beamAngleSetValue = this._manualBeamAngle * Math.PI / 180.0;
      }
    }

    internal void SetBeamAngle(double angle)
    {
      this._beamAngle = angle;
      this._BeamAngleChangedContextHandler.Invoke((object) this, new BeamAngleChangedEventArgs()
      {
        Angle = this.BeamAngle
      });
    }

    internal void SetSoundSourceAngleAndConfidence(double angle, double confidence)
    {
      this._soundSourceAngle = angle;
      this._soundSourceAngleConfidence = confidence;
      this._SoundSourceAngleChangedContextHandler.Invoke((object) this, new SoundSourceAngleChangedEventArgs()
      {
        Angle = this.SoundSourceAngle,
        ConfidenceLevel = this.SoundSourceAngleConfidence
      });
    }

    internal double FetchNextBeamAngle()
    {
      double beamAngleSetValue = this._beamAngleSetValue;
      this._beamAngleSetValue = double.MinValue;
      return beamAngleSetValue;
    }

    private IEnumerable<MicrophoneArrayDevice> FindCaptureDevices()
    {
      int count = 0;
      this._nativeAudioWrapper.GetMicrophoneArrayDevices((MicrophoneArrayDevice[]) null, ref count);
      MicrophoneArrayDevice[] deviceBuffer = new MicrophoneArrayDevice[count];
      this._nativeAudioWrapper.GetMicrophoneArrayDevices(deviceBuffer, ref count);
      return (IEnumerable<MicrophoneArrayDevice>) deviceBuffer;
    }

    private int GetDefaultSpeakerIndex()
    {
      int count = 0;
      this._nativeAudioWrapper.GetSpeakerDevices((SpeakerDevice[]) null, ref count);
      SpeakerDevice[] deviceBuffer = new SpeakerDevice[count];
      this._nativeAudioWrapper.GetSpeakerDevices(deviceBuffer, ref count);
      int defaultSpeakerIndex = 0;
      foreach (SpeakerDevice speakerDevice in deviceBuffer)
      {
        if (speakerDevice.IsDefault)
        {
          defaultSpeakerIndex = speakerDevice.DeviceIndex;
          break;
        }
      }
      return defaultSpeakerIndex;
    }

    internal ushort? FindMicrophoneIndex()
    {
      foreach (MicrophoneArrayDevice captureDevice in this.FindCaptureDevices())
      {
        if (this._nativeAudioWrapper.MatchAudioDeviceToSensor(captureDevice))
          return new ushort?((ushort) captureDevice.DeviceIndex);
      }
      return new ushort?();
    }

    internal void Dispose()
    {
      lock (this._initLock)
      {
        if (this._sensor == null)
          return;
        if (this._stream != null)
          this.Stop();
        this._BeamAngleChangedContextHandler.Dispose();
        this._SoundSourceAngleChangedContextHandler.Dispose();
        this._sensor = (KinectSensor) null;
      }
    }

    internal enum _MIC_ARRAY_MODE
    {
      SingleChannel = 0,
      SimpleSum = 256, // 0x00000100
      SingleBeam = 512, // 0x00000200
      FixedBeam = 1024, // 0x00000400
      ExternalBeam = 2048, // 0x00000800
      AdaptiveBeam = 4352, // 0x00001100
    }

    internal enum _AEC_SYSTEM_MODE
    {
      ArrayOnly = 2,
      ArrayAndEchoCancellation = 4,
    }
  }
}
