using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class GoToURL : MonoBehaviour
    {
        [SerializeField] string _linkToGo;
        [Button] public void LoadThisScriptURL() => OpenLinkURL.OpenLink(_linkToGo);
        [Button] public void LoadURL(string url) => OpenLinkURL.OpenLink(url);
    }
}