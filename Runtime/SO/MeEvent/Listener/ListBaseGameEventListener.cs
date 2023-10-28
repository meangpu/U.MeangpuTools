using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Meangpu.SOEvent
{
    public abstract class ListBaseGameEventListener<T, E, UER> : MonoBehaviour, IGameEventListener<T> where E : SOBaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] List<E> _gameEvent;
        public List<E> GameEvent { get { return _gameEvent; } set { _gameEvent = value; } }
        [SerializeField] UER _unityEventRespond;

        void OnEnable()
        {
            if (_gameEvent == null) return;
            foreach (E SoEvent in _gameEvent)
            {
                SoEvent.RegisterListener(this);
            }
        }
        void OnDisable()
        {
            if (_gameEvent == null) return;
            foreach (E SoEvent in _gameEvent)
            {
                SoEvent.RegisterListener(this);
            }
        }

        public void OnEventRaised(T data)
        {
            if (_unityEventRespond != null) _unityEventRespond?.Invoke(data);
        }
    }
}
