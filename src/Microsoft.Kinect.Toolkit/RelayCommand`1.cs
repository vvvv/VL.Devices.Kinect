// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.RelayCommand`1
// Assembly: Microsoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B38B75B-8556-4BDB-9D68-753B7C4809E2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.dll

using System;
using System.Globalization;
using System.Windows.Input;

namespace Microsoft.Kinect.Toolkit
{
  public class RelayCommand<T> : ICommand where T : class
  {
    private Action<T> executeDelegate;
    private Predicate<T> canExecuteDelegate;
    private EventHandler canExecuteEventhandler;

    public RelayCommand(Action<T> executeDelegate, Predicate<T> canExecuteDelegate)
    {
      if (executeDelegate == null)
        throw new ArgumentNullException(nameof (executeDelegate));
      this.canExecuteDelegate = canExecuteDelegate;
      this.executeDelegate = executeDelegate;
    }

    public RelayCommand(Action<T> executeDelegate)
      : this(executeDelegate, (Predicate<T>) null)
    {
    }

    public event EventHandler CanExecuteChanged
    {
      add
      {
        this.canExecuteEventhandler += value;
        CommandManager.RequerySuggested += value;
      }
      remove
      {
        this.canExecuteEventhandler -= value;
        CommandManager.RequerySuggested -= value;
      }
    }

    public bool CanExecute(object parameter)
    {
      if (parameter == null)
        throw new ArgumentNullException(nameof (parameter));
      if (this.canExecuteDelegate == null)
        return true;
      if (!(parameter is T obj))
        throw new InvalidCastException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Microsoft.Kinect.Toolkit.Properties.Resources.DelegateCommandCastException, (object) parameter.GetType().FullName, (object) typeof (T).FullName));
      return this.canExecuteDelegate(obj);
    }

    public void Execute(object parameter)
    {
      if (parameter == null)
        throw new ArgumentNullException(nameof (parameter));
      if (!(parameter is T obj))
        throw new InvalidCastException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Microsoft.Kinect.Toolkit.Properties.Resources.DelegateCommandCastException, (object) parameter.GetType().FullName, (object) typeof (T).FullName));
      this.executeDelegate(obj);
    }

    public void InvokeCanExecuteChanged()
    {
      if (this.canExecuteDelegate == null)
        return;
      EventHandler executeEventhandler = this.canExecuteEventhandler;
      if (executeEventhandler == null)
        return;
      executeEventhandler((object) this, EventArgs.Empty);
    }
  }
}
