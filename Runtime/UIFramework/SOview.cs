using UnityEngine;

namespace Meangpu.UI
{
    [CreateAssetMenu(fileName = "SOview", menuName = "Meangpu/UI/View")]
    public class SOview : ScriptableObject
    {
        public RectOffset Padding;
        public float Spacing;
    }
}