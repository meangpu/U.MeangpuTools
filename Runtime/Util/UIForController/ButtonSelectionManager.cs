using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Meangpu
{
    public class ButtonSelectionManager : MonoBehaviour
    {
        // solution from [Handle UI Like a Commercial Game (Custom Animations + Different Control Schemes) | Unity Tutorial - YouTube](https://www.youtube.com/watch?v=u3YdlUW1nx0)
        // the problem is cannot change ui to left right when change from keyboard to controller this help fix that problem

        public static ButtonSelectionManager Instance;
        public GameObject[] AllButton;
        public GameObject LastSelected { get; set; }
        public int LastSelectedIndex { get; set; }
        [Header("for mouse move up or down is -1 or 1")]
        public int _mouseScrollDirection = -1;
        [Tooltip("if disable will use mouse scroll only when button is not selected")]
        public bool _mouseScrollAlway = true;

        void Awake()
        {
            if (Instance == null) Instance = this;
        }
        void OnEnable() => StartCoroutine(SetSelectAfterOneFrame());
        private IEnumerator SetSelectAfterOneFrame()
        {
            yield return null;
            EventSystem.current.SetSelectedGameObject(AllButton[0]);
        }

        private void Update()
        {
            // mouse scroll input
            if (Input.mouseScrollDelta.y > 0) HandleNextCardSelection(1 * _mouseScrollDirection);// mouse up
            if (Input.mouseScrollDelta.y < 0) HandleNextCardSelection(-1 * _mouseScrollDirection);// mouse down
        }

        void HandleNextCardSelection(int addition)
        {
            if (_mouseScrollAlway)
            {
                UpdateCardIndex(addition);
            }
            else
            {
                // this case if for new xbox controller first connect like he said in video on top
                if (EventSystem.current.currentSelectedGameObject == null && LastSelected != null)
                {
                    UpdateCardIndex(addition);
                }
            }
        }

        private void UpdateCardIndex(int addition)
        {
            int newIndex = LastSelectedIndex + addition;
            newIndex = Mathf.Clamp(newIndex, 0, AllButton.Length - 1);
            EventSystem.current.SetSelectedGameObject(AllButton[newIndex]);
        }
    }
}