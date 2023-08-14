using UnityEngine;
using UnityEngine.UI;

namespace Meangpu.UI
{
    public class ScrollView : CustomUIComponent
    {
        [Expandable]
        [SerializeField] SOTheme _theme;
        [SerializeField] Style _style;

        [SerializeField] Image _containerBG;

        [SerializeField] Image _scrollBg;
        [SerializeField] Image _scrollHandle;

        public override void Configure()
        {
            _containerBG.color = _theme.GetBGColor(_style);
            _scrollBg.color = _theme.GetBGColor(_style);
            _scrollHandle.color = _theme.GetTextColor(_style);
        }

        public override void Setup()
        {
        }
    }
}