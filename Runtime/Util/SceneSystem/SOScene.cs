using VInspector;
using Meangpu.Util;
using UnityEngine;

namespace Meangpu
{
    [CreateAssetMenu(fileName = "SOScene", menuName = "Meangpu/SOScene")]
    public class SOScene : ScriptableObject
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

        [Header("Scene Loading Method")]
        public bool UseFeelLoadScene;

        [Button]
        public virtual void LoadThisScene()
        {
            if (UseFeelLoadScene)
            {
                ActionSceneLoad.OnLoadSceneFeel?.Invoke(SCENE_ID);
            }
            else
            {
                if (SceneChangeManager.Instance == null)
                {
                    Debug.LogError("Fail to find sceneChange instance");
                    return;
                }
                SceneChangeManager.Instance.LoadScene(this);
            }
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
