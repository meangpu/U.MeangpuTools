using UnityEditor;
using UnityEngine;

namespace MeangpuTools
{
    public static class MissingScriptTools
    {
        const string _commandName = "MeangpuTools/MissingScpt/";

        [MenuItem(_commandName + "Find")]
        static void FindMissingScript()
        {
            Debug.Log("<color=#37d2e1>=====================</color>");
            Debug.Log("<color=#37d2e1>FIND MISSING script....</color>");
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
            Debug.Log("<color=#37d2e1>=====================</color>");
        }

        [MenuItem(_commandName + "Delete")]
        static void DeleteMissingScript()
        {
            Debug.Log("<color=#37d2e1>=====================</color>");
            Debug.Log("<color=RED>DELETE</color> <color=#37d2e1>SCRIPT....</color>");
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
            Debug.Log("<color=#37d2e1>=====================</color>");
        }
    }
}