#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class Screenshot
{
    [MenuItem("Screenshot/Grab")]
    public static void Grab()
    {
        // if not Assets/ it will save at project root
        ScreenCapture.CaptureScreenshot("Assets/Screenshot" + GetCh() + GetCh() + GetCh() + "_" + Screen.width + "x" + Screen.height + ".png", 1);
    }

    public static char GetCh()
    {
        return (char)UnityEngine.Random.Range('A', 'Z');
    }
}
#endif
