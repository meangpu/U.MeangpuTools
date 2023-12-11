using VInspector;
using UnityEngine;

namespace Meangpu.SOEvent
{
    [CreateAssetMenu(fileName = "UE_VoidEvent", menuName = "Meangpu/SOEvent/Void")]
    public class SOVoidEvent : SOBaseGameEvent<Void>
    {
        [Button]
        public void Raise() => Raise(new Void());
    }
}