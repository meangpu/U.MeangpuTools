using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Meangpu
{
    public class ButtonSelectionHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        // solution from [Handle UI Like a Commercial Game (Custom Animations + Different Control Schemes) | Unity Tutorial - YouTube](https://www.youtube.com/watch?v=u3YdlUW1nx0)
        // the problem is cannot change ui to left right when change from keyboard to controller this help fix that problem

        [SerializeField] float _verticalMove = 10;
        [SerializeField] float _moveTime = .1f;
        [Range(0, 2)]
        [SerializeField] float _scaleAmount = 1.05f;
        private Vector3 _startPos;
        private Vector3 _startScale;
        // for add SO Sound on Move In out

        void Start()
        {
            _startPos = transform.position;
            _startScale = transform.localScale;
        }
        private IEnumerator MoveUI(bool startingAnimation)
        {
            Vector3 endPos;
            Vector3 endScale;
            float elapseTime = 0;
            while (elapseTime < _moveTime)
            {
                elapseTime += Time.deltaTime;
                if (startingAnimation)
                {
                    endPos = _startPos + new Vector3(0, _verticalMove, 0);
                    endScale = _startScale * _scaleAmount;
                }
                else
                {
                    endPos = _startPos;
                    endScale = _startScale;
                }
                Vector3 lerpPos = Vector3.Lerp(transform.position, endPos, elapseTime / _moveTime);
                Vector3 lerpScale = Vector3.Lerp(transform.localScale, endScale, elapseTime / _moveTime);
                transform.position = lerpPos;
                transform.localScale = lerpScale;
                yield return null;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            eventData.selectedObject = gameObject;
        }

        public void OnSelect(BaseEventData eventData)
        {
            StartCoroutine(MoveUI(true));
            ButtonSelectionManager.Instance.LastSelected = gameObject;
            for (var i = 0; i < ButtonSelectionManager.Instance.AllButton.Length; i++)
            {
                if (ButtonSelectionManager.Instance.AllButton[i] == gameObject)
                {
                    ButtonSelectionManager.Instance.LastSelectedIndex = i;
                    return;
                }
            }
        }

        public void OnDeselect(BaseEventData eventData)
        {
            StartCoroutine(MoveUI(false));
        }
    }
}