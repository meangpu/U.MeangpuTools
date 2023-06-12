#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class Screenshot
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
        Object obj = AssetDatabase.LoadAssetAtPath(imageName, typeof(Object));
        Selection.activeObject = obj;
    }

    public static char GetCh() => (char)UnityEngine.Random.Range('A', 'Z');
}

#endif
