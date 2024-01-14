using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// from CodeMonkey [How to DETECT ERRORS in your game! - YouTube](https://www.youtube.com/watch?v=cos9FbXzdWI)
namespace Meangpu.Util
{
    public class ErrorDetectorUI : MonoBehaviour
    {
        [SerializeField] TMP_Text _errorText;
        [SerializeField] Button _closeButton;
        [SerializeField] Button _copyToClipboardButton;
        [SerializeField] bool _ignoreSameError = true;
        List<string> _ignoreErrorStringList = new();

        void OnEnable()
        {
            Application.logMessageReceived += Application_logMessageReceived;
            Hide();
        }

        void OnDisable() => Application.logMessageReceived -= Application_logMessageReceived;

        private void Awake()
        {
            _closeButton.onClick.AddListener(() => gameObject.SetActive(false));
            _copyToClipboardButton.onClick.AddListener(() =>
            {
                try
                {
                    GUIUtility.systemCopyBuffer = _errorText.text;
                }
                catch (Exception e)
                {
                    Debug.LogException(new Exception("Fail to copy to clipboard \n" + e));
                }
            });
        }

        void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (type == LogType.Error || type == LogType.Exception)
            {
                string ErrorText = $"Error:{condition}\n{stackTrace}";
                if (_ignoreSameError && _ignoreErrorStringList.Contains(ErrorText)) return;

                _ignoreErrorStringList.Add(ErrorText);
                _errorText.SetText(ErrorText);
                Show();
            }
        }

        void Hide() => gameObject.SetActive(false);
        void Show() => gameObject.SetActive(true);
    }
}