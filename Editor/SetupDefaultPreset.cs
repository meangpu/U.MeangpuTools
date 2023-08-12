using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Presets;
using static UnityEditor.AssetDatabase;

namespace Meangpu
{
    public class SetupDefaultPreset : EditorWindow
    {
        public static string FolderPath = "Assets/Libraries";

        [MenuItem("MeangpuTools/Setup Preset")]
        public static void ShowWindow() => GetWindow<SetupDefaultPreset>("Set Default preset");

        private void OnGUI()
        {
            FolderPath = EditorGUILayout.TextField("FolderPath", FolderPath);
            if (GUILayout.Button("SetPreset")) SetDefaultPreset();
        }

        private static void SetDefaultPreset()
        {
            foreach (string guid in FindAssets("t:preset", new[] { FolderPath }))
            {
                string path = GUIDToAssetPath(guid);
                Preset preset = LoadAssetAtPath<Preset>(path);

                PresetType type = preset.GetPresetType();
                List<DefaultPreset> list = new(Preset.GetDefaultPresetsForType(type))
                {
                    new DefaultPreset(null, preset)
                };
                Preset.SetDefaultPresetsForType(type, list.ToArray());
                Debug.Log($"set:{type}, with:{guid}");
            }
        }
    }
}