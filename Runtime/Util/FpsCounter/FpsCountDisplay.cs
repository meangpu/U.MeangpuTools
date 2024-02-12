using UnityEngine;
using TMPro;

namespace Meangpu.Util
{
    public class FpsCountDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _fpsText = null;
        private FrameRateCalculator _frameRate = new();

        private void Update() => UpdateFrameRate();
        private void UpdateFrameRate()
        {
            if (_frameRate.Update(Time.unscaledDeltaTime)) _fpsText.text = _frameRate.GetIntFrameRate().ToString();
        }
    }
}