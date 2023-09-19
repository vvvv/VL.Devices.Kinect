// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.ContextEventHandler`1
// Assembly: Microsoft.Kinect, Version=1.8.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: DB268133-F353-41FE-808D-1E204C642CE2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Microsoft.Kinect
{
  internal class ContextEventHandler<T> : IDisposable where T : EventArgs
  {
    private readonly ThreadSafeList<ContextEventHandler<T>.ContextHandlerPair> _actualHandlers;
    private readonly ContextEventHandler<T>.ContextSynchronizationMethod _method;
    private bool _isDisposed;

    public ContextEventHandler(
      ContextEventHandler<T>.ContextSynchronizationMethod method = ContextEventHandler<T>.ContextSynchronizationMethod.Post)
    {
      this._isDisposed = false;
      this._method = method;
      this._actualHandlers = new ThreadSafeList<ContextEventHandler<T>.ContextHandlerPair>();
    }

    public bool HasHandlers => this._actualHandlers.Count > 0;

    public void AddHandler(EventHandler<T> originalHandler)
    {
      if (originalHandler == null)
        return;
      this._actualHandlers.Add(new ContextEventHandler<T>.ContextHandlerPair(originalHandler, new ContextEventHandler<T>.SynchronizationContextIdentifier(SynchronizationContext.Current)));
    }

    public void RemoveHandler(EventHandler<T> originalHandler)
    {
      ContextEventHandler<T>.SynchronizationContextIdentifier contextIdentifier = new ContextEventHandler<T>.SynchronizationContextIdentifier(SynchronizationContext.Current);
      ContextEventHandler<T>.ContextHandlerPair contextHandlerPair = (ContextEventHandler<T>.ContextHandlerPair) null;
      foreach (ContextEventHandler<T>.ContextHandlerPair actualHandler in (IEnumerable<ContextEventHandler<T>.ContextHandlerPair>) this._actualHandlers)
      {
        EventHandler<T> handler = actualHandler.Handler;
        ContextEventHandler<T>.SynchronizationContextIdentifier contextId = actualHandler.ContextId;
        if (contextIdentifier == contextId && handler == originalHandler)
        {
          contextHandlerPair = actualHandler;
          break;
        }
      }
      if (contextHandlerPair == null)
        return;
      this._actualHandlers.Remove(contextHandlerPair);
    }

    public void Invoke(object sender, T eventArgs)
    {
      if (!this.HasHandlers)
        return;
      foreach (ContextEventHandler<T>.ContextHandlerPair actualHandler in (IEnumerable<ContextEventHandler<T>.ContextHandlerPair>) this._actualHandlers)
      {
        EventHandler<T> handler = actualHandler.Handler;
        SynchronizationContext context = actualHandler.ContextId.Context;
        if (context == null)
          handler(sender, eventArgs);
        else if (this._method == ContextEventHandler<T>.ContextSynchronizationMethod.Post)
          context.Post(new SendOrPostCallback(this.SendOrPostDelegate), (object) new ContextEventHandler<T>.ContextEventHandlerArgsWrapper(handler, sender, eventArgs));
        else if (this._method == ContextEventHandler<T>.ContextSynchronizationMethod.Send)
          context.Send(new SendOrPostCallback(this.SendOrPostDelegate), (object) new ContextEventHandler<T>.ContextEventHandlerArgsWrapper(handler, sender, eventArgs));
      }
    }

    public void Dispose()
    {
      this._isDisposed = true;
      this._actualHandlers.Clear();
    }

    private void SendOrPostDelegate(object state)
    {
      if (this._isDisposed)
        return;
      ContextEventHandler<T>.ContextEventHandlerArgsWrapper handlerArgsWrapper = (ContextEventHandler<T>.ContextEventHandlerArgsWrapper) state;
      handlerArgsWrapper.Handler(handlerArgsWrapper.Sender, handlerArgsWrapper.Args);
    }

    internal enum ContextSynchronizationMethod
    {
      Send,
      Post,
    }

    private class ContextEventHandlerArgsWrapper
    {
      public ContextEventHandlerArgsWrapper(EventHandler<T> handler, object sender, T args)
      {
        this.Handler = handler;
        this.Sender = sender;
        this.Args = args;
      }

      public EventHandler<T> Handler { get; private set; }

      public object Sender { get; private set; }

      public T Args { get; private set; }
    }

    private class SynchronizationContextIdentifier
    {
      private const string DispatcherFieldName = "_dispatcher";
      private const string DispatcherSynchronizationContextName = "DispatcherSynchronizationContext";
      private object _dispatcherObject;

      public SynchronizationContextIdentifier(SynchronizationContext context)
      {
        this.Context = context;
        if (object.ReferenceEquals((object) context, (object) null))
          return;
        Type type = context.GetType();
        if (!("DispatcherSynchronizationContext" == type.Name))
          return;
        FieldInfo field = type.GetField("_dispatcher", BindingFlags.Instance | BindingFlags.NonPublic);
        if (!(field != (FieldInfo) null))
          return;
        this._dispatcherObject = field.GetValue((object) context);
      }

      public override int GetHashCode()
      {
        if (!object.ReferenceEquals(this._dispatcherObject, (object) null))
          return this._dispatcherObject.GetHashCode();
        return !object.ReferenceEquals((object) this.Context, (object) null) ? this.Context.GetHashCode() : 0;
      }

      public override bool Equals(object obj)
      {
        ContextEventHandler<T>.SynchronizationContextIdentifier contextIdentifier = obj as ContextEventHandler<T>.SynchronizationContextIdentifier;
        return !object.ReferenceEquals((object) contextIdentifier, (object) null) && this.Equals(contextIdentifier);
      }

      public bool Equals(
        ContextEventHandler<T>.SynchronizationContextIdentifier contextId)
      {
        if (object.ReferenceEquals((object) contextId, (object) null))
          return false;
        return this._dispatcherObject == null && contextId._dispatcherObject == null ? this.Context == contextId.Context : this._dispatcherObject == contextId._dispatcherObject;
      }

      public static bool operator ==(
        ContextEventHandler<T>.SynchronizationContextIdentifier contextId1,
        ContextEventHandler<T>.SynchronizationContextIdentifier contextId2)
      {
        return !object.ReferenceEquals((object) contextId1, (object) null) && !object.ReferenceEquals((object) contextId2, (object) null) && contextId1.Equals(contextId2);
      }

      public static bool operator !=(
        ContextEventHandler<T>.SynchronizationContextIdentifier contextId1,
        ContextEventHandler<T>.SynchronizationContextIdentifier contextId2)
      {
        return object.ReferenceEquals((object) contextId1, (object) null) || object.ReferenceEquals((object) contextId2, (object) null) || !contextId1.Equals(contextId2);
      }

      public SynchronizationContext Context { get; private set; }
    }

    private class ContextHandlerPair
    {
      public ContextHandlerPair(
        EventHandler<T> handler,
        ContextEventHandler<T>.SynchronizationContextIdentifier contextId)
      {
        this.Handler = handler;
        this.ContextId = contextId;
      }

      public ContextEventHandler<T>.SynchronizationContextIdentifier ContextId { get; private set; }

      public EventHandler<T> Handler { get; private set; }
    }
  }
}
