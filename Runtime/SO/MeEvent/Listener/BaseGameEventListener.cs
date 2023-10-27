using UnityEngine;
using UnityEngine.Events;

namespace Meangpu.SOEvent
{
    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour, IGameEventListener<T> where E : SOBaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] E _gameEvent;
        public E GameEvent { get { return _gameEvent; } set { _gameEvent = value; } }
        [SerializeField] UER _unityEventRespond;

        void OnEnable()
        {
            if (_gameEvent == null) return;
            GameEvent.RegisterListener(this);
        }
        void OnDisable()
        {
            if (_gameEvent == null) return;
            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T data)
        {
            if (_unityEventRespond != null) _unityEventRespond?.Invoke(data);
        }
    }
}
