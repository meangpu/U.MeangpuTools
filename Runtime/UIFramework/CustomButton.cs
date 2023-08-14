using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using EasyButtons;

namespace Meangpu.UI
{
    public class CustomButton : CustomUIComponent
    {
        [Expandable]
        [SerializeField] SOText _textData;

        [SerializeField] Style _style;
        [SerializeField] UnityEvent _onClick;

        Button _button;
        TextMeshProUGUI _buttonText;

        public override void Setup()
        {
            _button = GetComponentInChildren<Button>();
            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public override void Configure()
        {
            ColorBlock cb = _button.colors;
            cb.normalColor = _textData.Theme.GetBGColor(_style);
            _button.colors = cb;

            _buttonText.color = _textData.Theme.GetTextColor(_style);
            _buttonText.font = _textData.Font;
            _buttonText.fontSize = _textData.Size;
            _buttonText.margin = _textData.Padding;
        }

        [Button]
        public void OnClick() => _onClick?.Invoke();
    }
}