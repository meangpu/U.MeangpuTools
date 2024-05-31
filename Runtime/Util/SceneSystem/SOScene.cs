using VInspector;
using Meangpu.Util;
using UnityEngine;

namespace Meangpu
{
    public class SOScene : BaseSOScene
    {
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
