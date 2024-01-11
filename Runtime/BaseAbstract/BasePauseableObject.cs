using Meangpu.Util;
using UnityEngine;

namespace Meangpu
{
    public abstract class BasePauseableObject : MonoBehaviour
    {
        [SerializeField] bool _isThisCanGetPause = true;
        [ReadOnly] public bool IsPausing;

        void Awake()
        {
            ActionMeTools.OnPause += DoPause;
            ActionMeTools.OnUnPause += DoUnPause;
        }

        void OnDestroy()
        {
            ActionMeTools.OnPause -= DoPause;
            ActionMeTools.OnUnPause -= DoUnPause;
        }

        void DoPause()
        {
            if (!_isThisCanGetPause) return;
            IsPausing = true;
            enabled = false;
            AfterPause();
        }

        void DoUnPause()
        {
            if (!_isThisCanGetPause) return;
            IsPausing = false;
            enabled = true;
            AfterUnPause();
        }
        protected virtual void AfterPause() { }
        protected virtual void AfterUnPause() { }
    }
}