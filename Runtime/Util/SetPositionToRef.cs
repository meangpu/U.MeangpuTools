using UnityEngine;

namespace Meangpu
{
    public class SetPositionToRef : MonoBehaviour
    {
        [SerializeField] Transform _ref;
        [SerializeField] Transform _objectToFollow;
        [SerializeField] Vector3 _offset;

        /// <summary>
        /// First used in horseLeg project
        /// </summary>
        private void Update() => _objectToFollow.position = _ref.position + _offset;
    }
}