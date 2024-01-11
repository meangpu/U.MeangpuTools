using UnityEngine;
using UnityEditor;

namespace Meangpu
{
    public class DisplayGameVersionPanel : EditorWindow
    {
        private string versionName;
        [MenuItem("MeangpuTools/EditorUtil/GameVersionPanel")]
        static void Init()
        {
            DisplayGameVersionPanel window = (DisplayGameVersionPanel)GetWindow(typeof(DisplayGameVersionPanel));
            window.titleContent.text = "GameVersionPanel";
        }
        void OnGUI()
        {
            GUIStyle boldStyle = new()
            {
                fontSize = 15,
                fontStyle = FontStyle.Bold,
            };
            boldStyle.normal.textColor = Color.white;
            EditorGUILayout.LabelField(PlayerSettings.bundleVersion, boldStyle);
        }
    }
}