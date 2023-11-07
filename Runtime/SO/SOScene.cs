using UnityEngine;

namespace Meangpu
{
    [CreateAssetMenu(fileName = "SOScene", menuName = "Meangpu/SOScene")]
    public class SOScene : ScriptableObject
    {
#if UNITY_EDITOR
        public UnityEditor.SceneAsset SceneData;
#endif
        public string SceneName;
        [TextArea]
        public string SceneDesShort;
        [TextArea]
        public string SceneDes;
        public Sprite Icon;
        public Sprite ScenePreviewImage;
        public bool IsDone;
    }
}
