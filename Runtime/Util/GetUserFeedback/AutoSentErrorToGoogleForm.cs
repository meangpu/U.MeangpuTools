using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using VInspector;
using UnityEngine.Events;
using System.Collections.Generic;

namespace Meangpu.Util
{
    public class AutoSentErrorToGoogleForm : MonoBehaviour
    {
        [Header("Copy This")]
        [SerializeField] string CopyMe_formFieldURL = "formResponse";
        [SerializeField] string CopyMe_formSubmitActionURL = "entry.";

        [Header("Form setting")]
        [Tooltip("in google form page do F12 'inspect' web search for 'entry.' paste this as 'entry.122969'")]
        [SerializeField] string _formFieldURL;
        [Tooltip("in google form page do F12 'inspect' web search for 'formResponse'")]
        [SerializeField] string _formSubmitActionURL;

        [SerializeField] UnityEvent _afterErrorSubmitEvent;

        [SerializeField] bool _ignoreSameError = true;
        [SerializeField] List<string> _ignoreErrorStringList = new();

        void OnEnable() => Application.logMessageReceived += Application_logMessageReceived;
        void OnDisable() => Application.logMessageReceived -= Application_logMessageReceived;

        void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception)
            {
                string ErrorText = $"AutoReportError:{condition}\n{stackTrace}";
                if (_ignoreSameError && _ignoreErrorStringList.Contains(ErrorText)) return;
                _ignoreErrorStringList.Add(ErrorText);

                SubmitUserFeedbackToForm(ErrorText);
            }
        }

        private void SubmitUserFeedbackToForm(string wordToSubmit)
        {
            StopAllCoroutines();
            StartCoroutine(PostUserFeedback(wordToSubmit));
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

            _afterErrorSubmitEvent?.Invoke();
        }
    }
}