using System;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

namespace Meangpu.Util
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] Transform _camTransform;
        [SerializeField]
        Vector2 _clampMinMax = new(-3, -.6f);

        [SerializeField] bool _isUseMouseSlider = true;

        [ShowIf("_isUseMouseSlider")]
        [SerializeField] float _scrollZoomSpeed = 0;
        [EndIf()]

        [SerializeField] float _scrollSmoothDelta = 10f;
        [SerializeField] float _startOffset = .3f;

        [SerializeField] bool _isUsingSlider;

        [ShowIf("_isUsingSlider")]
        [SerializeField] Slider _sliderValue;

        float _targetZPos;
        Vector3 _targetPos = new();

        void Start()
        {
            _targetZPos = _startOffset;

            if (!_isUsingSlider || _sliderValue == null) return;
            UpdateSliderValue();
            _sliderValue.onValueChanged.AddListener(UpdateZValue);
        }

        private void UpdateSliderValue()
        {
            if (!_isUsingSlider || _sliderValue == null) return;
            _sliderValue.minValue = _clampMinMax.x;
            _sliderValue.maxValue = _clampMinMax.y;
            _sliderValue.value = _startOffset;
        }

        public void UpdateZValue(float pos) => _targetZPos = pos;

        void Update()
        {
            if (_isUseMouseSlider) ZoomInOut();
            _targetPos.Set(_camTransform.position.x, _camTransform.position.y, _targetZPos);
            _camTransform.position = Vector3.Lerp(_camTransform.position, _targetPos, _scrollSmoothDelta * Time.deltaTime);
        }

        private void ZoomInOut()
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                _targetZPos += _scrollZoomSpeed;// mouse up
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                _targetZPos -= _scrollZoomSpeed;// mouse down
            }

            _targetZPos = Mathf.Clamp(_targetZPos, _clampMinMax.x, _clampMinMax.y);

            if (!_isUsingSlider || _sliderValue == null) return;
            _sliderValue.value = _targetZPos;
        }
    }
}
