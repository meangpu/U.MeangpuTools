using UnityEngine;

namespace Meangpu.Util
{
    public class ArrowPointToTarget : MonoBehaviour
    {
        [SerializeField] Transform _target;
        [SerializeField] float _rotSpeed = 10f;

        private void Update()
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_target.position - transform.position), _rotSpeed * Time.deltaTime);
        }
    }
}