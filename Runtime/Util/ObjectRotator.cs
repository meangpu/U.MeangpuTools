using UnityEngine;

namespace Meangpu.Util
{
    public class ObjectRotator : MonoBehaviour
    {
        [SerializeField] float _sensitivity = .4f;
        private Vector3 _mouseReference;
        private Vector3 _mouseOffset;
        private bool _isRotating;

        void Update()
        {
            if (_isRotating)
            {
                _mouseOffset = Input.mousePosition - _mouseReference;

                float verticalRotation = -_mouseOffset.x * _sensitivity;
                float horizontalRotation = _mouseOffset.y * _sensitivity;

                transform.Rotate(Vector3.up, verticalRotation);
                transform.Rotate(Vector3.right, horizontalRotation);

                _mouseReference = Input.mousePosition;
            }
        }

        void OnMouseDown()
        {
            _isRotating = true;
            _mouseReference = Input.mousePosition;
        }

        void OnMouseUp() => _isRotating = false;

    }
}
