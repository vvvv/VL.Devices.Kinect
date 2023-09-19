// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.KinectSensorChooserUI
// Assembly: Microsoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B38B75B-8556-4BDB-9D68-753B7C4809E2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.dll

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Microsoft.Kinect.Toolkit
{
  public partial class KinectSensorChooserUI : UserControl, IComponentConnector
  {
    public static readonly DependencyProperty IsListeningProperty = DependencyProperty.Register(nameof (IsListening), typeof (bool), typeof (KinectSensorChooserUI), new PropertyMetadata((object) false));
    public static readonly DependencyProperty KinectSensorChooserProperty = DependencyProperty.Register(nameof (KinectSensorChooser), typeof (KinectSensorChooser), typeof (KinectSensorChooserUI), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty VisualStateProperty = DependencyProperty.Register(nameof (VisualState), typeof (string), typeof (KinectSensorChooserUI), new PropertyMetadata((object) null, (PropertyChangedCallback) ((o, args) => ((KinectSensorChooserUI) o).OnVisualstateChanged((string) args.NewValue))));
    private readonly DispatcherTimer popupCloseCheck;
    private bool suppressPopupOnFocus;
    private Window parentWindow;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid layoutRoot;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal VisualStateGroup SensorStatusStates;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal System.Windows.VisualState Stopped;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal System.Windows.VisualState Initializing;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal System.Windows.VisualState AllSetListening;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal System.Windows.VisualState NoAvailableSensors;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal System.Windows.VisualState Error;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal System.Windows.VisualState AllSetNotListening;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid initializingContent;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid allSetNotListeningContent;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid allSetListeningContent;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid noAvailableSensorsContent;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid errorContent;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Popup expandedPopup;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid popupGrid;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid expandedInitializingContent;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid expandedAllSetNotListeningContent;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid expandedAllSetListeningContent;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid expandedNoAvailableSensorsContent;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Grid expandedErrorContent;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal TextBlock MessageTextBlock;
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
    internal Hyperlink TellMeMoreLink;
    private bool _contentLoaded;

    public KinectSensorChooserUI()
    {
      this.Loaded += new RoutedEventHandler(this.OnLoaded);
      this.Unloaded += new RoutedEventHandler(this.OnUnloaded);
      this.InitializeComponent();
      this.popupCloseCheck = new DispatcherTimer(TimeSpan.FromMilliseconds(1000.0), DispatcherPriority.Normal, new EventHandler(this.OnPopupCloseCheckFired), this.Dispatcher);
      KinectSensorChooserUIViewModel target = new KinectSensorChooserUIViewModel();
      this.layoutRoot.DataContext = (object) target;
      Binding binding1 = new Binding(nameof (VisualState))
      {
        Source = (object) target
      };
      this.SetBinding(KinectSensorChooserUI.VisualStateProperty, (BindingBase) binding1);
      Binding binding2 = new Binding(nameof (KinectSensorChooser))
      {
        Source = (object) this
      };
      BindingOperations.SetBinding((DependencyObject) target, KinectSensorChooserUIViewModel.KinectSensorChooserProperty, (BindingBase) binding2);
      Binding binding3 = new Binding(nameof (IsListening))
      {
        Source = (object) this
      };
      BindingOperations.SetBinding((DependencyObject) target, KinectSensorChooserUIViewModel.IsListeningProperty, (BindingBase) binding3);
      this.expandedPopup.LayoutUpdated += new EventHandler(this.ExpandedPopupOnLayoutUpdated);
    }

    public bool IsListening
    {
      get => (bool) this.GetValue(KinectSensorChooserUI.IsListeningProperty);
      set => this.SetValue(KinectSensorChooserUI.IsListeningProperty, (object) value);
    }

    public KinectSensorChooser KinectSensorChooser
    {
      get => (KinectSensorChooser) this.GetValue(KinectSensorChooserUI.KinectSensorChooserProperty);
      set => this.SetValue(KinectSensorChooserUI.KinectSensorChooserProperty, (object) value);
    }

    public string VisualState
    {
      get => (string) this.GetValue(KinectSensorChooserUI.VisualStateProperty);
      set => this.SetValue(KinectSensorChooserUI.VisualStateProperty, (object) value);
    }

    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      this.parentWindow = Window.GetWindow((DependencyObject) this);
      if (this.parentWindow == null)
        return;
      this.parentWindow.Deactivated += new EventHandler(this.ParentWindowOnDeactivated);
    }

    private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
    {
      if (this.parentWindow == null)
        return;
      this.parentWindow.Deactivated -= new EventHandler(this.ParentWindowOnDeactivated);
    }

    private void ParentWindowOnDeactivated(object sender, EventArgs eventArgs) => this.ClosePopup();

    private void ClosePopup() => this.expandedPopup.IsOpen = false;

    private void OpenPopup() => this.expandedPopup.IsOpen = true;

    private void OnRootGridGotKeyboardFocus(object sender, RoutedEventArgs e)
    {
      if (this.suppressPopupOnFocus)
        return;
      this.OpenPopup();
    }

    private void OnRootGridMouseEnter(object sender, MouseEventArgs e) => this.OpenPopup();

    private void ExpandedPopupOnLayoutUpdated(object sender, EventArgs eventArgs) => this.expandedPopup.VerticalOffset = (this.popupGrid.ActualHeight - this.layoutRoot.ActualHeight - 1.0) / 2.0;

    private void ExpandedPopupOnOpened(object sender, EventArgs eventArgs)
    {
      this.popupCloseCheck.Stop();
      if (this.layoutRoot.IsKeyboardFocusWithin)
        Keyboard.Focus((IInputElement) this.popupGrid);
      else
        this.popupCloseCheck.Start();
    }

    private void OnExpandedPopupMouseLeave(object sender, MouseEventArgs e) => this.ClosePopup();

    private void OnPopupCloseCheckFired(object sender, EventArgs e)
    {
      this.popupCloseCheck.Stop();
      if (!this.expandedPopup.IsOpen || this.popupGrid.IsMouseOver || this.popupGrid.IsKeyboardFocusWithin)
        return;
      this.ClosePopup();
    }

    private void OnPopupGridGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
      FrameworkElement oldFocus = e.OldFocus as FrameworkElement;
      if (e.NewFocus as FrameworkElement != this.popupGrid || oldFocus == this.layoutRoot)
        return;
      this.suppressPopupOnFocus = true;
      this.layoutRoot.Focusable = false;
      e.Handled = true;
      FocusNavigationDirection focusNavigationDirection = (Keyboard.Modifiers & ModifierKeys.Shift) != ModifierKeys.None ? FocusNavigationDirection.Previous : FocusNavigationDirection.Next;
      this.ClosePopup();
      this.MoveFocus(new TraversalRequest(focusNavigationDirection));
      this.layoutRoot.Focusable = true;
      this.suppressPopupOnFocus = false;
    }

    private void OnVisualstateChanged(string newState) => VisualStateManager.GoToState((FrameworkElement) this, newState, true);

    private void TellMeMoreLinkRequestNavigate(object sender, RequestNavigateEventArgs e)
    {
      if (e.OriginalSource is Hyperlink originalSource)
      {
        try
        {
          Process.Start(new ProcessStartInfo(originalSource.NavigateUri.ToString()));
        }
        catch (Win32Exception ex)
        {
          int num = (int) MessageBox.Show(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Microsoft.Kinect.Toolkit.Properties.Resources.NoDefaultBrowserAvailable, (object) originalSource.NavigateUri));
        }
        this.ClosePopup();
      }
      e.Handled = true;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Microsoft.Kinect.Toolkit;component/kinectsensorchooserui.xaml", UriKind.Relative));
    }

    [DebuggerNonUserCode]
    [GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
    void IComponentConnector.Connect(int connectionId, object target)
    {
      switch (connectionId)
      {
        case 1:
          this.layoutRoot = (Grid) target;
          this.layoutRoot.MouseEnter += new MouseEventHandler(this.OnRootGridMouseEnter);
          this.layoutRoot.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(this.OnRootGridGotKeyboardFocus);
          break;
        case 2:
          this.SensorStatusStates = (VisualStateGroup) target;
          break;
        case 3:
          this.Stopped = (System.Windows.VisualState) target;
          break;
        case 4:
          this.Initializing = (System.Windows.VisualState) target;
          break;
        case 5:
          this.AllSetListening = (System.Windows.VisualState) target;
          break;
        case 6:
          this.NoAvailableSensors = (System.Windows.VisualState) target;
          break;
        case 7:
          this.Error = (System.Windows.VisualState) target;
          break;
        case 8:
          this.AllSetNotListening = (System.Windows.VisualState) target;
          break;
        case 9:
          this.initializingContent = (Grid) target;
          break;
        case 10:
          this.allSetNotListeningContent = (Grid) target;
          break;
        case 11:
          this.allSetListeningContent = (Grid) target;
          break;
        case 12:
          this.noAvailableSensorsContent = (Grid) target;
          break;
        case 13:
          this.errorContent = (Grid) target;
          break;
        case 14:
          this.expandedPopup = (Popup) target;
          this.expandedPopup.Opened += new EventHandler(this.ExpandedPopupOnOpened);
          this.expandedPopup.MouseLeave += new MouseEventHandler(this.OnExpandedPopupMouseLeave);
          break;
        case 15:
          this.popupGrid = (Grid) target;
          this.popupGrid.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(this.OnPopupGridGotKeyboardFocus);
          break;
        case 16:
          this.expandedInitializingContent = (Grid) target;
          break;
        case 17:
          this.expandedAllSetNotListeningContent = (Grid) target;
          break;
        case 18:
          this.expandedAllSetListeningContent = (Grid) target;
          break;
        case 19:
          this.expandedNoAvailableSensorsContent = (Grid) target;
          break;
        case 20:
          this.expandedErrorContent = (Grid) target;
          break;
        case 21:
          this.MessageTextBlock = (TextBlock) target;
          break;
        case 22:
          this.TellMeMoreLink = (Hyperlink) target;
          this.TellMeMoreLink.RequestNavigate += new RequestNavigateEventHandler(this.TellMeMoreLinkRequestNavigate);
          break;
        default:
          this._contentLoaded = true;
          break;
      }
    }
  }
}
