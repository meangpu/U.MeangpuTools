#if UNITY_EDITOR
using EasyButtons;
using UnityEngine;
using UnityEditor;

namespace Meangpu
{
    public abstract class SOCreateSOTemplate<T> : ScriptableObject where T : Object
    {
        // example: $"Assets/Resources/SO/OperationData/{i + 1}.asset";
        public string targetCreatePath = "Assets/Resources/";
        const string extension = ".asset";

        public string GetFullFilePath(string fileName) => $"{targetCreatePath}{fileName}{extension}";

        [Button] public abstract void CreateSO_ObjectList();

        public virtual void CreateAsset(T nowSO, string path)
        {
            AssetDatabase.CreateAsset(nowSO, path);
            AssetDatabase.SaveAssets();
            Debug.Log($"create {nowSO} obj at {path}!");
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = nowSO;
        }
    }
}
#endif