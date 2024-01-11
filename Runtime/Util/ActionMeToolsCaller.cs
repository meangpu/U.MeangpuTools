using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class ActionMeToolsCaller : MonoBehaviour
    {
        [Button] public void CallOnPause() => ActionMeTools.OnPause?.Invoke();
        [Button] public void CallOnUnPause() => ActionMeTools.OnUnPause?.Invoke();
    }
}