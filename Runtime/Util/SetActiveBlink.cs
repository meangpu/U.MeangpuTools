using EasyButtons;
using UnityEngine;
using System.Collections;

namespace Meangpu.Util
{
    public class SetActiveBlink : MonoBehaviour
    {
        [SerializeField] GameObject _target;

        [Header("Duration")]
        [SerializeField] float _onDuration = 1.2f;
        [SerializeField] float _offDuration = .25f;

        [Header("Start state")]
        [SerializeField] bool _objectActiveState = true;
        [SerializeField] bool _blinkOnStart;

        IEnumerator _enumerator;
        bool _isBlinking;

        void OnDisable() => StopAllCoroutines();

        public void SetObjectState(bool state)
        {
            _objectActiveState = state;
            _target?.SetActive(state);
        }

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
