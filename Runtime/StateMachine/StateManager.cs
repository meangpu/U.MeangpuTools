using UnityEngine;
using System;
using System.Collections.Generic;

namespace Meangpu.StateMachine
{
    public abstract class StateManager<EState> : MonoBehaviour where EState : Enum
    {
        protected Dictionary<EState, BaseState<EState>> States = new();
        protected BaseState<EState> CurrentState;
        protected bool IsTransitioningState;

        void Awake()
        { }
        void Start() => CurrentState.EnterState();
        void Update()
        {
            EState nextStateKey = CurrentState.GetNextState();
            if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey)) CurrentState.UpdateState();
            else TransitionToState(nextStateKey);
        }

        private void TransitionToState(EState nextStateKey)
        {
            IsTransitioningState = true;
            CurrentState.ExitState();
            CurrentState = States[nextStateKey];
            CurrentState.EnterState();
            IsTransitioningState = false;
        }

        void OnTriggerEnter(Collision other) => CurrentState.OnTriggerEnter(other);
        void OnTriggerStay(Collision other) => CurrentState.OnTriggerStay(other);
        void OnTriggerExit(Collision other) => CurrentState.OnTriggerExit(other);
    }
}