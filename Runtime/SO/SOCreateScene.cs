#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using EasyButtons;

namespace Meangpu
{
    [CreateAssetMenu(menuName = "Meangpu/SOCreateScene")]
    public class SOCreateScene : SOCreateSOTemplate
    {
        [Button] void SetPathToSOScene() => path = "Assets/Resources/SOScene/";
        [SerializeField] SceneAsset[] _scene;

        public override void CreateSO_ObjectList()
        {
            for (var i = 0; i < _scene.Length; i++)
            {
                string nowSceneName = _scene[i].name;
                SOScene nowScene = ScriptableObject.CreateInstance<SOScene>();

                nowScene.SceneData = _scene[i];

                string path = GetFullFilePath(nowSceneName);
                nowScene.name = nowSceneName;

                AssetDatabase.CreateAsset(nowScene, path);
                AssetDatabase.SaveAssets();
                Debug.Log($"create {nowScene} obj at {path}!");
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = nowScene;
            }
        }
    }
}
#endif