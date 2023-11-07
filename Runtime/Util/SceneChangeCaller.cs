using UnityEngine;
using UnityEngine.SceneManagement;

namespace Meangpu.Util
{
    public class SceneChangeCaller : MonoBehaviour
    {
        [Expandable][SerializeField] SOScene _sceneToGo;

        public void LoadScene()
        {
            if (SceneChange.instance == null)
            {
                Debug.LogError("Fail to find sceneChange instance");
                return;
            }
            SceneChange.instance.LoadScene(_sceneToGo);
        }
    }
}