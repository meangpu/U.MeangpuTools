using UnityEngine;
using UnityEditor;

namespace Meangpu
{
    public class ChangePlayMode : EditorWindow
    {
        [MenuItem("MeangpuTools/TogglePlayMode", priority = 9998)]
        public static void TOGGLE() => SetMode(!EditorSettings.enterPlayModeOptionsEnabled);
        void Set__FALSE() => SetMode(false);
        void Set__TRUE() => SetMode(true);
        public static void SetMode(bool mode)
        {
            EditorSettings.enterPlayModeOptionsEnabled = mode;
            UpdateStatusDisplay();
        }
        public static void UpdateStatusDisplay() => Debug.Log($"SetFast: {EditorSettings.enterPlayModeOptionsEnabled}");
    }
}