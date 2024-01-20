using VInspector;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace Meangpu.Util
{
    public abstract class BlinkSystem : MonoBehaviour
    {
        [Header("Duration")]
        [SerializeField] protected float _onDuration = 1.2f;
        [SerializeField] protected float _offDuration = .25f;

        [Header("Start state")]
        [SerializeField] protected bool _objectActiveState = true;
        [SerializeField] protected bool _blinkOnStart;

        [Header("Unity event")]
        [SerializeField] UnityEvent _onBlinkOnEvent;
        [SerializeField] UnityEvent _onBlinkOffEvent;

        IEnumerator _enumerator;
        bool _isBlinking;

        void OnDisable() => StopAllCoroutines();

        public void SetObjectState(bool state)
        {
            _objectActiveState = state;
            BlinkAction(state);
            if (state) _onBlinkOffEvent?.Invoke();
            else _onBlinkOnEvent?.Invoke();
        }

        public abstract void BlinkAction(bool nowState);

        private void Start()
        {
            SetObjectState(_objectActiveState);
            if (_blinkOnStart) StartBlink();
        }

        [Button]
        public void StartBlink()
        {
            StopAllCoroutines();
            _enumerator = Blink();
            _isBlinking = true;
            StartCoroutine(_enumerator);
        }

        [Button]
        public void StopBlink()
        {
            _isBlinking = false;
            if (_enumerator != null) StopCoroutine(_enumerator);
            _enumerator = null;
        }

        [Button]
        public void ToggleObjState()
        {
            _objectActiveState = !_objectActiveState;
            SetObjectState(_objectActiveState);
        }

        IEnumerator Blink()
        {
            while (_isBlinking)
            {
                ToggleObjState();
                yield return new WaitForSecondsRealtime(_offDuration);
                ToggleObjState();
                yield return new WaitForSecondsRealtime(_onDuration);
            }
        }
    }
}
