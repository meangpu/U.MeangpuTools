#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using EasyButtons;

namespace Meangpu
{
    [CreateAssetMenu(menuName = "Meangpu/SOCreateScene")]
    public class SOCreateScene : SOCreateSOTemplate<SOScene>
    {
        [Button]
        void SetPathToSOScene()
        {
            targetCreatePath = "Assets/Resources/SOScene/";
            EditorUtility.SetDirty(this);
            AssetDatabase.Refresh();
        }

        [SerializeField] SceneAsset[] _scene;

        public override void CreateSO_ObjectList()
        {
            for (var i = 0; i < _scene.Length; i++)
            {
                SOScene nowScene = CreateInstance<SOScene>();

                nowScene.SceneData = _scene[i];
                nowScene.name = _scene[i].name;
                nowScene.SCENE_ID = _scene[i].name;

                string path = GetFullFilePath(_scene[i].name);
                CreateAsset(nowScene, path);
            }
        }
    }
}
#endif