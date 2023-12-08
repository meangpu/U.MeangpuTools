using UnityEditor;
using UnityEngine;
using Unity.EditorCoroutines.Editor;
using System.Collections;
using System.IO;

namespace Meangpu
{
    public static class Screenshot
    {
        public static Object currentImage;

        [MenuItem("Screenshot/GrabCam")]
        public static void GrabCam()
        {
            string imageName = $"{Application.dataPath}/_Project/Screenshot/{GetCh()}{GetCh()}{GetCh()}_{Screen.width}x{Screen.height}.png";
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

        [MenuItem("Screenshot/GrabSceneViewNoFixed")]
        public static void GrabSceneView(int width = 0, int height = 0)
        {
            // [CAPTURE the Scene View Camera Image in Unity - YouTube](https://www.youtube.com/watch?v=vrolg5A8jYE)

            string imageName = $"{Application.dataPath}/_Project/Screenshot/{GetCh()}{GetCh()}{GetCh()}_{Screen.width}x{Screen.height}.png";
            SceneView view = SceneView.lastActiveSceneView;

            if (width == 0 || height == 0)
            {
                width = view.camera.pixelWidth;
                height = view.camera.pixelHeight;
            }

            Texture2D capture = new(width, height);
            view.camera.Render();
            RenderTexture.active = view.camera.targetTexture;

            capture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            capture.Apply();
            byte[] bytes = capture.EncodeToPNG();
            Debug.Log($"{Application.dataPath + "/" + imageName}");
            File.WriteAllBytes(imageName, bytes);
            AssetDatabase.Refresh();
        }
        [MenuItem("Screenshot/GrabSceneView_720")]
        public static void GrabSceneView720() => GrabSceneView(1280, 720);
        [MenuItem("Screenshot/GrabSceneView_1080")]
        public static void GrabSceneView1080() => GrabSceneView(1920, 1080);
    }
}
