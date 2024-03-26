using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Meangpu.Util
{
    public class GoogleFormFeedbackLegacy : BaseGoogleFormFeedback
    {
        [SerializeField] InputField _inputField;

        public override string GetUserInput()
        {
            if (_inputField != null) return _inputField.text;
            else
            {
                Debug.Log($"<color=#4ec9b0>User Input Field ERROR</color>");
                return "";
            }
        }

        public override void ResetUserInput()
        {
            if (_inputField != null) _inputField.text = "";
            else Debug.Log($"<color=#4ec9b0>User Input Field ERROR</color>");
        }
    }
}