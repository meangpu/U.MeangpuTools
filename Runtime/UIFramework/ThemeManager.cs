using UnityEngine;

namespace Meangpu.UI
{
    [DefaultExecutionOrder(-1)]
    public class ThemeManager : MonoBehaviour
    {
        [Expandable]
        [SerializeField] SOTheme _mainTheme;
        public static ThemeManager Instance;
        private void Awake() => Instance = this;
        public SOTheme GetMainTheme() => _mainTheme;
        void OnValidate() => Instance = this;
    }
}