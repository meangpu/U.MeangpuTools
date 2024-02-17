using UnityEngine;
using UnityEngine.EventSystems;

namespace Meangpu.Util
{
    // this still bug
    public class LookAtMouseUI : MonoBehaviour
    {
        [SerializeField] Camera _targetCamera;
        private RectTransform _rectTransform;

        private void Awake()
        {
            if (_targetCamera == null) _targetCamera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, Input.mousePosition, _targetCamera, out Vector2 localMousePosition);
                float angle = Mathf.Atan2(localMousePosition.y, localMousePosition.x) * Mathf.Rad2Deg;
                _rectTransform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}