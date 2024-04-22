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

        private void Update() => UpdateTimeScale();

        public void ResetTimeScale() => SetTimeScale(1);

        private void UpdateTimeScale()
        {
            Time.timeScale = _timeScale;
            Time.fixedDeltaTime = _timeScale * Time.deltaTime;
        }

        public void SetTimeScale(float newValue)
        {
            _timeScale.Variable.SetValue(newValue);
            _sequence.Kill();
            _isSlowing = false;
            UpdateTimeScale();
        }

        public void SlowTimeForSecond(float timeScale = .1f, float duration = 1f)
        {
            if (_isSlowing) return;
            _isSlowing = true;

            _sequence.Kill();

            _timeScale.Variable.SetValue(timeScale);
            _sequence = DOTween.Sequence();

            _sequence.Append(DOTween.To(() => _timeScale, x => _timeScale.Variable.SetValue(x), 1, duration).SetEase(Ease.InQuad).SetUpdate(true));

            _sequence.OnComplete(() => _isSlowing = false);
        }
    }
}
