using UnityEngine;

namespace Meangpu.Util
{
    public class LookAtMouse : MonoBehaviour
    {
        [SerializeField] Camera _targetCamera;

        private void Awake()
        {
            if (_targetCamera == null) _targetCamera = Camera.main;
        }

        private void Update()
        {
            Vector3 targetDir = _targetCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg) - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
