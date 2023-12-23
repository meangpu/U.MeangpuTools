#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using VInspector;

namespace Meangpu.Util
{
    public class MeFastPlayModeToggle : MonoBehaviour
    {
        [ReadOnly] public bool NOW_FAST_PLAY_MODE_STATUS;

        [Button] void TOGGLE() => SetMode(!EditorSettings.enterPlayModeOptionsEnabled);
        [Button] void Set__FALSE() => SetMode(false);
        [Button] void Set__TRUE() => SetMode(true);

        private void SetMode(bool mode)
        {
            EditorSettings.enterPlayModeOptionsEnabled = mode;
            UpdateStatusDisplay();
        }

        private void UpdateStatusDisplay()
        {
            NOW_FAST_PLAY_MODE_STATUS = EditorSettings.enterPlayModeOptionsEnabled;
            Debug.Log($"SetFast: {EditorSettings.enterPlayModeOptionsEnabled}");
        }
    }
}
#endif