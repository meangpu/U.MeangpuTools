using System.Linq;
using UnityEngine;
using VInspector;
using System.Collections.Generic;
using System;

namespace Meangpu.Util.GameState
{
    public class SetChildActiveIfGameState : MonoBehaviour
    {
        [SerializeField] List<SOGameState> _gameStateToActiveChild = new();
        void OnEnable() => ActionGameState.OnGameStateChange += SetObjectByState;
        void OnDisable() => ActionGameState.OnGameStateChange -= SetObjectByState;

        private void SetObjectByState(SOGameState state)
        {
            if (_gameStateToActiveChild.Contains(state)) SetChildActive(true);
            else SetChildActive(false);
        }

        void SetChildActive(bool status)
        {
            for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(status);
        }
    }
}
