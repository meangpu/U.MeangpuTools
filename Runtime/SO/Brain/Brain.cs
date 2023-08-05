using UnityEngine;

namespace Meangpu.Brain
{
    public abstract class Brain : ScriptableObject
    {
        public abstract void Think(ICanThink thinker);
    }
}
