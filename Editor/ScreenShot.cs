using UnityEditor;
using UnityEngine;
using System.IO;

namespace Meangpu
{
    public static class Screenshot
    {
        public const string ScreenShotPath = "Assets/_Project/Screenshot";

        [MenuItem("Screenshot/GrabCam")]
        public static void GrabCam()
        {
            string fileName = $"_Project/Screenshot/{GetCh()}{GetCh()}{GetCh()}_{Screen.width}x{Screen.height}.png";
            string filePath = $"{Application.dataPath}/{fileName}";
            ScreenCapture.CaptureScreenshot(filePath, 1);
            Debug.Log($"<color=#4ec9b0>{filePath} was create!</color>");
            OpenScreenShotFolder();
        }

        private static void SelectObjectOnEditor(string imageName)
        {
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Object nowImage = AssetDatabase.LoadAssetAtPath<Object>(imageName);
            PingAndSetActive(nowImage);
            AssetDatabase.Refresh();
        }

        public static void PingAndSetActive(Object obj)
        {
            Selection.activeObject = obj;
            EditorGUIUtility.PingObject(obj);
        }

        private static void OpenScreenShotFolder()
        {
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(ScreenShotPath);
            AssetDatabase.OpenAsset(obj);
            PingAndSetActive(obj);
            AssetDatabase.Refresh();
        }

        public static char GetCh() => (char)Random.Range('A', 'Z');

        [MenuItem("Screenshot/GrabSceneViewNoFixed")]
        public static void GrabSceneView()
        {
            // [CAPTURE the Scene View Camera Image in Unity - YouTube](https://www.youtube.com/watch?v=vrolg5A8jYE)
            SceneView view = SceneView.lastActiveSceneView;

            int width = view.camera.pixelWidth;
            int height = view.camera.pixelHeight;

            string fileName = $"_Project/Screenshot/{GetCh()}{GetCh()}{GetCh()}_{width}x{height}.png";
            string filePath = $"{Application.dataPath}/{fileName}";

            Texture2D capture = new(width, height);
            view.camera.Render();
            RenderTexture.active = view.camera.targetTexture;

            capture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            capture.Apply();
            byte[] bytes = capture.EncodeToPNG();
            Debug.Log($"<color=#4ec9b0>{filePath} was create!</color>");
            File.WriteAllBytes(filePath, bytes);

            SelectObjectOnEditor($"Assets/{fileName}");
        }
    }
}
