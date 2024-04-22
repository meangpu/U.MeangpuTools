using Ryan;
using UnityEngine;

namespace Meangpu
{
    public class TimeScaleSetter : BaseMeSingleton<TimeScaleSetter>
    {
        [SerializeField] FloatReference _timeScale;

        private void Update() => UpdateTimeScale();

        public void ResetTimeScale()
        {
            _timeScale.Variable.SetValue(1);
            UpdateTimeScale();
        }

        private void UpdateTimeScale()
        {
            Time.timeScale = _timeScale;
            Time.fixedDeltaTime = _timeScale * Time.deltaTime;
        }
    }
}
