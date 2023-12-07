using UnityEditor;
using UnityEngine;

namespace Meangpu
{
    /// <summary>
    /// learn from this awesome guy [Next LEVEL Unity Hierarchy - YouTube](https://www.youtube.com/watch?v=EFh7tniBqkk) 
    /// </summary>
    public static class UnityEditorBackgroundColor
    {
        static readonly Color k_defaultColor = new(.7843f, .7843f, .7843f);
        static readonly Color k_defaultProColor = new(.2196f, .2196f, .2196f);

        static readonly Color k_selectedColor = new(.22745f, .447f, .6902f);
        static readonly Color k_selectedProColor = new(.1725f, .3647f, .5294f);

        static readonly Color k_selectedUnFocusedColor = new(.68f, 68f, 68f);
        static readonly Color k_selectedUnFocusedProColor = new(.3f, .3f, .3f);

        static readonly Color k_hoveredColor = new(.698f, .698f, .698f);
        static readonly Color k_hoveredProColor = new(.2706f, .2706f, .2706f);



        public static Color Get(bool isSelected, bool isHovered, bool isWindowFocused)
        {
            if (isSelected)
            {
                if (isWindowFocused) return EditorGUIUtility.isProSkin ? k_selectedProColor : k_selectedColor;
                else return EditorGUIUtility.isProSkin ? k_selectedUnFocusedProColor : k_selectedUnFocusedColor;
            }
            else if (isHovered)
            {
                return EditorGUIUtility.isProSkin ? k_hoveredProColor : k_hoveredColor;
            }
            else
            {
                return EditorGUIUtility.isProSkin ? k_defaultProColor : k_defaultColor;
            }

        }
    }
}