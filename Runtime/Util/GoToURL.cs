using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class GoToURL : MonoBehaviour
    {
        [SerializeField] string _linkToGo;
        [Button]
        public void LoadThisScriptURL()
        {
            if (string.IsNullOrEmpty(_linkToGo)) return;
            OpenLinkURL.OpenLink(_linkToGo);
        }

        [Button]
        public void LoadURL(string url)
        {
            if (string.IsNullOrEmpty(_linkToGo)) return;
            OpenLinkURL.OpenLink(url);
        }
    }
}