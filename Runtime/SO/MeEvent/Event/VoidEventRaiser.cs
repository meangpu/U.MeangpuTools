using VInspector;
using UnityEngine;

namespace Meangpu.SOEvent
{
    public class VoidEventRaiser : MonoBehaviour
    {
        [SerializeField] SOVoidEvent _Data;
        [Button] void RaiseEventData() => _Data?.Raise();
    }
}