#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Meangpu
{
    public static class Screenshot
    {
        [MenuItem("Screenshot/Grab")]
        public static void Grab()
        {
            // if not Assets/ it will save at project root
            string imageName = $"Assets/Screenshot{GetCh()}{GetCh()}{GetCh()}_{Screen.width}x{Screen.height}.png";

            ScreenCapture.CaptureScreenshot(imageName, 1);

            Debug.Log($"<color=#4ec9b0>{imageName} was create!</color>");
            SelectObjectOnEditor(imageName);
        }

        private static void SelectObjectOnEditor(string imageName)
        {
            AssetDatabase.Refresh();
            Selection.activeObject = AssetDatabase.LoadAssetAtPath(imageName, typeof(Object));
        }

        public static char GetCh() => (char)Random.Range('A', 'Z');
    }
}

#endif
