using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Meangpu
{
    public class SetIconWIndow : EditorWindow
    {
        const string k_menuPath = "Assets/Create/SetIcon..";
        List<Texture2D> m_icons = null;
        int m_selectedIcon = 0;

        [MenuItem(k_menuPath, priority = 0)]
        private static void ShowMenuItem()
        {
            SetIconWIndow window = (SetIconWIndow)GetWindow(typeof(SetIconWIndow));
            window.titleContent = new("Set icon");
            window.Show();
        }

        [MenuItem(k_menuPath, validate = true)]
        private static bool ShowMenuItemValidation()
        {
            foreach (Object asset in Selection.objects)
            {
                if (asset.GetType() != typeof(MonoScript)) return false;
            }
            return true;
        }

        private void OnGUI()
        {
            if (m_icons == null)
            {
                m_icons = new List<Texture2D>();
                string[] assetGuids = AssetDatabase.FindAssets("t:texture2d l:ScriptIcon");
                foreach (string assetGuid in assetGuids)
                {
                    string path = AssetDatabase.GUIDToAssetPath(assetGuid);
                    m_icons.Add(AssetDatabase.LoadAssetAtPath<Texture2D>(path));
                }
            }

            if (m_icons == null)
            {
                GUILayout.Label("No icons to display");
                if (GUILayout.Button("Close", GUILayout.Width(100))) Close();
            }
            else
            {
                m_selectedIcon = GUILayout.SelectionGrid(m_selectedIcon, m_icons.ToArray(), 5);
                if (Event.current != null)
                {
                    if (Event.current.isKey)
                    {
                        switch (Event.current.keyCode)
                        {
                            case KeyCode.KeypadEnter:
                            case KeyCode.Return:
                                ApplyIcon(m_icons[m_selectedIcon]);
                                Close();
                                break;
                            case KeyCode.Escape:
                                Close();
                                break;
                            default:
                                break;
                        }
                    }
                    else // double click
                    if (Event.current.button == 0 && Event.current.clickCount == 2)
                    {
                        ApplyIcon(m_icons[m_selectedIcon]);
                        Close();
                    }
                }
            }
            if (GUILayout.Button("Apply", GUILayout.Width(100)))
            {
                ApplyIcon(m_icons[m_selectedIcon]);
                Close();
            }
        }

        private void ApplyIcon(Texture2D icon)
        {
            AssetDatabase.StartAssetEditing();
            foreach (Object asset in Selection.objects)
            {
                string path = AssetDatabase.GetAssetPath(asset);
                MonoImporter monoImporter = AssetImporter.GetAtPath(path) as MonoImporter;
                monoImporter.SetIcon(icon);
                AssetDatabase.ImportAsset(path);
            }
            AssetDatabase.StopAssetEditing();
            AssetDatabase.Refresh();
        }
    }
}