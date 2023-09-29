using TMPro;
using UnityEngine;

namespace Meangpu.UI
{
    [CreateAssetMenu(fileName = "SOText", menuName = "Meangpu/UI/Text")]
    public class SOText : ScriptableObject
    {
        public TMP_FontAsset Font;
        public float Size;
        public Vector4 Padding;
    }
}