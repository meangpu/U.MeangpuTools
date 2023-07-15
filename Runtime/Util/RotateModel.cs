using UnityEngine;

namespace Meangpu.Util
{
    public class RotateModel : MonoBehaviour
    {
        [SerializeField] Vector3 _rotSpeed = new(0, 1, 0);
        Vector3 _nowRotSpeed;

        public void RotateStop() => _nowRotSpeed = Vector3.zero;
        public void RotateStart() => _nowRotSpeed = _rotSpeed;

        private void Start() => _nowRotSpeed = _rotSpeed;

        void FixedUpdate() => transform.Rotate(_nowRotSpeed * Time.deltaTime);
    }
}