using Meangpu.Util;
using UnityEngine;
using VInspector;

#if UNITY_EDITOR
namespace Meangpu
{
    [ExecuteInEditMode]
    public class SceneButtonChangeSpawner : MonoBehaviour
    {
        [SerializeField] Transform _parentTrans;
        [SerializeField] SceneChangeInfoHolder _scenePrefab;
        [SerializeField] SOScene[] _allScene;
        [SerializeField] bool _doLoadAllTheTime = true;

        private void Start()
        {
            if (_doLoadAllTheTime)
            {
                LoadAllScene();
                EditorSpawnAllButtonScene();
            }
        }

        [Button] public void LoadAllScene() => _allScene = Resources.LoadAll<SOScene>("SOScene");

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
                SceneChangeInfoHolder nowObject = (SceneChangeInfoHolder)UnityEditor.PrefabUtility.InstantiatePrefab(_scenePrefab);
                nowObject.transform.SetParent(_parentTrans, false);
                nowObject.SetSceneToNewData(scene);
            }
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
}
#endif
