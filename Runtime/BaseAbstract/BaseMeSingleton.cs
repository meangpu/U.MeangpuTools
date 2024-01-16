using UnityEngine;

namespace Meangpu
{
    public abstract class BaseMeSingleton<T> : MonoBehaviour where T : Component
    {
        // prevent create another singleton when quit if it get called
        private static bool _applicationIsQuitting = false;

        [RuntimeInitializeOnLoadMethod] static void RunOnStart() { Application.quitting += () => _applicationIsQuitting = true; }

        public static T _instance;
        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting) return null;

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

        public void OnDestroy() => _applicationIsQuitting = true;
    }
}