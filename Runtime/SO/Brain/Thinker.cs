using UnityEngine;

namespace Meangpu.Brain
{
    public class Thinker : MonoBehaviour
    {
        // can make it into List<Brain> then update brain by hp condition
        public Brain Brain;
        private void Update() => Brain.Think(this);
    }
}