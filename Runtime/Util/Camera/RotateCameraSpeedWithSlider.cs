using UnityEngine;
using UnityEngine.UI;

namespace Meangpu.Util
{
    public class RotateCameraSpeedWithSlider : MonoBehaviour
    {
        public float Speed = 2f;
        public bool DoRotate = true;
        [SerializeField] Slider _mainSlider;

        void Update()
        {
            if (DoRotate) transform.Rotate(0, Speed * Time.deltaTime, 0);
        }

        public void ToggleRotate()
        {
            if (DoRotate) DoRotate = !DoRotate;
            else DoRotate = !DoRotate;
        }

        public void SetRotSpeed()
        {
            Speed = _mainSlider.value;
        }
    }
}