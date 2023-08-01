using UnityEngine;
using System.Collections.Generic;

namespace Meangpu.Util.GameState
{
    [System.Serializable]
    public struct ObjectWithGameState
    {
        public SOGameState State;
        public List<GameObject> ActiveObj;
        public List<GameObject> DisableObj;

        public ObjectWithGameState(SOGameState state, List<GameObject> activeObj, List<GameObject> disableObj)
        {
            State = state;
            ActiveObj = activeObj;
            DisableObj = disableObj;
        }
    }
}