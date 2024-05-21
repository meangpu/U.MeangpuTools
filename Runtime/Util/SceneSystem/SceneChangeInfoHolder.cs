using VInspector;
using UnityEngine;
using TMPro;

namespace Meangpu.Util
{
    public class SceneChangeInfoHolder : MonoBehaviour
    {
        [Expandable][SerializeField] SOScene _data;
        [SerializeField] TMP_Text _sceneNameText;

        [Button]
        public void LoadScene()
        {
            if (SceneChangeManager.Instance == null)
            {
                Debug.LogError("Fail to find sceneChange instance");
                return;
            }
            if (_data == null)
            {
                Debug.LogError("No scene in caller script");
                return;
            }
            SceneChangeManager.Instance.LoadScene(_data);
        }

        public virtual void SetupVisual()
        {
            string sceneName = _data.SceneNameDisplay == string.Empty ? _data.name : _data.SceneNameDisplay;
            _sceneNameText.SetText(sceneName);
        }

        public void SetSceneToNewData(SOScene newData)
        {
            _data = newData;
            SetupVisual();
            gameObject.name = newData.name;
        }
    }
}