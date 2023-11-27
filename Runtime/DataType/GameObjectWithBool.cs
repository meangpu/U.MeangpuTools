using UnityEngine;
namespace Meangpu.Datatype
{
    [System.Serializable]
    public class GameObjectWithBool
    {
        public GameObject gameObjectValue;
        public bool boolValue;

        public GameObjectWithBool(GameObject _gameObjectValue, bool _boolValue)
        {
            boolValue = _boolValue;
            gameObjectValue = _gameObjectValue;
        }

        public void SetActiveByState() => gameObjectValue.SetActive(boolValue);
    }
}