using UnityEngine;

namespace Meangpu.Util
{
    public static class OpenLinkURL
    {
        // prevent space in IOS device
        public static void OpenLink(string link)
        {
            bool googleSearch = link.Contains("google.com/search");
            string linkNoSpace = link.Replace(" ", googleSearch ? "+" : "%20");
            Application.OpenURL(linkNoSpace);
        }
    }
}