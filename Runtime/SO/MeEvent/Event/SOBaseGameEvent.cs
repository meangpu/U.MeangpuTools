using System.Collections.Generic;
using VInspector;
using UnityEngine;

namespace Meangpu.SOEvent
{
    public abstract class SOBaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> _eventListener = new();

        [Button]
        public void Raise(T data)
        {
            for (int i = _eventListener.Count - 1; i >= 0; i--)
            {
                _eventListener[i].OnEventRaised(data);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!_eventListener.Contains(listener)) _eventListener.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            if (_eventListener.Contains(listener)) _eventListener.Remove(listener);
        }
    }
}