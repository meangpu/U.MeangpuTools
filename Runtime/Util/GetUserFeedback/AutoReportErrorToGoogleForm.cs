using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Meangpu.Util
{
    public class AutoReportErrorToGoogleForm : MonoBehaviour
    {
        [Header("Copy This")]
        public string CopyMe_formFieldURL = "formResponse";
        public string CopyMe_formSubmitActionURL = "entry.";

        [Header("Form setting")]
        [Tooltip("in google form page do F12 'inspect' web search for 'entry.' paste this as 'entry.122969'")]
        [SerializeField] string _formFieldURL;
        [Tooltip("in google form page do F12 'inspect' web search for 'formResponse'")]
        [SerializeField] string _formSubmitActionURL;

        [SerializeField] UnityEvent _afterErrorSubmitEvent;
        [SerializeField] UnityEvent _OnFoundErrorEvent;

        [SerializeField] bool _ignoreSameError = true;
        [SerializeField] List<string> _ignoreErrorStringList = new();

#if UNITY_EDITOR
        [SerializeField] bool _disableSendFormInUnityEditor = true;
#endif

        [SerializeField] bool _doAutoSentToForm = true;

        string _errorText;
        public string LastErrorText => _errorText;

        void OnEnable() => Application.logMessageReceived += Application_logMessageReceived;
        void OnDisable() => Application.logMessageReceived -= Application_logMessageReceived;

        void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {

#if UNITY_EDITOR
            if (_disableSendFormInUnityEditor)
            {
                return;
            }
#endif
            if (type == LogType.Error || type == LogType.Exception)
            {
                _errorText = $"AutoReportError\n\nDeviceData: {DeviceData.GetDeviceData()}\n\nErrorAtScene: {SceneManager.GetActiveScene().name}\n\n{condition}\n\n{stackTrace}";
                if (_ignoreSameError && _ignoreErrorStringList.Contains(_errorText)) return;
                _ignoreErrorStringList.Add(_errorText);

                _OnFoundErrorEvent?.Invoke();

                if (_doAutoSentToForm)
                {
                    SubmitUserFeedbackToForm(_errorText);
                }
            }
        }

        public void UploadToFormWithLastErrorText() => SubmitUserFeedbackToForm(_errorText);

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
            Debug.Log($"AutoReport '{text}' Is Sent To '{_formFieldURL}'");

            _afterErrorSubmitEvent?.Invoke();
        }
    }
}