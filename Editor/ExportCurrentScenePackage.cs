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

        class ExportNowScene : EditorToolbarDropdownToggle
        {
            public const string k_id = "ExportCurrentScenePackage/ExportToggle";
            public string ExportFileName = "ExportedScene.unitypackage";

            ExportNowScene()
            {
                text = "ExportPackage";
                tooltip = "Export current scene";
                icon = AssetDatabase.LoadAssetAtPath<Texture2D>(k_icon);
                dropdownClicked += ExportPackage;
            }

            private void ExportPackage()
            {
                string path = SceneManager.GetActiveScene().path;
                Debug.Log($"<color=#4ec9b0>Exporting :{path}:</color>");

                AssetDatabase.ExportPackage(path, ExportFileName,
                    ExportPackageOptions.Recurse | ExportPackageOptions.IncludeDependencies);
                EditorUtility.RevealInFinder(ExportFileName);

                Debug.Log($"<color=#4ec9b0>Finish export file at {ExportFileName}</color>");
            }
        }
    }
}