using UnityEngine;
using UnityEngine.EventSystems;
using Meangpu.Audio;
using DG.Tweening;
using UnityEngine.UI;

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

        [SerializeField] Button _button;

        private void Awake() => _defaultTransform = transform.localScale;
        private void OnDisable() => DOTween.KillAll();

        bool SkipFeedbackBecauseButton()
        {
            if (_button == null) return false;
            if (_button.IsInteractable()) return false;
            return true;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (SkipFeedbackBecauseButton()) return;
            _clickSound?.PlayOneShot();
            transform.DOScale(_defaultTransform, _animationTime).SetEase(_easeType).SetUpdate(true);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (SkipFeedbackBecauseButton()) return;
            _hoverSound?.PlayOneShot();
            transform.DOScale(_hoverTransform, _animationTime).SetEase(_easeType).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (SkipFeedbackBecauseButton()) return;
            _exitSound?.PlayOneShot();
            transform.DOScale(_defaultTransform, _animationTime).SetEase(_easeType).SetUpdate(true);
        }
    }
}