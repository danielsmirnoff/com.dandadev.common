using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseGameEvent<T> : ScriptableObject
{
    private readonly List<IGameEventListener<T>> _listeners = new();
    private readonly List<Action<T>> _actions = new();

    public void Raise(T value)
    {
        // Iterate backwards for priority
        for (int i = _listeners.Count - 1; i >= 0; i--)
        {
            _listeners[i].OnEventRaised(value);
        }

        for (int i = _actions.Count - 1; i >= 0; i--)
        {
            _actions[i]?.Invoke(value);
        }
    }

    public void Subscribe(IGameEventListener<T> listener)
    {
        if (!_listeners.Contains(listener))
        {
            _listeners.Add(listener);
        }
    }

    public void Unsubscribe(IGameEventListener<T> listener) => _listeners.Remove(listener);
    
    // For code-side subscriptions
    public void Subscribe(Action<T> action)
    {
        if (!_actions.Contains(action)) _actions.Add(action);
            
    }

    public void Unsubscribe(Action<T> action)  => _actions.Remove(action);
}