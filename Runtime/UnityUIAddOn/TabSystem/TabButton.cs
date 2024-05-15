using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

namespace Meangpu
{
    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        // learn from [Creating a Custom Tab System in Unity - YouTube](https://www.youtube.com/watch?v=211t6r12XPQ&t=298s)
        [SerializeField] TabGroup _tabGroup;
        Image _background;

        [SerializeField] UnityEvent _onTabSelected;
        [SerializeField] UnityEvent _onTabDeSelected;

        void Start()
        {
            _background = GetComponent<Image>();
            _tabGroup.Subscribe(this);
        }

        public void SetBackground(Sprite newImage) => _background.sprite = newImage;
        public void SetColor(Color newColor) => _background.color = newColor;

        public void OnPointerClick(PointerEventData eventData) => _tabGroup.OnTabSelect(this);
        public void OnPointerEnter(PointerEventData eventData) => _tabGroup.OnTabEnter(this);
        public void OnPointerExit(PointerEventData eventData) => _tabGroup.OnTabExit(this);
        public void Select() => _onTabSelected?.Invoke();
        public void DeSelect() => _onTabDeSelected?.Invoke();
    }
}
