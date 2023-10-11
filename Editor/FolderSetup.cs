using UnityEditor;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using static UnityEditor.AssetDatabase;
using static UnityEngine.Application;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Unity.EditorCoroutines.Editor;

namespace Meangpu
{
    public static class FolderSetup
    {
        [MenuItem("MeangpuTools/__DO_SETUP")]
        public static void MainInitLoop()
        {
            CreateDefaultFolders();
            CreateEditorConfig();
            SetFastPlayMode();
        }

        [MenuItem("MeangpuTools/Setup/Fast Play Mode")]
        private static void SetFastPlayMode()
        {
            EditorSettings.enterPlayModeOptionsEnabled = true;
        }

        [MenuItem("MeangpuTools/Setup/Create Default Folder")]
        public static void CreateDefaultFolders()
        {
            Dir("_Project", "_Scripts", "_Scenes", "Art", "_Prefabs", "Sound", "Data", "Screenshot", "Editor");
            Dir("_Project/Art", "Materials", "Model", "Icon");
            Dir("Resources", "SOSound", "GameState");
            Refresh();
        }

        [MenuItem("MeangpuTools/Setup/Create Editor Config")]
        public static void CreateEditorConfig()
        {
            string projectFolderPath = GetDirectoryName(dataPath);
            string editorConfigPath = projectFolderPath + "\\.editorconfig";
            const string EditorConfigLink = "https://gist.github.com/meangpu/ba88b1d2f35d611cbfac91c659211ac3/raw/";

            if (!File.Exists(editorConfigPath))
            {
                EditorCoroutineUtility.StartCoroutineOwnerless(GetText(EditorConfigLink, editorConfigPath));
            }
        }

        [MenuItem("MeangpuTools/Setup/Edit Editor Config")]
        public static void EditEditorConfig()
        {
            OpenURL("https://gist.github.com/meangpu/ba88b1d2f35d611cbfac91c659211ac3/");
        }

        public static IEnumerator GetText(string url, string path)
        {
            UnityWebRequest www = UnityWebRequest.Get(url);
            Debug.Log(www.downloadHandler.text);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log($"<color=red>{www.error}</color>");
            }
            else
            {
                string fileContent = www.downloadHandler.text;
                File.WriteAllText(path, fileContent);
                Debug.Log($"write{fileContent} to {path}");
            }
        }

        public static void Dir(string root, params string[] dir)
        {
            var fullPath = Combine(dataPath, root);
            foreach (var newDirectory in dir)
            {
                CreateDirectory(Combine(fullPath, newDirectory));
            }
        }

        static async void LoadNewManifest(string packageID)
        {
            var url = GetGistUrl(packageID);
            var contents = await GetContents(url);
            ReplacePackageFile(contents);
        }

        [MenuItem("MeangpuTools/Setup/Load MINIMAL Manifest")]
        static void LoadMinimalPackage()
        {
            // https://gist.githubusercontent.com/meangpu/6c2d6c6292a3f36de0e445acd2535698/raw/
            LoadNewManifest("6c2d6c6292a3f36de0e445acd2535698"); // minimal package
        }

        [MenuItem("MeangpuTools/Setup/Load VR Manifest")]
        static void LoadVRPackage()
        {
            // https://gist.githubusercontent.com/meangpu/25cefd54ac38510850b5174a6026b64e/raw/
            LoadNewManifest("25cefd54ac38510850b5174a6026b64e"); // vr package
        }

        static string GetGistUrl(string id, string user = "meangpu") => $"https://gist.githubusercontent.com/{user}/{id}/raw/";

        static async Task<string> GetContents(string url)
        {
            using var client = new HttpClient();
            var respond = await client.GetAsync(url);
            var content = await respond.Content.ReadAsStringAsync();
            return content;
        }

        static void ReplacePackageFile(string contents)
        {
            var existing = Combine(dataPath, "../Packages/manifest.json");
            File.WriteAllText(existing, contents);
            UnityEditor.PackageManager.Client.Resolve();
        }
    }
}