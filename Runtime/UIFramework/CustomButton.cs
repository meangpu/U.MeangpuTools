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

        Image _image;
        TextMeshProUGUI _buttonText;

        public override void Setup()
        {
            _image = GetComponentInChildren<Image>();
            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }

        public override void Configure()
        {
            SOTheme theme = GetMainTheme();
            if (theme == null) return;
            _image.color = theme.GetBGColor(_style);

            _buttonText.color = theme.GetTextColor(_style);
            _buttonText.font = _textData.Font;
            _buttonText.fontSize = _textData.Size;
            _buttonText.margin = _textData.Padding;
        }

        [Button]
        public void OnClick() => _onClick?.Invoke();
    }
}