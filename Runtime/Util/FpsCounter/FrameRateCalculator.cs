using UnityEngine;

// original from FirstGearGames.Utilities.Maths
// asset from [Smooth Camera Shaker | Camera | Unity Asset Store](https://assetstore.unity.com/packages/tools/camera/smooth-camera-shaker-162991)
namespace Meangpu.Util
{
    public class FrameRateCalculator
    {
        #region Private.
        private float _timePassed = 0f;
        private float _frames = 0;
        #endregion

        #region Const.
        private const int RESET_FRAME_COUNT = 60;
        private const float CALCULATION_SLICE_PERCENT = 0.7f;
        #endregion

        public int GetIntFrameRate()
        {
            return Mathf.CeilToInt(_frames / _timePassed);
        }
        public float GetFloatFrameRate()
        {
            return _frames / _timePassed;
        }

        public bool Update(float unscaledDeltaTime)
        {
            _timePassed += unscaledDeltaTime;
            _frames++;

            if (_frames > RESET_FRAME_COUNT)
            {
                _frames *= CALCULATION_SLICE_PERCENT;
                _timePassed *= CALCULATION_SLICE_PERCENT;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}