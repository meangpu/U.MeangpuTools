using System.Linq;
using UnityEngine;

namespace Meangpu.Util
{
    // use with code_monkey concept [Simple Framerate FPS Counter in 30 SECONDS! - YouTube](https://www.youtube.com/shorts/I2r97r9h074)
    public class FpsCountDisplay : MonoBehaviour
    {
        int _lastFrameIndex;
        float[] _frameDeltaTimeArray;
        float _frameNum;

        private void Awake() => _frameDeltaTimeArray = new float[50];
        private void Update()
        {
            _frameDeltaTimeArray[_lastFrameIndex] = Time.unscaledDeltaTime;
            _lastFrameIndex = (_lastFrameIndex + 1) % _frameDeltaTimeArray.Length;
            _frameNum = CalculateFPS();
        }
        float CalculateFPS() => _frameDeltaTimeArray.Length / _frameDeltaTimeArray.Sum();
        void OnGUI() => GUI.Label(new Rect(10, 10, 100, 20), _frameNum.ToString("f0"));
    }

}