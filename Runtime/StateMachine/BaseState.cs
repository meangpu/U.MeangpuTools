using UnityEngine;
using System;

namespace Meangpu.StateMachine
{
    public abstract class BaseState<EState> where EState : Enum
    {
        protected BaseState(EState key) { StateKey = key; }
        public EState StateKey { get; private set; }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract EState GetNextState();
        public abstract void OnTriggerEnter(Collision other);
        public abstract void OnTriggerStay(Collision other);
        public abstract void OnTriggerExit(Collision other);
    }
}