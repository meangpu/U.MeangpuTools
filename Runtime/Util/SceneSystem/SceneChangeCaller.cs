using VInspector;
using UnityEngine;

namespace Meangpu.Util
{
    public class SceneChangeCaller : MonoBehaviour
    {
        [Expandable] public SOScene SceneToGoData;

        [Button]
        public void LoadScene()
        {
            if (SceneChangeManager.instance == null)
            {
                Debug.LogError("Fail to find sceneChange instance");
                return;
            }

            if (SceneToGoData == null)
            {
                Debug.LogError("No scene in caller script");
                return;
            }
            SceneChangeManager.instance.LoadScene(SceneToGoData);
        }
    }
}