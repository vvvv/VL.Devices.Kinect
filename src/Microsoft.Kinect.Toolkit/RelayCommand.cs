// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.RelayCommand
// Assembly: Microsoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B38B75B-8556-4BDB-9D68-753B7C4809E2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.dll

using System;
using System.Windows.Input;

namespace Microsoft.Kinect.Toolkit
{
  public class RelayCommand : ICommand
  {
    private Action executeDelegate;
    private Func<bool> canExecuteDelegate;
    private EventHandler canExecuteEventhandler;

    public RelayCommand(Action executeDelegate, Func<bool> canExecuteDelegate)
    {
      if (executeDelegate == null)
        throw new ArgumentNullException(nameof (executeDelegate));
      this.canExecuteDelegate = canExecuteDelegate;
      this.executeDelegate = executeDelegate;
    }

    public RelayCommand(Action executeDelegate)
      : this(executeDelegate, (Func<bool>) null)
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

    public bool CanExecute(object parameter) => this.canExecuteDelegate == null || this.canExecuteDelegate();

    public void Execute(object parameter) => this.executeDelegate();

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
