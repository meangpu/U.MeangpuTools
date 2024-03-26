using UnityEngine;
using TMPro;

namespace Meangpu.Util
{
    public class GoogleFormFeedbackTMP : BaseGoogleFormFeedback
    {
        [SerializeField] TMP_InputField _InputFieldTmp;

        public override string GetUserInput()
        {
            if (_InputFieldTmp != null) return _InputFieldTmp.text;
            else
            {
                Debug.Log($"<color=#4ec9b0>User Input Field ERROR</color>");
                return "";
            }
        }

        public override void ResetUserInput()
        {
            if (_InputFieldTmp != null) _InputFieldTmp.text = "";
            else Debug.Log($"<color=#4ec9b0>User Input Field ERROR</color>");
        }
    }
}