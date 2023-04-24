using UnityEngine;

public class GetOrCreate : MonoBehaviour
{
    public static T GetCreateComponent<T>(Transform _transform) where T : Component
    {
        if (_transform.GetComponent<T>()) return _transform.GetComponent<T>();
        return _transform.gameObject.AddComponent<T>();
    }
    // ======= example =======
    // public BoxCollider _box;
    // _box = GetOrCreate.GetCreateComponent<BoxCollider>(transform);
}
