using UnityEngine;
using UnityEditor;

public class DeletePlayerPref : EditorWindow
{
    [MenuItem("MeangpuTools/Delete PlayerPrefs (All)")]
    static void DeleteAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
