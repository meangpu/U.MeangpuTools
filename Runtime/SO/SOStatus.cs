using UnityEngine;

namespace Meangpu
{
    [CreateAssetMenu(fileName = "SOStatus", menuName = "Meangpu/SOStatus")]
    public class SOStatus : ScriptableObject
    {
        public Color EnableColor;
        public Color DisableColor;

        public Sprite EnableImg;
        public Sprite DisableImg;
    }
}