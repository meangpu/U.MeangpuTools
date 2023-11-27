using UnityEngine;
namespace Meangpu.Datatype
{
    [System.Serializable]
    public struct GameObjectWithBool
    {
        public GameObject gameObjectValue;
        public bool boolValue;

        public GameObjectWithBool(GameObject _gameObjectValue, bool _boolValue)
        {
            boolValue = _boolValue;
            gameObjectValue = _gameObjectValue;
        }
    }
}