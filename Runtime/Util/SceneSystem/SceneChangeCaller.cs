using EasyButtons;
using UnityEngine;

namespace Meangpu.Util
{
    public class SceneChangeCaller : MonoBehaviour
    {
        [Expandable][SerializeField] SOScene _sceneToGo;

        [Button]
        public void LoadScene()
        {
            if (SceneChangeManager.instance == null)
            {
                Debug.LogError("Fail to find sceneChange instance");
                return;
            }
            SceneChangeManager.instance.LoadScene(_sceneToGo);
        }
    }
}