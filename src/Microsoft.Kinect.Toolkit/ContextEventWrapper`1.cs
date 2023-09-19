// Decompiled with JetBrains decompiler
// Type: Microsoft.Kinect.Toolkit.ContextEventWrapper`1
// Assembly: Microsoft.Kinect.Toolkit, Version=1.8.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B38B75B-8556-4BDB-9D68-753B7C4809E2
// Assembly location: C:\Users\elias\source\repos\VL.Devices.Kinect\lib\net472\Microsoft.Kinect.Toolkit.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;

namespace Microsoft.Kinect.Toolkit
{
  public class ContextEventWrapper<T> where T : EventArgs
  {
    private readonly ThreadSafeCollection<ContextEventWrapper<T>.ContextHandlerPair> actualHandlers;
    private readonly ContextSynchronizationMethod method;
    private bool isDisposed;

    public ContextEventWrapper(ContextSynchronizationMethod method)
    {
      this.isDisposed = false;
      this.method = method;
      this.actualHandlers = new ThreadSafeCollection<ContextEventWrapper<T>.ContextHandlerPair>();
    }

    public ContextEventWrapper()
      : this(ContextSynchronizationMethod.Post)
    {
    }

    public bool HasHandlers => this.actualHandlers.Count > 0;

    public void AddHandler(EventHandler<T> originalHandler)
    {
      if (originalHandler == null)
        return;
      this.actualHandlers.Add(new ContextEventWrapper<T>.ContextHandlerPair(originalHandler, new ContextEventWrapper<T>.SynchronizationContextIdentifier(SynchronizationContext.Current)));
    }

    public void RemoveHandler(EventHandler<T> originalHandler)
    {
      ContextEventWrapper<T>.SynchronizationContextIdentifier contextIdentifier = new ContextEventWrapper<T>.SynchronizationContextIdentifier(SynchronizationContext.Current);
      ContextEventWrapper<T>.ContextHandlerPair contextHandlerPair = (ContextEventWrapper<T>.ContextHandlerPair) null;
      foreach (ContextEventWrapper<T>.ContextHandlerPair actualHandler in (IEnumerable<ContextEventWrapper<T>.ContextHandlerPair>) this.actualHandlers)
      {
        EventHandler<T> handler = actualHandler.Handler;
        ContextEventWrapper<T>.SynchronizationContextIdentifier contextId = actualHandler.ContextId;
        if (contextIdentifier == contextId && handler == originalHandler)
        {
          contextHandlerPair = actualHandler;
          break;
        }
      }
      if (contextHandlerPair == null)
        return;
      this.actualHandlers.Remove(contextHandlerPair);
    }

    public void Invoke(object sender, T eventArgs)
    {
      if (!this.HasHandlers)
        return;
      foreach (ContextEventWrapper<T>.ContextHandlerPair actualHandler in (IEnumerable<ContextEventWrapper<T>.ContextHandlerPair>) this.actualHandlers)
      {
        EventHandler<T> handler = actualHandler.Handler;
        SynchronizationContext context = actualHandler.ContextId.Context;
        if (context == null)
          handler(sender, eventArgs);
        else if (this.method == ContextSynchronizationMethod.Post)
          context.Post(new SendOrPostCallback(this.SendOrPostDelegate), (object) new ContextEventWrapper<T>.ContextEventHandlerArgsWrapper(handler, sender, eventArgs));
        else if (this.method == ContextSynchronizationMethod.Send)
          context.Send(new SendOrPostCallback(this.SendOrPostDelegate), (object) new ContextEventWrapper<T>.ContextEventHandlerArgsWrapper(handler, sender, eventArgs));
      }
    }

    public void Dispose()
    {
      this.isDisposed = true;
      this.actualHandlers.Clear();
    }

    private void SendOrPostDelegate(object state)
    {
      if (this.isDisposed)
        return;
      ContextEventWrapper<T>.ContextEventHandlerArgsWrapper handlerArgsWrapper = (ContextEventWrapper<T>.ContextEventHandlerArgsWrapper) state;
      handlerArgsWrapper.Handler(handlerArgsWrapper.Sender, handlerArgsWrapper.Args);
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
      private object dispatcherObject;

      public SynchronizationContextIdentifier(SynchronizationContext context)
      {
        this.Context = context;
        if (context == null)
          return;
        Type type = context.GetType();
        if (!("DispatcherSynchronizationContext" == type.Name))
          return;
        FieldInfo field = type.GetField("_dispatcher", BindingFlags.Instance | BindingFlags.NonPublic);
        if (!(field != (FieldInfo) null))
          return;
        this.dispatcherObject = field.GetValue((object) context);
      }

      public SynchronizationContext Context { get; private set; }

      public static bool operator ==(
        ContextEventWrapper<T>.SynchronizationContextIdentifier contextId1,
        ContextEventWrapper<T>.SynchronizationContextIdentifier contextId2)
      {
        return (object) contextId1 != null && (object) contextId2 != null && contextId1.Equals(contextId2);
      }

      public static bool operator !=(
        ContextEventWrapper<T>.SynchronizationContextIdentifier contextId1,
        ContextEventWrapper<T>.SynchronizationContextIdentifier contextId2)
      {
        return (object) contextId1 == null || (object) contextId2 == null || !contextId1.Equals(contextId2);
      }

      public override int GetHashCode()
      {
        if (this.dispatcherObject != null)
          return this.dispatcherObject.GetHashCode();
        return this.Context != null ? this.Context.GetHashCode() : 0;
      }

      public override bool Equals(object obj)
      {
        ContextEventWrapper<T>.SynchronizationContextIdentifier contextId = obj as ContextEventWrapper<T>.SynchronizationContextIdentifier;
        return (object) contextId != null && this.Equals(contextId);
      }

      public bool Equals(
        ContextEventWrapper<T>.SynchronizationContextIdentifier contextId)
      {
        if ((object) contextId == null)
          return false;
        return this.dispatcherObject == null && contextId.dispatcherObject == null ? this.Context == contextId.Context : this.dispatcherObject == contextId.dispatcherObject;
      }
    }

    private class ContextHandlerPair
    {
      public ContextHandlerPair(
        EventHandler<T> handler,
        ContextEventWrapper<T>.SynchronizationContextIdentifier contextId)
      {
        this.Handler = handler;
        this.ContextId = contextId;
      }

      public ContextEventWrapper<T>.SynchronizationContextIdentifier ContextId { get; private set; }

      public EventHandler<T> Handler { get; private set; }
    }
  }
}
