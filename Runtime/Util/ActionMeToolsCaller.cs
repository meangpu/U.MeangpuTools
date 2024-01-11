using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class ActionMeToolsCaller : MonoBehaviour
    {
        [Button]
        public void Pause()
        {
            Debug.Log("call pause");
            ActionMeTools.OnPause?.Invoke();
        }

        [Button]
        public void UnPause()
        {
            Debug.Log("call unpause");
            ActionMeTools.OnUnPause?.Invoke();
        }
    }
}