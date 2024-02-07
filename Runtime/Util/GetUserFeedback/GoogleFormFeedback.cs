using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using VInspector;

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
        [SerializeField] TMP_Text _formTextTmp;
        [SerializeField] InputField _inputField;
        [SerializeField] Text _formText;

        string GetUserInput()
        {
            if (_InputFieldTmp != null) return _InputFieldTmp.text;
            else if (_formTextTmp != null) return _formTextTmp.text;
            else if (_inputField != null) return _inputField.text;
            else if (_formText != null) return _formText.text;
            else
            {
                Debug.Log($"<color=#4ec9b0>User Input Field ERROR</color>");
                return "";
            }
        }

        [Button]
        public void SubmitUserFeedbackToForm()
        {
            StartCoroutine(PostUserFeedback(GetUserInput()));
        }

        IEnumerator PostUserFeedback(string text)
        {
            if (string.IsNullOrWhiteSpace(_formSubmitActionURL) || string.IsNullOrWhiteSpace(_formFieldURL))
            {
                Debug.Log($"<color=red>User form URL is null!</color>");
                yield return new();
            }
            if (string.IsNullOrEmpty(text))
            {
                Debug.Log($"<color=red>User text is empty!</color>");
                yield return new();
            }

            WWWForm form = new();
            form.AddField(_formFieldURL, text);
            UnityWebRequest submitReq = UnityWebRequest.Post(_formSubmitActionURL, form);
            yield return submitReq.SendWebRequest();
            Debug.Log("Submit Request: " + submitReq.downloadHandler.text);
        }
    }

    enum UserFormInputMethod
    {
        TmpTxt, TMP_InputField, Txt, InputField
    }
}