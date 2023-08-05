using UnityEngine;
using System.Collections.Generic;

namespace Meangpu.Brain
{
    public class ThinkerExample : MonoBehaviour, ICanThink
    {
        // can make it into List<Brain> then update brain by hp condition
        // public Brain Brain;
        // private void Update() => Brain.Think(this);
        [SerializeField] List<Brain> _BrainList;
        private void Update() => _BrainList[0].Think(this);
    }
}
