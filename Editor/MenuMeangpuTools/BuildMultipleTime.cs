using UnityEditor;
using UnityEngine;

namespace Meangpu
{
    public class BuildMultipleTime : EditorWindow
    {
        public int levelNow;
        public static string FolderPath = "D:\\Github\\SPOLS_Web\\level";
        // public int[] buildList = { 4, 5, 6, 7, 8 };

        [MenuItem("MeangpuTools/EditorUtil/Build/BuildMultipleTime")]
        public static void ShowWindow() => GetWindow<BuildMultipleTime>("BuildSetting");

        private void OnGUI()
        {
            levelNow = EditorGUILayout.IntField("lvNow", levelNow);
            FolderPath = EditorGUILayout.TextField("FolderPath", FolderPath);

            if (GUILayout.Button("BuildOneLevel"))
            {
                FullBuildLoop(levelNow);
            }

            if (GUILayout.Button("BuildLOOP"))
            {
                int[] buildList = { 4, 5, 6, 7, 8 };
                foreach (int nowLv in buildList)
                {
                    Debug.Log(nowLv);
                    FullBuildLoop(nowLv);
                }
            }

            void FullBuildLoop(int buildLev)
            {
                SetAllScene(false);
                SetActiveScene(buildLev);
                string BuildPath = GetBuildLocation(buildLev);
                BuildAndRun(BuildPath);
            }

            void BuildAndRun(string path)
            {
                BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, path, BuildTarget.WebGL, BuildOptions.AutoRunPlayer);
            }

            string GetBuildLocation(int nowLevel)
            {
                return $"{FolderPath}{nowLevel}";
            }

            void SetAllScene(bool isEnable)
            {
                EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
                foreach (EditorBuildSettingsScene scene in scenes)
                {
                    scene.enabled = isEnable;
                }
                EditorBuildSettings.scenes = scenes;
            }

            void SetActiveScene(int sceneID)
            {
                int menuID = (sceneID * 2) - 2;
                int gameID = (sceneID * 2) - 1;

                EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;
                EnableSceneUseID();
                EditorBuildSettings.scenes = scenes;

                void EnableSceneUseID()
                {
                    for (int i = 0; i < scenes.Length; i++)
                    {
                        if (i == menuID || i == gameID)
                        {
                            scenes[i].enabled = true;
                        }
                    }
                }
            }
        }
    }
}