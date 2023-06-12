#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace MeangpuTools
{
    public static class MissingScriptTools
    {
        const string _commandName = "MeangpuTools/MissingScpt/";

        [MenuItem(_commandName + "Find")]
        static void FindMissingScript()
        {
            foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
            {
                foreach (Component component in gameObject.GetComponentsInChildren<Component>())
                {
                    if (component == null)
                    {
                        Debug.Log($"<color=red>MissingScpt: {gameObject.name}</color>", gameObject);
                        break;
                    }
                }
            }
        }

        [MenuItem(_commandName + "Delete")]
        static void DeleteMissingScript()
        {
            foreach (GameObject gameObject in GameObject.FindObjectsOfType<GameObject>(true))
            {
                foreach (Component component in gameObject.GetComponentsInChildren<Component>())
                {
                    if (component == null)
                    {
                        GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
                        Debug.Log($"<color=red>DELETE:{gameObject.name}</color>", gameObject);
                        break;
                    }
                }
            }
        }
    }
}