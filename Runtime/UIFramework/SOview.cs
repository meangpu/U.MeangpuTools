using UnityEngine;

namespace Meangpu.UI
{
    [CreateAssetMenu(fileName = "SOview", menuName = "Meangpu/SO_UIView")]
    public class SOview : ScriptableObject
    {
        [Expandable]
        public SOTheme Theme;
        public RectOffset Padding;
        public float Spacing;
    }
}