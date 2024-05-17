using VInspector;
using Meangpu.Util;
using UnityEngine;

namespace Meangpu
{
    [CreateAssetMenu(fileName = "SOScene", menuName = "Meangpu/SOScene")]
    public class SOScene : ScriptableObject
    {
#if UNITY_EDITOR
        public UnityEditor.SceneAsset SceneData;
        private void OnValidate()
        {
            if (SceneData == null) return;
            SCENE_ID = SceneData.name;
            UnityEditor.EditorUtility.SetDirty(this);
        }

        [Button]
        public void RenameThis()
        {
            if (SceneData == null)
            {
                Debug.LogError("Scene data is null");
                return;
            }
            string assetPath = UnityEditor.AssetDatabase.GetAssetPath(this.GetInstanceID());
            UnityEditor.AssetDatabase.RenameAsset(assetPath, SceneData.name);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.EditorUtility.SetDirty(this);
        }
#endif
        [ReadOnly]
        public string SCENE_ID;

        public string SceneName;
        [TextArea]
        public string SceneDescriptionShort;
        [TextArea]
        public string SceneDescription;
        public Sprite Icon;
        public Sprite ScenePreviewImage;
        public bool IsDone;

        [Button]
        public virtual void LoadThisScene()
        {
            if (SceneChangeManager.Instance == null)
            {
                Debug.LogError("Fail to find sceneChange instance");
                return;
            }
            SceneChangeManager.Instance.LoadScene(this);
        }

        [Button]
        public virtual void LoadThisSceneAdditive()
        {
            if (SceneChangeManager.Instance == null)
            {
                Debug.LogError("Fail to find sceneChange instance");
                return;
            }
            SceneChangeManager.Instance.LoadSceneAdditive(this);
        }

        [Button]
        public virtual void UnloadThisScene()
        {
            if (SceneChangeManager.Instance == null)
            {
                Debug.LogError("Fail to find sceneChange instance");
                return;
            }
            SceneChangeManager.Instance.UnloadScene(this);
        }
    }
}
