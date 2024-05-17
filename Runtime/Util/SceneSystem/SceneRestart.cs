using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class SceneRestart : MonoBehaviour
    {
        [Button] public void RestartThisScene() => SceneChangeManager.Instance.RestartThisScene();
    }
}