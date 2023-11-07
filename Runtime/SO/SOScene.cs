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
            if (SceneData != null)
            {
                SCENE_ID = SceneData.name;
            }
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
    }
}
