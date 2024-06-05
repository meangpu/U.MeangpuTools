using UnityEngine;

namespace Meangpu
{
    public abstract class BaseMeSingleton<T> : MonoBehaviour where T : Component
    {
        public static T _instance { get; private set; }
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        GameObject container = new(typeof(T).Name);
                        _instance = container.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this) Destroy(this);
            else _instance = this as T;
        }
    }
}