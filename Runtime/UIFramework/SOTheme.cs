using UnityEngine;

namespace Meangpu.UI
{
    [CreateAssetMenu(fileName = "SOTheme", menuName = "Meangpu/SO_UITheme")]
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
    }
}