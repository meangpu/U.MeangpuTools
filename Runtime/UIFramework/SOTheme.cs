using UnityEngine;

namespace Meangpu.UI
{
    [CreateAssetMenu(fileName = "SOTheme", menuName = "Meangpu/UI/Theme")]
    public class SOTheme : ScriptableObject
    {
        [Header("Primary")]
        public Color PrimaryBG;
        public Color PrimaryText;
        [Header("Secondary")]
        public Color SecondaryBG;
        public Color SecondaryText;
        [Header("Tertiary")]
        public Color TertiaryBG;
        public Color TertiaryText;
        [Header("Other")]
        public Color DisableColor;

        public Color GetTextColor(Style style)
        {
            return style switch
            {
                Style.Primary => PrimaryText,
                Style.Secondary => SecondaryText,
                Style.Tertiary => TertiaryText,
                _ => DisableColor,
            };
        }

        public Color GetBGColor(Style style)
        {
            return style switch
            {
                Style.Primary => PrimaryBG,
                Style.Secondary => SecondaryBG,
                Style.Tertiary => TertiaryBG,
                _ => DisableColor,
            };
        }
    }
}