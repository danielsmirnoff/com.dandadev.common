using System;
using System.Collections.Generic;
using UnityEngine;

namespace CommonDan
{
    [CreateAssetMenu(menuName = "EngineData/EventBus/Game Event")]
    public class GameEvent : ScriptableObject
    {
        private readonly List<IGameEventListener> _listeners = new();
        private readonly List<Action> _actions = new();

        public void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }

            for (int i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i]?.Invoke();
            }
        }

        public void Subscribe(IGameEventListener l)
        {
            if (!_listeners.Contains(l)) _listeners.Add(l);
        }

        public void Unsubscribe(IGameEventListener l) => _listeners.Remove(l);

        public void Subscribe(Action a)
        {
            if (!_actions.Contains(a)) _actions.Add(a);
        }

        public void Unsubscribe(Action a) => _actions.Remove(a);
    }


    public interface IGameEventListener
    {
        void OnEventRaised();
    }
}