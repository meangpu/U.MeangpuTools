using UnityEngine;
using UnityEngine.UI;

namespace Meangpu.UI
{
    public class ScrollView : CustomUIComponent
    {
        [Expandable]
        [SerializeField] SOTheme _theme;
        [SerializeField] Style _styleBG;
        [SerializeField] Style _styleScrollBar;

        [SerializeField] Image _containerBG;

        [SerializeField] Image _scrollBg;
        [SerializeField] Image _scrollHandle;

        public override void Configure()
        {
            _containerBG.color = _theme.GetBGColor(_styleBG);

            _scrollBg.color = _theme.GetBGColor(_styleScrollBar);
            _scrollHandle.color = _theme.GetTextColor(_styleScrollBar);
        }

        public override void Setup()
        {
        }
    }
}