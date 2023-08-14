using UnityEngine;
using UnityEngine.UI;

namespace Meangpu.UI
{
    public class View : CustomUIComponent
    {
        [Expandable]
        [SerializeField] SOview _data;
        [SerializeField] GameObject _containerTop;
        [SerializeField] GameObject _containerMid;
        [SerializeField] GameObject _containerBot;
        Image _imageTop;
        Image _imageMid;
        Image _imageBot;
        VerticalLayoutGroup _verticalLayoutGroup;

        public override void Setup()
        {
            _verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
            _imageTop = _containerTop.GetComponent<Image>();
            _imageMid = _containerMid.GetComponent<Image>();
            _imageBot = _containerBot.GetComponent<Image>();
        }

        public override void Configure()
        {
            _verticalLayoutGroup.padding = _data.Padding;
            _verticalLayoutGroup.spacing = _data.Spacing;
            _imageTop.color = _data.Theme.PrimaryBG;
            _imageMid.color = _data.Theme.SecondaryBG;
            _imageBot.color = _data.Theme.TertiaryBG;
        }
    }
}
