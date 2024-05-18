using UnityEngine;
using UnityEngine.EventSystems;
using Meangpu.Audio;
using DG.Tweening;

namespace Meangpu
{
    public class UIHoverClickFeedback : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] SOSound _hoverSound;
        [SerializeField] SOSound _clickSound;
        [SerializeField] SOSound _exitSound;

        Vector3 _defaultTransform;
        [SerializeField] float _hoverTransform = 1.04f;
        [SerializeField] float _animationTime = .1f;
        [SerializeField] Ease _easeType = Ease.InOutSine;

        private void Awake() => _defaultTransform = transform.localScale;

        private void OnDisable()
        {
            DOTween.KillAll();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _clickSound?.PlayOneShot();
            transform.DOScale(_defaultTransform, _animationTime).SetEase(_easeType);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hoverSound?.PlayOneShot();
            transform.DOScale(_hoverTransform, _animationTime).SetEase(_easeType);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _exitSound?.PlayOneShot();
            transform.DOScale(_defaultTransform, _animationTime).SetEase(_easeType);
        }
    }
}