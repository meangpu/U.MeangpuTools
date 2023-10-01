using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Meangpu.Util
{
    public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
    {
        [SerializeField] RectTransform _dragRectTransform;
        [SerializeField] Canvas _canvas;
        [SerializeField] Image _imgTransWhenDrag;
        [SerializeField] float _onDragAlpha = 0.4f;
        Color _bgColor;

        [Header("OptionalImageColorChange")]
        [SerializeField] Image _imgToChangeColor;
        [SerializeField] Color _colorToChangeWhenDrag;
        Color _defaultColor;

        void Start()
        {
            _bgColor = _imgTransWhenDrag.color;
            if (_canvas == null) _canvas = transform.GetComponentInParent<Canvas>();
            if (_dragRectTransform == null) _dragRectTransform = transform.GetComponent<RectTransform>();
            if (_imgToChangeColor == null) return;
            _defaultColor = _imgToChangeColor.color;
        }

        public void OnDrag(PointerEventData eventData) => _dragRectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

        private void ChangeImageBGAlpha(float newValue)
        {
            _bgColor.a = newValue;
            _imgTransWhenDrag.color = _bgColor;
        }

        private void ChangeImageColor(Color newColor)
        {
            if (_imgToChangeColor == null) return;
            _imgToChangeColor.color = newColor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            ChangeImageBGAlpha(_onDragAlpha);
            ChangeImageColor(_colorToChangeWhenDrag);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ChangeImageBGAlpha(1);
            ChangeImageColor(_defaultColor);
        }

        public void OnPointerDown(PointerEventData eventData) => _dragRectTransform.SetAsLastSibling();
    }
}