using System;
using System.Collections.Generic;

/// <summary>
/// A string-channeled, generic event bus.
/// </summary>
public static class EventBus
{
    private static readonly Dictionary<string, Delegate> events = new();

    // Typed Events

    public static void Subscribe<T>(string channel, Action<T> listener)
    {
        if (events.TryGetValue(channel, out var existing))
        {
            if (existing is not Action<T>) throw new InvalidOperationException(
                $"Channel '{channel}' is already registered with an incompatible delegate type.");
            events[channel] = Delegate.Combine(existing, listener);
        }
        else
        {
            events[channel] = listener;
        }
    }

    public static void Unsubscribe<T>(string channel, Action<T> listener)
    {
        if (!events.TryGetValue(channel, out var existing)) return;
        var updated = Delegate.Remove(existing, listener);
        if (updated == null) {
            events.Remove(channel);
        } else {
            events[channel] = updated;
        }
    }

    public static void Raise<T>(string channel, T evt)
    {
        if (!events.TryGetValue(channel, out var del)) return;

        var invocationList = del.GetInvocationList();
        List<Exception> errors = null;

        foreach (var handler in invocationList)
        {
            try { ((Action<T>)handler).Invoke(evt); }
            catch (Exception e) { (errors ??= new List<Exception>()).Add(e); }
        }

        if (errors != null) throw new AggregateException(
            $"One or more subscribers threw during Raise<{typeof(T).Name}> on channel '{channel}'.", errors);
    }

    // Parameter-less Events

    public static void Subscribe(string channel, Action listener)
    {
        if (events.TryGetValue(channel, out var existing))
        {
            if (existing is not Action) throw new InvalidOperationException(
                $"Channel '{channel}' is already registered with an incompatible delegate type.");
            events[channel] = Delegate.Combine(existing, listener);
        }
        else
        {
            events[channel] = listener;
        }
    }

    public static void Unsubscribe(string channel, Action listener)
    {
        if (!events.TryGetValue(channel, out var existing)) return;
        var updated = Delegate.Remove(existing, listener);
        
        if (updated == null) {
            events.Remove(channel);
        }else {
            events[channel] = updated;
        }
    }

    public static void Raise(string channel)
    {
        if (!events.TryGetValue(channel, out var del)) return;

        var invocationList = del.GetInvocationList();
        List<Exception> errors = null;

        foreach (var handler in invocationList)
        {
            try { ((Action)handler).Invoke(); }
            catch (Exception e) { (errors ??= new List<Exception>()).Add(e); }
        }

        if (errors != null) throw new AggregateException(
            $"One or more subscribers threw during Raise on channel '{channel}'.", errors);
    }

    // Utility
    /// <summary>Removes all subscribers on a specific channel.</summary>
    public static void Clear(string channel) => events.Remove(channel);

    /// <summary>Removes all subscribers from all channels.</summary>
    public static void ClearAll() => events.Clear();

    /// <summary>Returns true if any subscribers are registered on the given channel.</summary>
    public static bool HasSubscribers(string channel) => events.ContainsKey(channel);
}