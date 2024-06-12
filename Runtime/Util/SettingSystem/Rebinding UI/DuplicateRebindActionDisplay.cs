using TMPro;
using UnityEngine;

namespace Meangpu.Util
{
    public class DuplicateRebindActionDisplay : MonoBehaviour
    {
        [SerializeField] TMP_Text _displayErrorText;

        void OnEnable()
        {
            ActionRebindKey.OnDuplicateBinding += DisplayErrorMessage;
            ActionRebindKey.OnSuccessRebind += ClearText;
        }

        void OnDisable()
        {
            ActionRebindKey.OnDuplicateBinding -= DisplayErrorMessage;
            ActionRebindKey.OnSuccessRebind -= ClearText;
        }

        private void Awake() => ClearText();
        private void ClearText() => _displayErrorText.gameObject.SetActive(false);
        private void DisplayErrorMessage(string message)
        {
            _displayErrorText.gameObject.SetActive(true);
            _displayErrorText.SetText(message);
        }
    }
}
