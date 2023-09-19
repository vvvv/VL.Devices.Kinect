// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.KinectSensorChooserUIViewModel
// Assembly: Microsoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B38B75B-8556-4BDB-9D68-753B7C4809E2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.dll

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Microsoft.Kinect.Toolkit
{
  public class KinectSensorChooserUIViewModel : DependencyObject
  {
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:ElementsMustBeOrderedByAccess", Justification = "ReadOnlyDependencyProperty requires private static field to be initialized prior to the public static field")]
    private static readonly DependencyPropertyKey MessagePropertyKey = DependencyProperty.RegisterReadOnly(nameof (Message), typeof (string), typeof (KinectSensorChooserUIViewModel), new PropertyMetadata((object) string.Empty));
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:ElementsMustBeOrderedByAccess", Justification = "ReadOnlyDependencyProperty requires private static field to be initialized prior to the public static field")]
    private static readonly DependencyPropertyKey MoreInformationPropertyKey = DependencyProperty.RegisterReadOnly(nameof (MoreInformation), typeof (string), typeof (KinectSensorChooserUIViewModel), new PropertyMetadata((object) string.Empty));
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:ElementsMustBeOrderedByAccess", Justification = "ReadOnlyDependencyProperty requires private static field to be initialized prior to the public static field")]
    private static readonly DependencyPropertyKey MoreInformationUriPropertyKey = DependencyProperty.RegisterReadOnly(nameof (MoreInformationUri), typeof (Uri), typeof (KinectSensorChooserUIViewModel), new PropertyMetadata((PropertyChangedCallback) null));
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:ElementsMustBeOrderedByAccess", Justification = "ReadOnlyDependencyProperty requires private static field to be initialized prior to the public static field")]
    private static readonly DependencyPropertyKey MoreInformationVisibilityPropertyKey = DependencyProperty.RegisterReadOnly(nameof (MoreInformationVisibility), typeof (Visibility), typeof (KinectSensorChooserUIViewModel), new PropertyMetadata((object) Visibility.Collapsed));
    public static readonly DependencyProperty IsListeningProperty = DependencyProperty.Register(nameof (IsListening), typeof (bool), typeof (KinectSensorChooserUIViewModel), new PropertyMetadata((object) false, (PropertyChangedCallback) ((o, args) => ((KinectSensorChooserUIViewModel) o).IsListeningChanged())));
    public static readonly DependencyProperty KinectSensorChooserProperty = DependencyProperty.Register(nameof (KinectSensorChooser), typeof (KinectSensorChooser), typeof (KinectSensorChooserUIViewModel), new PropertyMetadata((object) null, (PropertyChangedCallback) ((o, args) => ((KinectSensorChooserUIViewModel) o).OnKinectKinectSensorChooserChanged((KinectSensorChooser) args.NewValue))));
    public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(nameof (Status), typeof (ChooserStatus), typeof (KinectSensorChooserUIViewModel), new PropertyMetadata((object) ChooserStatus.None, (PropertyChangedCallback) ((o, args) => ((KinectSensorChooserUIViewModel) o).OnStatusChanged())));
    public static readonly DependencyProperty VisualStateProperty = DependencyProperty.Register(nameof (VisualState), typeof (string), typeof (KinectSensorChooserUIViewModel), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty MessageProperty = KinectSensorChooserUIViewModel.MessagePropertyKey.DependencyProperty;
    public static readonly DependencyProperty MoreInformationProperty = KinectSensorChooserUIViewModel.MoreInformationPropertyKey.DependencyProperty;
    public static readonly DependencyProperty MoreInformationUriProperty = KinectSensorChooserUIViewModel.MoreInformationUriPropertyKey.DependencyProperty;
    public static readonly DependencyProperty MoreInformationVisibilityProperty = KinectSensorChooserUIViewModel.MoreInformationVisibilityPropertyKey.DependencyProperty;
    private RelayCommand retryCommand;

    public bool IsListening
    {
      get => (bool) this.GetValue(KinectSensorChooserUIViewModel.IsListeningProperty);
      set => this.SetValue(KinectSensorChooserUIViewModel.IsListeningProperty, (object) value);
    }

    public KinectSensorChooser KinectSensorChooser
    {
      get => (KinectSensorChooser) this.GetValue(KinectSensorChooserUIViewModel.KinectSensorChooserProperty);
      set => this.SetValue(KinectSensorChooserUIViewModel.KinectSensorChooserProperty, (object) value);
    }

    public string Message
    {
      get => (string) this.GetValue(KinectSensorChooserUIViewModel.MessageProperty);
      private set => this.SetValue(KinectSensorChooserUIViewModel.MessagePropertyKey, (object) value);
    }

    public string MoreInformation
    {
      get => (string) this.GetValue(KinectSensorChooserUIViewModel.MoreInformationProperty);
      private set => this.SetValue(KinectSensorChooserUIViewModel.MoreInformationPropertyKey, (object) value);
    }

    public Uri MoreInformationUri
    {
      get => (Uri) this.GetValue(KinectSensorChooserUIViewModel.MoreInformationUriProperty);
      private set => this.SetValue(KinectSensorChooserUIViewModel.MoreInformationUriPropertyKey, (object) value);
    }

    public Visibility MoreInformationVisibility
    {
      get => (Visibility) this.GetValue(KinectSensorChooserUIViewModel.MoreInformationVisibilityProperty);
      private set => this.SetValue(KinectSensorChooserUIViewModel.MoreInformationVisibilityPropertyKey, (object) value);
    }

    public ICommand RetryCommand
    {
      get
      {
        if (this.retryCommand == null)
          this.retryCommand = new RelayCommand(new Action(this.Retry), new Func<bool>(this.CanRetry));
        return (ICommand) this.retryCommand;
      }
    }

    public ChooserStatus Status
    {
      get => (ChooserStatus) this.GetValue(KinectSensorChooserUIViewModel.StatusProperty);
      set => this.SetValue(KinectSensorChooserUIViewModel.StatusProperty, (object) value);
    }

    public string VisualState
    {
      get => (string) this.GetValue(KinectSensorChooserUIViewModel.VisualStateProperty);
      set => this.SetValue(KinectSensorChooserUIViewModel.VisualStateProperty, (object) value);
    }

    private bool CanRetry() => (this.Status & ChooserStatus.SensorConflict) != ChooserStatus.None || this.Status == ChooserStatus.NoAvailableSensors;

    private void IsListeningChanged() => this.UpdateState();

    private void OnKinectKinectSensorChooserChanged(KinectSensorChooser newValue)
    {
      if (newValue != null)
      {
        Binding binding = new Binding("Status")
        {
          Source = (object) newValue
        };
        BindingOperations.SetBinding((DependencyObject) this, KinectSensorChooserUIViewModel.StatusProperty, (BindingBase) binding);
      }
      else
        BindingOperations.ClearBinding((DependencyObject) this, KinectSensorChooserUIViewModel.StatusProperty);
    }

    private void OnStatusChanged() => this.UpdateState();

    private void Retry()
    {
      if (this.KinectSensorChooser == null)
        return;
      this.KinectSensorChooser.TryResolveConflict();
    }

    private void UpdateState()
    {
      string str1 = (string) null;
      Uri uri = (Uri) null;
      string str2;
      string str3;
      if ((this.Status & ChooserStatus.SensorStarted) != ChooserStatus.None)
      {
        if (this.IsListening)
        {
          str2 = "AllSetListening";
          str3 = Microsoft.Kinect.Toolkit.Properties.Resources.MessageAllSetListening;
        }
        else
        {
          str2 = "AllSetNotListening";
          str3 = Microsoft.Kinect.Toolkit.Properties.Resources.MessageAllSet;
        }
      }
      else if ((this.Status & ChooserStatus.SensorInitializing) != ChooserStatus.None)
      {
        str2 = "Initializing";
        str3 = Microsoft.Kinect.Toolkit.Properties.Resources.MessageInitializing;
      }
      else if ((this.Status & ChooserStatus.SensorConflict) != ChooserStatus.None)
      {
        str2 = "Error";
        str3 = Microsoft.Kinect.Toolkit.Properties.Resources.MessageConflict;
        str1 = Microsoft.Kinect.Toolkit.Properties.Resources.MoreInformationConflict;
        uri = new Uri("http://go.microsoft.com/fwlink/?LinkID=239812");
      }
      else if ((this.Status & ChooserStatus.SensorNotGenuine) != ChooserStatus.None)
      {
        str2 = "Error";
        str3 = Microsoft.Kinect.Toolkit.Properties.Resources.MessageNotGenuine;
        str1 = Microsoft.Kinect.Toolkit.Properties.Resources.MoreInformationNotGenuine;
        uri = new Uri("http://go.microsoft.com/fwlink/?LinkID=239813");
      }
      else if ((this.Status & ChooserStatus.SensorNotSupported) != ChooserStatus.None)
      {
        str2 = "Error";
        str3 = Microsoft.Kinect.Toolkit.Properties.Resources.MessageNotSupported;
        str1 = Microsoft.Kinect.Toolkit.Properties.Resources.MoreInformationNotSupported;
        uri = new Uri("http://go.microsoft.com/fwlink/?LinkID=239814");
      }
      else if ((this.Status & ChooserStatus.SensorError) != ChooserStatus.None)
      {
        str2 = "Error";
        str3 = Microsoft.Kinect.Toolkit.Properties.Resources.MessageError;
        str1 = Microsoft.Kinect.Toolkit.Properties.Resources.MoreInformationError;
        uri = new Uri("http://go.microsoft.com/fwlink/?LinkID=239817");
      }
      else if ((this.Status & ChooserStatus.SensorInsufficientBandwidth) != ChooserStatus.None)
      {
        str2 = "Error";
        str3 = Microsoft.Kinect.Toolkit.Properties.Resources.MessageInsufficientBandwidth;
        str1 = Microsoft.Kinect.Toolkit.Properties.Resources.MoreInformationInsufficientBandwidth;
        uri = new Uri("http://go.microsoft.com/fwlink/?LinkID=239818");
      }
      else if ((this.Status & ChooserStatus.SensorNotPowered) != ChooserStatus.None)
      {
        str2 = "Error";
        str3 = Microsoft.Kinect.Toolkit.Properties.Resources.MessageNotPowered;
        str1 = Microsoft.Kinect.Toolkit.Properties.Resources.MoreInformationNotPowered;
        uri = new Uri("http://go.microsoft.com/fwlink/?LinkID=239819");
      }
      else if ((this.Status & ChooserStatus.NoAvailableSensors) != ChooserStatus.None)
      {
        str2 = "NoAvailableSensors";
        str3 = Microsoft.Kinect.Toolkit.Properties.Resources.MessageNoAvailableSensors;
        str1 = Microsoft.Kinect.Toolkit.Properties.Resources.MoreInformationNoAvailableSensors;
        uri = new Uri("http://go.microsoft.com/fwlink/?LinkID=239815");
      }
      else
      {
        str2 = "Stopped";
        str3 = Microsoft.Kinect.Toolkit.Properties.Resources.MessageNoAvailableSensors;
        str1 = Microsoft.Kinect.Toolkit.Properties.Resources.MoreInformationNoAvailableSensors;
        uri = new Uri("http://go.microsoft.com/fwlink/?LinkID=239815");
      }
      this.Message = str3;
      this.MoreInformation = str1;
      this.MoreInformationUri = uri;
      this.MoreInformationVisibility = uri == (Uri) null ? Visibility.Collapsed : Visibility.Visible;
      if (this.retryCommand != null)
        this.retryCommand.InvokeCanExecuteChanged();
      this.VisualState = str2;
    }
  }
}
