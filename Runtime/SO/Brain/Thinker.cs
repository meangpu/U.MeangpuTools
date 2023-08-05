using UnityEngine;

namespace Meangpu.Brain
{
    public class Thinker : MonoBehaviour
    {
        public Brain Brain;
        private void Update() => Brain.Think(this);
    }
}