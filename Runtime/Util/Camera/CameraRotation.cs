using UnityEngine;
using Meangpu.Util;

namespace Meangpu.Util
{
    public class CameraRotation : MonoBehaviour
    {
        [SerializeField] Transform _cameraTransformTarget;
        [SerializeField] Transform _targetToRotateAround;

        [SerializeField] float _rotationSpeed = 20f;
        [SerializeField] float _panSpeed = 10f;
        [SerializeField] float _zoomSpeed = 5f;
        [SerializeField] float _smoothTime = 0.1f;

        [Tooltip("0 left / 1 right / 2 middle")]
        [SerializeField] int _mouseToCheckRotate = 0;
        [SerializeField] int _mouseToCheckPan = 2;

        private Vector3 _previousMousePosition;
        private float _currentZoomVelocity;
        private Vector3 _mouseDelta;
        private Vector3 _panDirection;
        private Vector3 _zoomVector;
        Vector3 _cameraToTarget;

        private void Start()
        {
            if (_cameraTransformTarget == null) _cameraTransformTarget = transform;
        }

        private void Update()
        {
            if (_targetToRotateAround != null && Camera.main != null)
            {
                DoRotateByMouse();
                DoPan();
                DoMouseZoom();

                _cameraToTarget = _targetToRotateAround.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(_cameraToTarget, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }

        private void DoPan()
        {
            if (Input.GetMouseButton(_mouseToCheckPan))
            {
                _panDirection = new Vector3(-_mouseDelta.x, 0f, -_mouseDelta.y);
                Vector3 panAmount = _panDirection * _panSpeed * Time.deltaTime;
                transform.Translate(panAmount, Space.Self);
            }
        }

        private void DoMouseZoom()
        {
            float scrollAmount = Input.GetAxis("Mouse ScrollWheel");
            float targetZoom = transform.position.y + (scrollAmount * _zoomSpeed);
            float smoothedZoom = Mathf.SmoothDamp(transform.position.y, targetZoom, ref _currentZoomVelocity, _smoothTime);
            _zoomVector = transform.forward * (smoothedZoom - transform.position.y);
            transform.Translate(_zoomVector, Space.World);
        }

        private void DoRotateByMouse()
        {
            if (Input.GetMouseButton(_mouseToCheckRotate))
            {
                _mouseDelta = Input.mousePosition - _previousMousePosition;

                float rotationX = _rotationSpeed * _mouseDelta.y * Time.deltaTime;
                float rotationY = _rotationSpeed * -_mouseDelta.x * Time.deltaTime;
                const float rotationZ = 0f;

                transform.RotateAround(_targetToRotateAround.position, Vector3.up, rotationY);
                transform.RotateAround(_targetToRotateAround.position, transform.right, rotationX);
                transform.RotateAround(_targetToRotateAround.position, transform.forward, rotationZ);

                _previousMousePosition = Input.mousePosition;
            }
        }
    }
}