using VInspector;
using UnityEngine;

namespace Meangpu
{
    public abstract class BaseSOScene : ScriptableObject
    {
#if UNITY_EDITOR
        public SceneReference SceneRefData;

        private void OnValidate()
        {
            if (SceneRefData == null) return;
            if (SceneRefData.GetSceneName == null) return;
            SCENE_ID = SceneRefData.GetSceneName;
            UnityEditor.EditorUtility.SetDirty(this);
        }

        [Button]
        public void RenameThisSOTOSceneName()
        {
            if (SceneRefData == null)
            {
                Debug.LogError("Scene data is null");
                return;
            }
            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this.GetInstanceID());
            UnityEditor.AssetDatabase.RenameAsset(assetPath, SceneRefData.GetSceneName);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif

        [ReadOnly] public string SCENE_ID;

        public string SceneNameDisplay;
        [TextArea]
        public string SceneDescriptionShort;
        [TextArea]
        public string SceneDescription;
        public Sprite Icon;
        public Sprite ScenePreviewImage;
        public bool IsDone;
        public bool IsUseFeelLoadScene;
    }
}
