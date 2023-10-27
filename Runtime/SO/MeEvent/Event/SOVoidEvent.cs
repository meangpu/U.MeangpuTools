using EasyButtons;
using UnityEngine;

namespace Meangpu.SOEvent
{
    [CreateAssetMenu(fileName = "VoidEvent", menuName = "Meangpu/Event/SOVoidEvent")]
    public class SOVoidEvent : SOBaseGameEvent<Void>
    {
        [Button]
        public void Raise() => Raise(new Void());
    }
}