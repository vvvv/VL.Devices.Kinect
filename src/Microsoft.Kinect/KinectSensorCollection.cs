// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.KinectSensorCollection
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using Microsoft.Kinect.Interop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;

namespace Microsoft.Kinect
{
  [ComVisible(false)]
  public sealed class KinectSensorCollection : ReadOnlyCollection<KinectSensor>, IDisposable
  {
    private static readonly object s_sensorsLock = new object();
    private static readonly ThreadSafeList<KinectSensor> s_activeSensors = new ThreadSafeList<KinectSensor>(KinectSensorCollection.s_sensorsLock);
    private static readonly KinectSensorCollection s_instance = KinectSensorCollection.Initialize();
    private readonly List<KinectSensor> _sensors = new List<KinectSensor>();
    private readonly NativeMethods.NuiStatusCallback _nuiCallBackDelegate;
    private readonly ContextEventHandler<StatusChangedEventArgs> _statusChangedContextHandler;

    internal static KinectSensorCollection Instance => KinectSensorCollection.s_instance;

    public event EventHandler<StatusChangedEventArgs> StatusChanged
    {
      add => this._statusChangedContextHandler.AddHandler(value);
      remove => this._statusChangedContextHandler.RemoveHandler(value);
    }

    public KinectSensor this[string instanceId]
    {
      get
      {
        lock (KinectSensorCollection.s_sensorsLock)
        {
          if (string.IsNullOrEmpty(instanceId))
            throw new ArgumentNullException(nameof (instanceId));
          KinectSensor kinectSensor = this._sensors.FirstOrDefault<KinectSensor>((Func<KinectSensor, bool>) (r => r.GetDeviceConnectionId() == instanceId || r.GetUniqueKinectId() == instanceId));
          if (kinectSensor == null)
          {
            INuiSensor pNuiSensor = (INuiSensor) null;
            KinectExceptionHelper.CheckHr(NativeMethods.NuiCreateSensorById(instanceId, out pNuiSensor));
            kinectSensor = new KinectSensor(pNuiSensor);
            this._sensors.Add(kinectSensor);
            this.UpdateActiveSensors();
          }
          return kinectSensor;
        }
      }
    }

    private KinectSensorCollection()
      : base((IList<KinectSensor>) KinectSensorCollection.s_activeSensors)
    {
      this._nuiCallBackDelegate = new NativeMethods.NuiStatusCallback(this.OnNuiStatusCallback);
      NativeMethods.NuiSetDeviceStatusCallback(this._nuiCallBackDelegate, IntPtr.Zero);
      this._statusChangedContextHandler = new ContextEventHandler<StatusChangedEventArgs>();
    }

    private static KinectSensorCollection Initialize()
    {
      KinectSensorCollection sensorCollection = new KinectSensorCollection();
      try
      {
        int pCount;
        KinectExceptionHelper.CheckHr(NativeMethods.NuiGetSensorCount(out pCount));
        for (int index = 0; index < pCount; ++index)
        {
          INuiSensor pNuiSensor = (INuiSensor) null;
          if (NativeMethods.NuiCreateSensorByIndex(index, out pNuiSensor) == 0)
          {
            KinectSensor kinectSensor = new KinectSensor(pNuiSensor);
            sensorCollection._sensors.Add(kinectSensor);
          }
        }
        sensorCollection.UpdateActiveSensors();
      }
      catch (Exception ex)
      {
        sensorCollection.Dispose();
        throw;
      }
      return sensorCollection;
    }

    internal void OnNuiStatusCallback(
      int status,
      string instanceName,
      string uniqueDeviceName,
      IntPtr pUserData)
    {
      this.NotifyUserKinectsStatusChanged((object) new InternalStatusChangedEventArgs()
      {
        KinectStatus = KinectSensor.MapStatus((uint) status),
        DeviceConnectionId = instanceName,
        UniqueKinectId = uniqueDeviceName
      });
    }

    internal void NotifyUserKinectsStatusChanged(object state)
    {
      KinectSensor sensor = (KinectSensor) null;
      InternalStatusChangedEventArgs internalArgs = (InternalStatusChangedEventArgs) state;
      lock (KinectSensorCollection.s_sensorsLock)
      {
        sensor = this._sensors.FirstOrDefault<KinectSensor>((Func<KinectSensor, bool>) (r => r.GetDeviceConnectionId() == internalArgs.DeviceConnectionId));
        if (internalArgs.KinectStatus == KinectStatus.Disconnected)
        {
          if (sensor != null)
            this.RemakeSensor(sensor, internalArgs.DeviceConnectionId);
        }
        else if (sensor == null)
        {
          INuiSensor pNuiSensor = (INuiSensor) null;
          KinectExceptionHelper.CheckHr(NativeMethods.NuiCreateSensorById(internalArgs.DeviceConnectionId, out pNuiSensor));
          sensor = new KinectSensor(pNuiSensor);
          this._sensors.Add(sensor);
        }
        if (sensor != null)
          sensor.LastStatus = internalArgs.KinectStatus;
        this.UpdateActiveSensors();
      }
      if (!this._statusChangedContextHandler.HasHandlers)
        return;
      StatusChangedEventArgs eventArgs = new StatusChangedEventArgs()
      {
        Status = internalArgs.KinectStatus
      };
      eventArgs.Sensor = sensor;
      this._statusChangedContextHandler.Invoke((object) this, eventArgs);
    }

    private void UpdateActiveSensors()
    {
      lock (KinectSensorCollection.s_sensorsLock)
      {
        KinectSensorCollection.s_activeSensors.Clear();
        KinectSensorCollection.s_activeSensors.AddRange((IEnumerable<KinectSensor>) this._sensors.Where<KinectSensor>((Func<KinectSensor, bool>) (r => r.GetStatus() != KinectStatus.Disconnected)).OrderBy<KinectSensor, int>((Func<KinectSensor, int>) (r => r.InstanceIndex)));
      }
    }

    internal void RemakeSensor(KinectSensor sensor, string name)
    {
      lock (KinectSensorCollection.s_sensorsLock)
        this._sensors.Remove(sensor);
      if (sensor.LastStatus != KinectStatus.Connected)
        return;
      if (name == null)
        return;
      try
      {
        sensor = KinectSensorCollection.Instance[name];
      }
      catch (InvalidOperationException ex)
      {
      }
    }

    public void Dispose() => this._statusChangedContextHandler.Dispose();
  }
}
