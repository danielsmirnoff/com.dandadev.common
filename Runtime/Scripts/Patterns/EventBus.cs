using System;
using System.Collections.Generic;

namespace CommonDan
{
    /// <summary>
    /// A generic eventbus pattern
    /// Used to decouple objects by using channels instead
    /// </summary>
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> events = new();

        public static void Subscribe<T>(Action<T> listener)
        {
            if (events.ContainsKey(typeof(T))) {
                events[typeof(T)] = Delegate.Combine(events[typeof(T)], listener);
            } else {
                events[typeof(T)] = listener;
            }
        }

        public static void Unsubscribe<T>(Action<T> listener)
        {
            if (!events.ContainsKey(typeof(T))) return;
            var current = Delegate.Remove(events[typeof(T)], listener);
            if (current == null) events.Remove(typeof(T));
            else events[typeof(T)] = current;
        }

        public static void Raise<T>(T evt)
        {
            if (events.TryGetValue(typeof(T), out var del))
            {
                (del as Action<T>)?.Invoke(evt);
            }
        }
    }
}