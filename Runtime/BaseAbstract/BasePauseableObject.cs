using Meangpu.Interface;
using Meangpu.Util;
using UnityEngine;

namespace Meangpu
{
    public abstract class BasePauseableObject : MonoBehaviour, ICanPause
    {
        [SerializeField] bool _isThisCanGetPause = true;
        [ReadOnly] public bool IsPausing;

        void OnEnable() => ActionMeTools.OnPause += DoPause;
        void OnDisable() => ActionMeTools.OnUnPause -= DoUnPause;

        public void DoPause()
        {
            IsPausing = true;
            enabled = false;
            AfterPause();
        }

        public void DoUnPause()
        {
            IsPausing = false;
            enabled = true;
            AfterUnPause();
        }
        protected virtual void AfterPause() { }
        protected virtual void AfterUnPause() { }
    }
}