using System;
using DG.Tweening;
using Ryan;
using UnityEngine;

namespace Meangpu
{
    public class TimeScaleSetter : BaseMeSingleton<TimeScaleSetter>
    {
        [SerializeField] FloatReference _timeScale;
        Sequence _sequence;
        bool _isSlowing;
        public bool IsSlowing => _isSlowing;
        public static Action<bool> OnTimeIsSlowStateChange;

        private void Update() => UpdateTimeScale();

        public void ResetTimeScale() => SetTimeScale(1);

        public void UpdateIsSlowState(bool newState)
        {
            if (newState == _isSlowing) return;
            _isSlowing = newState;
            OnTimeIsSlowStateChange?.Invoke(_isSlowing);
        }

        private void UpdateTimeScale()
        {
            Time.timeScale = _timeScale;
            Time.fixedDeltaTime = _timeScale * Time.deltaTime;
        }

        public void SetTimeScale(float newValue)
        {
            _timeScale.Variable.SetValue(newValue);
            _sequence.Kill();
            UpdateIsSlowState(false);
            UpdateTimeScale();
        }

        public void SlowTimeForSecond(float timeScale = .1f, float duration = 1f)
        {
            if (_isSlowing) return;
            UpdateIsSlowState(true);

            _sequence.Kill();

            _timeScale.Variable.SetValue(timeScale);
            _sequence = DOTween.Sequence();

            _sequence.Append(DOTween.To(() => _timeScale, x => _timeScale.Variable.SetValue(x), 1, duration).SetEase(Ease.InQuad).SetUpdate(true));

            _sequence.OnComplete(() => UpdateIsSlowState(true));
        }
    }
}
