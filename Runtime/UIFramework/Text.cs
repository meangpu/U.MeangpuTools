using EasyButtons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meangpu.UI
{
    public class Text : MonoBehaviour
    {
        [Expandable]
        [SerializeField] SOText _textData;
        [SerializeField] Style _textStyle;

        private TextMeshProUGUI _textMeshProUGUI;

        private void Awake() => Init();

        [Button]
        private void Init()
        {
            Setup();
            Configure();
        }

        private void Setup()
        {
            _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Configure()
        {
            _textMeshProUGUI.color = _textData.Theme.GetTextColor(_textStyle);
            _textMeshProUGUI.font = _textData.Font;
            _textMeshProUGUI.fontSize = _textData.Size;
            _textMeshProUGUI.margin = _textData.Padding;
        }
        private void OnValidate() => Init();
    }
}
