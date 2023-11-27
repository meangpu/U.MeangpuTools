using UnityEngine;

namespace Meangpu.Util
{
    public static class GetOrCreate
    {
        public static T GetCreateComponent<T>(Transform _transform) where T : Component
        {
            if (_transform.GetComponent<T>()) return _transform.GetComponent<T>();
            return _transform.gameObject.AddComponent<T>();
        }

        public static T GetCreateComponent<T>(GameObject gameObject) where T : Component
        {
            if (gameObject.GetComponent<T>()) return gameObject.GetComponent<T>();
            return gameObject.AddComponent<T>();
        }
        // ======= example =======
        // public BoxCollider _box;
        // _box = GetOrCreate.GetCreateComponent<BoxCollider>(transform);
    }
}