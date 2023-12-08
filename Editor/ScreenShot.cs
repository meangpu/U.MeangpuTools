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
            int width = Screen.width;
            int height = Screen.height;

            string fileName = $"_Project/Screenshot/{GetDateTime()}_{width}x{height}.png";
            string filePath = $"{Application.dataPath}/{fileName}";

            Camera mainCam = Camera.main;
            RenderTexture originalCamTex = mainCam.targetTexture;

            RenderTexture screenTexture = new(width, height, 16);
            mainCam.targetTexture = screenTexture;
            RenderTexture.active = screenTexture;
            mainCam.Render();
            Texture2D renderedTexture = new(width, height);
            renderedTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            RenderTexture.active = null;
            byte[] byteArray = renderedTexture.EncodeToPNG();
            File.WriteAllBytes(filePath, byteArray);

            mainCam.targetTexture = originalCamTex;

            Debug.Log($"<color=#4ec9b0>{filePath} was create!</color>");
            SelectObjectOnEditor($"Assets/{fileName}");

            AssetDatabase.Refresh();
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

        public static char GetCh() => (char)Random.Range('A', 'Z');
        public static string GetDateTime() => System.DateTime.Now.ToString("dd-MMM-yy_HHmmss)");

        [MenuItem("Screenshot/GrabSceneViewNoFixed")]
        public static void GrabSceneView()
        {
            // [CAPTURE the Scene View Camera Image in Unity - YouTube](https://www.youtube.com/watch?v=vrolg5A8jYE)
            SceneView view = SceneView.lastActiveSceneView;

            int width = view.camera.pixelWidth;
            int height = view.camera.pixelHeight;

            string fileName = $"_Project/Screenshot/{GetDateTime()}_{width}x{height}.png";
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
