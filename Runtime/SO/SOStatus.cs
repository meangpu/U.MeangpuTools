using UnityEngine;

namespace Meangpu
{
    [CreateAssetMenu(fileName = "SOStatus", menuName = "Meangpu/SOStatus")]
    public class SOStatus : ScriptableObject
    {
        public Color _enableColor;
        public Color _disableColor;

        public Sprite _enableImg;
        public Sprite _disableImg;
    }
}