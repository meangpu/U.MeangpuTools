using System.IO;
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.Toolbars;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

namespace Meangpu
{
    [Overlay(typeof(SceneView), "Scene selection")]
    [Icon(k_icon)]
    public sealed class SceneSelectionOverlay : ToolbarOverlay
    {
        public const string k_icon = "Assets/Editor/Icons/UnityIcon.png";

        SceneSelectionOverlay() : base(SceneDropDownToggle.k_id) { }

        [EditorToolbarElement(k_id, typeof(SceneView))]
        class SceneDropDownToggle : EditorToolbarDropdownToggle, IAccessContainerWindow
        {
            public const string k_id = "SceneSelectionOverlay/SceneDropDownToggle";
            public EditorWindow containerWindow { get; set; }
            SceneDropDownToggle()
            {
                text = "Scenes";
                tooltip = "select scene to load";
                icon = AssetDatabase.LoadAssetAtPath<Texture2D>(k_icon);
                dropdownClicked += ShowSceneMenu;
            }

            private void ShowSceneMenu()
            {
                GenericMenu menu = new();

                Scene currentScene = SceneManager.GetActiveScene();

                string[] sceneGuids = AssetDatabase.FindAssets("t:scene", null);
                for (var i = 0; i < sceneGuids.Length; i++)
                {
                    string path = AssetDatabase.GUIDToAssetPath(sceneGuids[i]);
                    string name = Path.GetFileNameWithoutExtension(path);
                    menu.AddItem(new GUIContent(name), name.Equals(currentScene.name), () => OpenScene(currentScene, path));
                }
                menu.ShowAsContext();
            }

            void OpenScene(Scene currentScene, string path)
            {
                if (currentScene.isDirty)
                {
                    if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                        EditorSceneManager.OpenScene(path);
                }
                else
                {
                    EditorSceneManager.OpenScene(path);
                }
            }
        }
    }
}