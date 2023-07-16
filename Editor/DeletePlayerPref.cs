using UnityEngine;
using UnityEditor;

namespace Meangpu
{
    public class DeletePlayerPref : EditorWindow
    {
        [MenuItem("MeangpuTools/Delete PlayerPrefs (All)")]
        static void DeleteAllPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}