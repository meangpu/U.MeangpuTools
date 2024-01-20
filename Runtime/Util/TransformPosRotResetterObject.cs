using UnityEngine;

namespace Meangpu.Util
{
    public class TransformPosRotResetterObject : MonoBehaviour
    {
        Vector3 _startLocalPos;
        Quaternion _startLocalRot;

        Vector3 _startPos;
        Quaternion _startRot;

        Rigidbody _rb;

        [SerializeField] bool _resetVelocityOnReset = true;

        private void Awake()
        {
            _startLocalPos = transform.localPosition;
            _startLocalRot = transform.localRotation;

            _startPos = transform.position;
            _startRot = transform.rotation;
            TryGetComponent(out _rb);
        }

        public void DoResetLocalPosRot()
        {
            DoResetVelocity();
            transform.SetPositionAndRotation(_startLocalPos, _startLocalRot);
        }

        public void DoResetWorldPosRot()
        {
            DoResetVelocity();
            transform.SetPositionAndRotation(_startPos, _startRot);
        }

        public void DoResetVelocity()
        {
            if (!_resetVelocityOnReset || _rb == null) return;
            _rb.velocity = Vector3.zero;
        }
    }
}
