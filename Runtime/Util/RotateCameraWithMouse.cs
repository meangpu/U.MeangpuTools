using UnityEngine;
using Meangpu;

namespace Meangpu.Util
{
    public class RotateCameraWithMouse : MonoBehaviour
    {
        [SerializeField] Camera _cam;

        [SerializeField] Vector3 _offset = new(0, 0, -10);
        [SerializeField] Vector3 _scrollZoomSpeed = new(0, 0, 5);

        [SerializeField] float _clampZMin = -20;
        [SerializeField] float _clampZMax = 0;

        [Header("Key")]
        [SerializeField] SOMeKeyCode _rotateKey;
        [SerializeField] SOMeKeyCode _panKey;

        Vector3 _previousPosRotate;
        Vector3 _lastMousePosPan;

        [SerializeField] float _panSpeed = 0.1f;

        void Start()
        {
            if (_cam == null) _cam = Camera.main;
            SetCameraPosition();
        }

        void Update()
        {
            PanMouse();
            RotateAroundObj();
            ZoomInOut();

            _offset.z = Mathf.Clamp(_offset.z, _clampZMin, _clampZMax);  // clamp z value
            SetCameraPosition();
        }

        private void ZoomInOut()
        {
            if (Input.mouseScrollDelta.y > 0) _offset += _scrollZoomSpeed;// mouse up
            if (Input.mouseScrollDelta.y < 0) _offset -= _scrollZoomSpeed;// mouse down
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
                _cam.transform.position = new Vector3();
                _cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 180);
                _cam.transform.Rotate(new Vector3(0, 1, 0), -direction.x * 180, Space.World);
                _previousPosRotate = _cam.ScreenToViewportPoint(Input.mousePosition);
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
            _cam.transform.position = new Vector3();
            _cam.transform.Translate(_offset);
        }
    }
}