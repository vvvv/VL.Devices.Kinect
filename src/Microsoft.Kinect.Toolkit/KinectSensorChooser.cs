// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.KinectSensorChooser
// Assembly: Microsoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B38B75B-8556-4BDB-9D68-753B7C4809E2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace Microsoft.Kinect.Toolkit
{
  public class KinectSensorChooser : INotifyPropertyChanged
  {
    private readonly object lockObject = new object();
    private readonly ContextEventWrapper<KinectChangedEventArgs> kinectChangedContextWrapper = new ContextEventWrapper<KinectChangedEventArgs>(ContextSynchronizationMethod.Post);
    private readonly ContextEventWrapper<PropertyChangedEventArgs> propertyChangedContextWrapper = new ContextEventWrapper<PropertyChangedEventArgs>(ContextSynchronizationMethod.Post);
    private readonly Dictionary<PropertyChangedEventHandler, EventHandler<PropertyChangedEventArgs>> changedHandlers = new Dictionary<PropertyChangedEventHandler, EventHandler<PropertyChangedEventArgs>>();
    private bool isStarted;
    private string requiredConnectionId;

    public event EventHandler<KinectChangedEventArgs> KinectChanged
    {
      add => this.kinectChangedContextWrapper.AddHandler(value);
      remove => this.kinectChangedContextWrapper.RemoveHandler(value);
    }

    public event PropertyChangedEventHandler PropertyChanged
    {
      add
      {
        lock (this.lockObject)
        {
          EventHandler<PropertyChangedEventArgs> originalHandler = (EventHandler<PropertyChangedEventArgs>) ((sender, args) => value(sender, args));
          this.changedHandlers[value] = originalHandler;
          this.propertyChangedContextWrapper.AddHandler(originalHandler);
        }
      }
      remove
      {
        lock (this.lockObject)
        {
          EventHandler<PropertyChangedEventArgs> changedHandler = this.changedHandlers[value];
          this.changedHandlers.Remove(value);
          this.propertyChangedContextWrapper.RemoveHandler(changedHandler);
        }
      }
    }

    public string RequiredConnectionId
    {
      get => this.requiredConnectionId;
      set
      {
        if (value == this.requiredConnectionId)
          return;
        using (CallbackLock callbackLock = new CallbackLock(this.lockObject))
        {
          if (value == this.requiredConnectionId)
            return;
          this.requiredConnectionId = value;
          if (this.requiredConnectionId == null || this.Kinect != null && this.Kinect.DeviceConnectionId == this.requiredConnectionId)
            return;
          this.TryFindAndStartKinect(callbackLock);
        }
      }
    }

    public KinectSensor Kinect { get; private set; }

    public ChooserStatus Status { get; private set; }

    public void Start()
    {
      if (this.isStarted)
        return;
      using (CallbackLock callbackLock = new CallbackLock(this.lockObject))
      {
        if (this.isStarted)
          return;
        this.isStarted = true;
        KinectSensor.KinectSensors.StatusChanged += new EventHandler<StatusChangedEventArgs>(this.KinectSensorsOnStatusChanged);
        this.TryFindAndStartKinect(callbackLock);
      }
    }

    public void Stop()
    {
      if (!this.isStarted)
        return;
      using (CallbackLock callbackLock = new CallbackLock(this.lockObject))
      {
        if (!this.isStarted)
          return;
        this.isStarted = false;
        KinectSensor.KinectSensors.StatusChanged -= new EventHandler<StatusChangedEventArgs>(this.KinectSensorsOnStatusChanged);
        this.SetSensorAndStatus(callbackLock, (KinectSensor) null, ChooserStatus.None);
      }
    }

    public void TryResolveConflict()
    {
      using (CallbackLock callbackLock = new CallbackLock(this.lockObject))
        this.TryFindAndStartKinect(callbackLock);
    }

    private static ChooserStatus GetErrorStatusFromSensor(KinectSensor sensor)
    {
      switch (sensor.Status)
      {
        case KinectStatus.Undefined:
          return ChooserStatus.SensorError;
        case KinectStatus.Disconnected:
          return ChooserStatus.SensorError;
        case KinectStatus.Connected:
          return ChooserStatus.None;
        case KinectStatus.Initializing:
          return ChooserStatus.SensorInitializing;
        case KinectStatus.Error:
          return ChooserStatus.SensorError;
        case KinectStatus.NotPowered:
          return ChooserStatus.SensorNotPowered;
        case KinectStatus.NotReady:
          return ChooserStatus.SensorError;
        case KinectStatus.DeviceNotGenuine:
          return ChooserStatus.SensorNotGenuine;
        case KinectStatus.DeviceNotSupported:
          return ChooserStatus.SensorNotSupported;
        case KinectStatus.InsufficientBandwidth:
          return ChooserStatus.SensorInsufficientBandwidth;
        default:
          throw new ArgumentOutOfRangeException(nameof (sensor));
      }
    }

    private void KinectSensorsOnStatusChanged(object sender, StatusChangedEventArgs e)
    {
      if (e == null || e.Sensor != this.Kinect && this.Kinect != null)
        return;
      using (CallbackLock callbackLock = new CallbackLock(this.lockObject))
      {
        if (e.Sensor != this.Kinect && this.Kinect != null)
          return;
        this.TryFindAndStartKinect(callbackLock);
      }
    }

    private void SetSensorAndStatus(
      CallbackLock callbackLock,
      KinectSensor newKinect,
      ChooserStatus newChooserStatus)
    {
      KinectSensor oldKinect = this.Kinect;
      if (oldKinect != newKinect)
      {
        if (oldKinect != null)
          oldKinect.Stop();
        this.Kinect = newKinect;
        callbackLock.LockExit += (LockExitEventHandler) (() => this.kinectChangedContextWrapper.Invoke((object) this, new KinectChangedEventArgs(oldKinect, newKinect)));
        callbackLock.LockExit += (LockExitEventHandler) (() => this.RaisePropertyChanged("Kinect"));
      }
      if (this.Status == newChooserStatus)
        return;
      this.Status = newChooserStatus;
      callbackLock.LockExit += (LockExitEventHandler) (() => this.RaisePropertyChanged("Status"));
    }

    private void TryFindAndStartKinect(CallbackLock callbackLock)
    {
      if (!this.isStarted || this.Kinect != null && this.Kinect.Status == KinectStatus.Connected && (this.requiredConnectionId == null || this.Kinect.DeviceConnectionId == this.requiredConnectionId))
        return;
      KinectSensor newKinect = (KinectSensor) null;
      ChooserStatus newChooserStatus = ChooserStatus.None;
      if (KinectSensor.KinectSensors.Count == 0)
      {
        newChooserStatus = ChooserStatus.NoAvailableSensors;
      }
      else
      {
        foreach (KinectSensor kinectSensor in (ReadOnlyCollection<KinectSensor>) KinectSensor.KinectSensors)
        {
          if (this.requiredConnectionId != null && kinectSensor.DeviceConnectionId != this.requiredConnectionId)
            newChooserStatus |= ChooserStatus.NoAvailableSensors;
          else if (kinectSensor.Status != KinectStatus.Connected)
            newChooserStatus |= KinectSensorChooser.GetErrorStatusFromSensor(kinectSensor);
          else if (kinectSensor.IsRunning)
          {
            newChooserStatus |= ChooserStatus.NoAvailableSensors;
          }
          else
          {
            try
            {
              kinectSensor.Start();
            }
            catch (IOException ex)
            {
              newChooserStatus |= ChooserStatus.SensorConflict;
              continue;
            }
            catch (InvalidOperationException ex)
            {
              newChooserStatus |= ChooserStatus.SensorConflict;
              continue;
            }
            newChooserStatus = ChooserStatus.SensorStarted;
            newKinect = kinectSensor;
            break;
          }
        }
      }
      this.SetSensorAndStatus(callbackLock, newKinect, newChooserStatus);
    }

    private void RaisePropertyChanged(string propertyName) => this.propertyChangedContextWrapper.Invoke((object) this, new PropertyChangedEventArgs(propertyName));
  }
}
