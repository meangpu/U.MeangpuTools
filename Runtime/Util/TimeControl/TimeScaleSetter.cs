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

        [SerializeField] bool _resetTimeScaleOnStart = true;
        [SerializeField] Ease _easeInSlowMotionType;
        [SerializeField] Ease _easeOutSlowMotionType;

        void Start()
        {
            if (_resetTimeScaleOnStart) ResetTimeScale();
        }

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

        public void SlowTimeForSecond(float targetTimeScale = .001f, float slowDurationSecond = 4, float durationFadeInSlow = 1f, float durationFadeOutOfSlow = 1f)
        {
            if (_isSlowing) return;
            UpdateIsSlowState(true);

            _sequence.Kill();

            _sequence = DOTween.Sequence().SetUpdate(true);

            _sequence.Append(DOTween.To(() => _timeScale.Value, x => _timeScale.Variable.SetValue(x), targetTimeScale, durationFadeInSlow).SetEase(_easeInSlowMotionType));

            _sequence.AppendInterval(slowDurationSecond);

            _sequence.Append(DOTween.To(() => _timeScale.Value, x => _timeScale.Variable.SetValue(x), 1, durationFadeOutOfSlow).SetEase(_easeOutSlowMotionType));

            _sequence.OnComplete(() => UpdateIsSlowState(false));
        }
    }
}
