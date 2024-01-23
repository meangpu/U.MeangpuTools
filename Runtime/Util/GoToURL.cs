using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class GoToURL : MonoBehaviour
    {
        [SerializeField] string _linkToGo;
        [Button] public void LoadThisScriptURL() => Application.OpenURL(_linkToGo);
        [Button] public void LoadURL(string url) => Application.OpenURL(url);
    }
}