using UnityEngine;
using VInspector;
using UnityEngine.UI;

namespace Meangpu.Util
{
    public class RotateCameraWithMouse : MonoBehaviour
    {
        [SerializeField] Camera _cam;
        [SerializeField] Transform _camTransform;

        [SerializeField] Vector3 _offset = new(0, 0, -10);

        [Tooltip("Clamp still error don't use")]
        [SerializeField] bool _clampRotation;
        [ShowIf("_clampRotation")]
        [SerializeField] Vector3 _minClampRot;
        [SerializeField] Vector3 _maxClampRot;
        [EndIf]

        [SerializeField] bool _canScrollToZoom;
        [ShowIf("_canScrollToZoom")]
        [SerializeField] float _scrollZoomSpeed = 2;
        [MinMaxSlider(-99999f, 99909f, "_maxZoom", "ZoomClamp")]
        [SerializeField] float _minZoom = 0.8f;
        [HideInInspector]
        [SerializeField] float _maxZoom = 1.2f;
        [EndIf]

        [SerializeField] bool _useZoomSlider;
        [ShowIf("_useZoomSlider")]
        [SerializeField] Slider _slider;
        [EndIf]

        [Header("Key")]
        [SerializeField] SOMeKeyCode _rotateKey;
        [SerializeField] SOMeKeyCode _panKey;

        Vector3 _previousPosRotate;
        Vector3 _lastMousePosPan;
        Vector3 _clampVector;

        [SerializeField] float _panSpeed = 0.1f;

        void Start()
        {
            if (_cam == null) _cam = Camera.main;
            if (_camTransform == null) _camTransform = _cam.transform;
            SetCameraPosition();

            if (_useZoomSlider && _slider != null)
            {
                _slider.minValue = _minZoom;
                _slider.maxValue = _maxZoom;
                _slider.onValueChanged.AddListener(UpdateZValue);
            }
        }

        void UpdateZValue(float newValue) => _offset.z = newValue;

        void Update()
        {
            PanMouse();
            RotateAroundObj();
            ZoomInOut();

            SetCameraPosition();
        }

        private void ZoomInOut()
        {
            if (Input.mouseScrollDelta.y > 0) _offset.z += _scrollZoomSpeed;// mouse up
            if (Input.mouseScrollDelta.y < 0) _offset.z -= _scrollZoomSpeed;// mouse down

            _offset.z = Mathf.Clamp(_offset.z, _minZoom, _maxZoom);
            if (_useZoomSlider && _slider != null) _slider.value = _offset.z;
        }

        private void RotateAroundObj()
        {
            if (Input.GetKeyDown(_rotateKey.KeyCode))
            {
                _previousPosRotate = _cam.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.GetKey(_rotateKey.KeyCode))
            {
                Vector3 direction = _previousPosRotate - _cam.ScreenToViewportPoint(Input.mousePosition);
                _camTransform.position = new Vector3();
                _camTransform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                _camTransform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
                _previousPosRotate = _cam.ScreenToViewportPoint(Input.mousePosition);

                // clamp still error
                if (!_clampRotation) return;
                _clampVector = _camTransform.rotation.eulerAngles;
                _clampVector.x = Mathf.Clamp(_clampVector.x, _minClampRot.x, _maxClampRot.x);
                _clampVector.y = Mathf.Clamp(_clampVector.y, _minClampRot.y, _maxClampRot.y);
                _clampVector.z = Mathf.Clamp(_clampVector.z, _minClampRot.z, _maxClampRot.z);
                _camTransform.rotation = Quaternion.Euler(_clampVector);
            }
        }

        private void PanMouse()
        {
            if (Input.GetKeyDown(_panKey.KeyCode))
            {
                _lastMousePosPan = Input.mousePosition;
            }

            if (Input.GetKey(_panKey.KeyCode))
            {
                Vector3 _delta = Input.mousePosition - _lastMousePosPan;
                _offset += new Vector3(-_delta.x * _panSpeed, -_delta.y * _panSpeed, 0);
                _lastMousePosPan = Input.mousePosition;
            }
        }

        private void SetCameraPosition()
        {
            _camTransform.position = new Vector3();
            _camTransform.Translate(_offset);
        }
    }
}