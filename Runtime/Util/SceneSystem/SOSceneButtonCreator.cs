using Meangpu.Util;
using UnityEngine;
using VInspector;

namespace Meangpu
{
    public class SOSceneButtonCreator : MonoBehaviour
    {
        [SerializeField] Transform _parentTrans;
        [SerializeField] SetTmpText _scenePrefab;
        [SerializeField] SOScene[] _allScene;

        [Button] public void LoadAllScene() => _allScene = Resources.LoadAll<SOScene>("SOScene");

#if UNITY_EDITOR
        [Button]
        public void EditorSpawnAllButtonScene()
        {
            KillAllChild.KillAllChildInTransform(_parentTrans);
            if (_allScene.Length == 0)
            {
                Debug.Log("Load scene first!!");
                return;
            }
            foreach (SOScene scene in _allScene)
            {
                SetTmpText nowObject = (SetTmpText)UnityEditor.PrefabUtility.InstantiatePrefab(_scenePrefab);
                nowObject.transform.SetParent(_parentTrans, false);
                string sceneName = scene.SceneName == string.Empty ? scene.name : scene.SceneName;

                nowObject.SetText(sceneName);

                SceneChangeCaller ChangeScript = GetOrCreate.GetCreateComponent<SceneChangeCaller>(transform);
                ChangeScript.SceneToGoData = scene;
            }
        }
#endif
    }
}
