using UnityEngine;
using TMPro;

namespace Meangpu.Util
{
    public class SetTmpText : MonoBehaviour
    {
        [SerializeField] TMP_Text _targetText;
        public TMP_Text TargetText => _targetText;

        public void SetText(string word) => _targetText.SetText(word);
        public void SetTextColor(Color newColor) => _targetText.color = newColor;
    }
}