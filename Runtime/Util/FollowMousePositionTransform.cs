using UnityEngine;

namespace Meangpu.Util
{
    public class FollowMousePositionTransform : MonoBehaviour
    {
        Camera _mainCamera;
        Vector3 nowPos;
        [SerializeField] float _distance = 1;
        void Start() => _mainCamera = Camera.main;
        void Update()
        {
            nowPos = Input.mousePosition;
            nowPos.z = _distance;
            transform.position = _mainCamera.ScreenToWorldPoint(nowPos);
        }
    }
}
