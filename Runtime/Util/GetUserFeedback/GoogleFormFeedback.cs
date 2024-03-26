using System.Data;
using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using VInspector;
using UnityEngine.Events;
using Meangpu.SOEvent;

namespace Meangpu.Util
{
    // learn from [User Feedback Survey (updated) - How to Send Unity Data to Google Forms using UnityWebRequest - YouTube](https://www.youtube.com/watch?v=2-tUwIQmBNE)
    public class GoogleFormFeedback : MonoBehaviour
    {
        [Header("form setting")]
        [Tooltip("in google form page do F12 'inspect' web search for 'formResponse'")]
        [SerializeField] string _formFieldURL;
        [Tooltip("in google form page do F12 'inspect' web search for 'entry.' paste this as 'entry.122969'")]
        [SerializeField] string _formSubmitActionURL;

        [Header("User input choose ONE")]
        [SerializeField] TMP_InputField _InputFieldTmp;
        [SerializeField] TMP_Text _textTmp;
        [SerializeField] InputField _inputField;
        [SerializeField] Text _text;
        [Header("Submit Event")]
        [SerializeField] Button _submitBtn;
        [SerializeField] UnityEvent _afterSubmitEvent;

        string GetUserInput()
        {
            if (_InputFieldTmp != null) return _InputFieldTmp.text;
            else if (_textTmp != null) return _textTmp.text;
            else if (_inputField != null) return _inputField.text;
            else if (_text != null) return _text.text;
            else
            {
                Debug.Log($"<color=#4ec9b0>User Input Field ERROR</color>");
                return "";
            }
        }

        void ResetUserInput()
        {
            if (_InputFieldTmp != null) _InputFieldTmp.text = "";
            else if (_textTmp != null) _textTmp.text = "";
            else if (_inputField != null) _inputField.text = "";
            else if (_text != null) _text.text = "";
            else
            {
                Debug.Log($"<color=#4ec9b0>User Input Field ERROR</color>");
            }
        }


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
            if (string.IsNullOrWhiteSpace(_formSubmitActionURL) || string.IsNullOrWhiteSpace(_formFieldURL))
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
            form.AddField(_formFieldURL, text);
            UnityWebRequest submitReq = UnityWebRequest.Post(_formSubmitActionURL, form);
            yield return submitReq.SendWebRequest();
            Debug.Log($"add this '{text}' to google form at '{_formFieldURL}'");
            _afterSubmitEvent?.Invoke();
        }
    }

    enum UserFormInputMethod
    {
        TmpTxt, TMP_InputField, Txt, InputField
    }
}