#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using System.Collections;

namespace Meangpu
{
    public static class Screenshot
    {
        public static Object currentImage;

        [MenuItem("Screenshot/Grab")]
        public static void Grab()
        {
            // if not Assets/ it will save at project root
            string imageName = $"Assets/_Project/Screenshot/{GetCh()}{GetCh()}{GetCh()}_{Screen.width}x{Screen.height}.png";
            ScreenCapture.CaptureScreenshot(imageName, 1);
            Debug.Log($"<color=#4ec9b0>{imageName} was create!</color>");
            EditorCoroutineUtility.StartCoroutine(SelectObjectOnEditor(imageName), new());
        }

        private static IEnumerator SelectObjectOnEditor(string imageName)
        {
            yield return new EditorWaitForSeconds(1.2f);
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            currentImage = AssetDatabase.LoadAssetAtPath(imageName, typeof(Object));
            Selection.activeObject = currentImage;
        }

        public static char GetCh() => (char)Random.Range('A', 'Z');
    }
}

#endif
