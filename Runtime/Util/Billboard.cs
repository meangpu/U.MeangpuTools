using UnityEngine;

namespace Meangpu.Util
{
    public class Billboard : MonoBehaviour
    {
        private enum Mode
        {
            LookAt,
            LookAtInverted,
            CameraForward,
            CameraForwardInverted,
        }

        [SerializeField] Mode _cameraMode = Mode.CameraForward;
        Camera _mainCamera;

        void Start() => _mainCamera = Camera.main;

        void LateUpdate()
        {
            switch (_cameraMode)
            {
                case Mode.LookAt:
                    transform.LookAt(_mainCamera.transform);
                    break;
                case Mode.LookAtInverted:
                    Vector3 dirFromCamera = transform.position - _mainCamera.transform.position;
                    transform.LookAt(transform.position + dirFromCamera);
                    break;
                case Mode.CameraForward:
                    transform.forward = _mainCamera.transform.forward;
                    break;
                case Mode.CameraForwardInverted:
                    transform.forward = -_mainCamera.transform.forward;
                    break;
            }
        }
    }
}