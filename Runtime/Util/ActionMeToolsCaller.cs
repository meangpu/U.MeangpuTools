using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class ActionMeToolsCaller : MonoBehaviour
    {
        [Button]
        public void CallOnPause()
        {
            Debug.Log("call pause");
            ActionMeTools.OnPause?.Invoke();
        }

        [Button]
        public void CallOnUnPause()
        {
            Debug.Log("call unpause");
            ActionMeTools.OnUnPause?.Invoke();
        }
    }
}