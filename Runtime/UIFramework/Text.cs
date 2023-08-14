using TMPro;
using UnityEngine;

namespace Meangpu.UI
{
    public class Text : CustomUIComponent
    {
        [Expandable]
        [SerializeField] SOText _textData;
        [SerializeField] Style _textStyle;

        private TextMeshProUGUI _textMeshProUGUI;

        public override void Setup() => _textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();

        public override void Configure()
        {
            _textMeshProUGUI.color = _textData.Theme.GetTextColor(_textStyle);
            _textMeshProUGUI.font = _textData.Font;
            _textMeshProUGUI.fontSize = _textData.Size;
            _textMeshProUGUI.margin = _textData.Padding;
        }
    }
}
