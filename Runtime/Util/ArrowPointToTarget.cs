using UnityEngine;

namespace Meangpu.Util
{
    public class ArrowPointToTarget : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] float _rotSpeed = 10f;
        [SerializeField] Vector3 _allowRotationAxis = Vector3.one;
        Vector3 _tempRot;

        private void Update()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.position - transform.position), _rotSpeed * Time.deltaTime);

            _tempRot = transform.localEulerAngles;
            _tempRot = Vector3.Scale(_tempRot, _allowRotationAxis);
            transform.localEulerAngles = _tempRot;
        }
    }
}