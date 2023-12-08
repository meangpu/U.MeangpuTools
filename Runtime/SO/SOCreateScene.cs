#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using EasyButtons;

namespace Meangpu
{
    [CreateAssetMenu(menuName = "Meangpu/SOCreateScene")]
    public class SOCreateScene : SOCreateSOTemplate<SOScene>
    {
        [Button] void SetPathToSOScene() => targetCreatePath = "Assets/Resources/SOScene/";
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

                CreateAsset(nowScene, path);
            }
        }
    }
}
#endif