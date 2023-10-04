using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Meangpu
{
    [Overlay(typeof(SceneView), "ExportScene")]
    [Icon(k_icon)]
    public sealed class ExportCurrentScenePackage : ToolbarOverlay
    {
        // create for easy export music&sfx in this scene
        public const string k_icon = "Assets/Editor/Icons/UnityIcon.png";
        ExportCurrentScenePackage() : base(ExportNowScene.k_id) { }
        [EditorToolbarElement(k_id, typeof(SceneView))]

        class ExportNowScene : EditorToolbarDropdownToggle, IAccessContainerWindow
        {
            public const string k_id = "ExportCurrentScenePackage/ExportToggle";
            public EditorWindow containerWindow { get; set; }
            ExportNowScene()
            {
                text = "ExportPackage";
                tooltip = "Export current scene";
                icon = AssetDatabase.LoadAssetAtPath<Texture2D>(k_icon);
                dropdownClicked += ShowSceneMenu;
            }

            private void ShowSceneMenu()
            {
                GenericMenu menu = new();
                string path = SceneManager.GetActiveScene().path;
                AssetDatabase.ExportPackage(path, "scene.unitypackage",
                    ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
                menu.ShowAsContext();
            }
        }
    }
}