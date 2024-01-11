using Meangpu.Interface;
using Meangpu.Util;
using UnityEngine;

namespace Meangpu
{
    public abstract class BasePauseableObject : MonoBehaviour
    {
        [SerializeField] bool _isThisCanGetPause = true;
        [ReadOnly] public bool IsPausing;

        void OnEnable()
        {
            ActionMeTools.OnPause += DoPause;
            ActionMeTools.OnUnPause += DoUnPause;
        }
        void OnDisable()
        {
            ActionMeTools.OnPause -= DoPause;
            ActionMeTools.OnUnPause -= DoUnPause;
        }

        void DoPause()
        {
            if (!_isThisCanGetPause) return;
            IsPausing = true;
            this.enabled = false;
            AfterPause();
        }

        void DoUnPause()
        {
            if (!_isThisCanGetPause) return;
            IsPausing = false;
            this.enabled = true;
            AfterUnPause();
        }
        protected virtual void AfterPause() { }
        protected virtual void AfterUnPause() { }
    }
}