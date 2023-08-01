using System.Linq;
using UnityEngine;
using EasyButtons;
using System.Collections.Generic;

namespace Meangpu.Util.GameState
{
    public class GameStateManager : MonoBehaviour
    {
        public SOGameState NowState;

        [SerializeField] List<ObjectWithGameState> _objectWithStateList = new();
        List<SOGameState> _allStateList = new();

        void OnEnable() => ActionGameState.OnGameStateChange += SetObjectByState;
        void OnDisable() => ActionGameState.OnGameStateChange -= SetObjectByState;

        [Button]
        void GenerateStateList()
        {
            _allStateList.Clear();
            _allStateList = Resources.LoadAll<SOGameState>("GameState").ToList();
            _objectWithStateList.Clear();
            foreach (SOGameState state in _allStateList) _objectWithStateList.Add(new(state, null, null));
        }

        private void SetObjectByState(SOGameState newState)
        {
            foreach (ObjectWithGameState stateData in _objectWithStateList)
            {
                if (!stateData.State.Equals(newState)) continue;
                if (stateData.DisableObj != null)
                    foreach (GameObject toDisableObj in stateData.DisableObj) toDisableObj.SetActive(false);
                if (stateData.ActiveObj != null)
                    foreach (GameObject toActiveObj in stateData.ActiveObj) toActiveObj.SetActive(true);
            }
        }

        [Button]
        public void ChangeGameState(SOGameState newState)
        {
            NowState = newState;
            ActionGameState.OnGameStateChange?.Invoke(NowState);
        }
    }
}