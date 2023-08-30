using UnityEngine;

namespace Meangpu.Util
{
    public class DragAndDrop : MonoBehaviour
    {
        Vector3 _mousePos;
        Camera _mainCam;

        private void Start() => _mainCam = Camera.main;
        Vector3 GetMousePos() => _mainCam.WorldToScreenPoint(transform.position);

        void OnMouseDown() => _mousePos = Input.mousePosition - GetMousePos();
        void OnMouseDrag() => transform.position = _mainCam.ScreenToWorldPoint(Input.mousePosition - _mousePos);
    }
}