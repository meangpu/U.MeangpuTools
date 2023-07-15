using UnityEngine;

namespace Meangpu.Util
{
    public class RotateModelAroundObject : MonoBehaviour
    {
        [SerializeField] Transform _objectToRotateAround;
        [SerializeField] Vector3 _rotSpeed = new(0, 1, 0);
        [SerializeField] float _speed;

        void RotateAroundObject() => transform.RotateAround(_objectToRotateAround.position, _rotSpeed, _speed * Time.deltaTime);
        private void FIxedUpdate() => RotateAroundObject();
    }
}