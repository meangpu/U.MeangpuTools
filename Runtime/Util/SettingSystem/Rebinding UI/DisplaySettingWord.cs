using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using VInspector;

namespace Meangpu.Util
{
    public class DisplaySettingWord : MonoBehaviour
    {
        [SerializeField] InputActionReference _ref;
        [Header("text")]
        [SerializeField] TMP_Text _actionWord;
        [SerializeField] TMP_Text _keyBindWord;

        private void Start() => SetWordToRef();

        private void OnValidate()
        {
            if (_actionWord == null || _keyBindWord == null || _ref) return;
            SetWordToRef();
        }


        [Button]
        void SetWordToRef()
        {
            _actionWord.text = _ref.action.name;
            _keyBindWord.text = _ref.action.bindings[0].ToDisplayString();
        }
    }
}

