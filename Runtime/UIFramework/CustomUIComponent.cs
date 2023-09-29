using EasyButtons;
using UnityEngine;

namespace Meangpu.UI
{
    public abstract class CustomUIComponent : MonoBehaviour
    {
        [Expandable]
        [Tooltip("This is optional, if null will use ThemeManager one")]
        public SOTheme OverwriteTheme;

        private void Awake() => Init();

        [Button]
        public void Init()
        {
            Setup();
            Configure();
        }

        public abstract void Setup();
        public abstract void Configure();

        private void OnValidate() => Init();

        protected SOTheme GetMainTheme()
        {
            if (OverwriteTheme != null) return OverwriteTheme;
            else if (ThemeManager.Instance != null) return ThemeManager.Instance.GetMainTheme();
            return null;
        }
    }
}