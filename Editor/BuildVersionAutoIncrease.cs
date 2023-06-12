using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;

namespace Meangpu
{
    public class BuildVersionAutoIncrease : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;
        private const string initialVersion = "0.0";

        public void OnPreprocessBuild(BuildReport report)
        {
            string currentVersion = FindCurrentVersion();
            UpdateVersion(currentVersion);
        }

        private string FindCurrentVersion()
        {
            // the first version in version setting should be 0.01-11/30/2022 2:26 PM
            string[] currentVersionNumber = PlayerSettings.bundleVersion.Split('-');
            return currentVersionNumber.Length >= 1 ? currentVersionNumber[0] : initialVersion;
        }

        private void UpdateVersion(string version)
        {
            if (float.TryParse(version, out float versionNumber))
            {
                float newVersion = versionNumber + 0.01f;
                string date = DateTime.Now.ToString("d-MMM-yyy");
                string time = DateTime.Now.ToString("t");
                string finalDateTime = $"{date} {time}";
                PlayerSettings.bundleVersion = $"{newVersion}-{finalDateTime}";
                Debug.Log(PlayerSettings.bundleVersion);
            }
        }

        [MenuItem("MeangpuTools/Setup/Set Game Version")]
        public static void SetFirstTimeFormat()
        {
            Debug.Log("Perform first time version set");
            string date = DateTime.Now.ToString("d-MMM-yyy");
            string time = DateTime.Now.ToString("t");
            string finalDateTime = $"{date} {time}";
            PlayerSettings.bundleVersion = $"0.0-{finalDateTime}";
            Debug.Log(PlayerSettings.bundleVersion);
            UnityEditor.AssetDatabase.Refresh();
        }
    }
}