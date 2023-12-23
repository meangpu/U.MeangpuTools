using UnityEngine;

namespace Meangpu
{
    public class MeSingleton<T> : MonoBehaviour where T : Component, new()
    {
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
                        GameObject container = new();
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