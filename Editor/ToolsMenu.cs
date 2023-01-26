using System.Net.Mime;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using UnityEditor;
using static UnityEditor.AssetDatabase;
using static UnityEngine.Application;

namespace meangpu
{
    public static class ToolsMenu
    {
        [MenuItem("MeangpuTools/Setup/Create Default Folder")]
        public static void CreateDefaultFolders()
        {

            Dir("_Project", "_Scripts", "_Scenes", "Art", "_Prefabs", "Sound");
            Dir("_Project/Art", "Materials");
            Refresh();
        }

        public static void Dir(string root, params string[] dir)
        {
            var fullPath = Combine(dataPath, root);
            foreach (var newDirectory in dir)
            {
                CreateDirectory(Combine(fullPath, newDirectory));

            }
        }

        [MenuItem("MeangpuTools/Setup/Load New Manifest")]
        static async void LoadNewManifest()
        {
            // https://gist.githubusercontent.com/meangpu/25cefd54ac38510850b5174a6026b64e/raw/
            var url = GetGistUrl("25cefd54ac38510850b5174a6026b64e");
            var contents = await GetContents(url);
            ReplacePackageFile(contents);
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
            var existing = Path.Combine(Application.dataPath, "../Packages/manifest.json");
            File.WriteAllText(existing, contents);
            UnityEditor.PackageManager.Client.Resolve();
        }


    }

}
