using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using VInspector;
using UnityEngine.Events;

namespace Meangpu.Util
{
    // learn from [User Feedback Survey (updated) - How to Send Unity Data to Google Forms using UnityWebRequest - YouTube](https://www.youtube.com/watch?v=2-tUwIQmBNE)
    public abstract class BaseGoogleFormFeedback : MonoBehaviour
    {
        [Header("Copy This")]
        public string CopyMe_formEntry = "entry.";
        public string CopyMe_formActionResponse = "formResponse";

        [Header("Form setting")]
        [Tooltip("in google form page do F12 'inspect' web search for 'entry.' paste this as 'entry.122969'")]
        [SerializeField] string _formEntryFieldID;
        [Tooltip("in google form page do F12 'inspect' web search for 'formResponse'")]
        [SerializeField] string _formSubmitActionURL;

        [Header("Submit Event")]
        [SerializeField] Button _submitBtn;
        [SerializeField] UnityEvent _afterSubmitEvent;

        public abstract string GetUserInput();
        public abstract void ResetUserInput();

        [Button]
        public void SubmitUserFeedbackToForm()
        {
            StopAllCoroutines();
            SetSubmitBtnInteractable(false);
            StartCoroutine(PostUserFeedback(GetUserInput()));
        }

        [Button]
        public void MakeFormCanSubmitAgain()
        {
            SetSubmitBtnInteractable(true);
            ResetUserInput();
        }

        public void SetSubmitBtnInteractable(bool canInteract)
        {
            if (_submitBtn == null)
            {
                Debug.Log($"<color=#4ec9b0>No submit button!!</color>");
                return;
            }
            _submitBtn.interactable = canInteract;
        }

        IEnumerator PostUserFeedback(string text)
        {
            if (string.IsNullOrWhiteSpace(_formSubmitActionURL) || string.IsNullOrWhiteSpace(_formEntryFieldID))
            {
                Debug.Log($"<color=red>User form URL is null!</color>");
                yield break;
            }
            if (string.IsNullOrEmpty(text))
            {
                Debug.Log($"<color=red>User text is empty!</color>");
                yield break;
            }

            WWWForm form = new();
            form.AddField(_formEntryFieldID, text);
            UnityWebRequest submitReq = UnityWebRequest.Post(_formSubmitActionURL, form);
            yield return submitReq.SendWebRequest();
            Debug.Log($"add this '{text}' to google form at '{_formEntryFieldID}'");

            _afterSubmitEvent?.Invoke();
        }
    }
}