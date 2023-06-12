using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using static System.IO.Directory;
using static System.IO.Path;
using UnityEditor;
using static UnityEditor.AssetDatabase;
using static UnityEngine.Application;

namespace Meangpu
{
    public static class FolderSetup
    {
        [MenuItem("MeangpuTools/Setup/Create Default Folder")]
        public static void CreateDefaultFolders()
        {
            Dir("_Project", "_Scripts", "_Scenes", "Art", "_Prefabs", "Sound", "Data");
            Dir("_Project/Art", "Materials", "Model", "Icon");
            Dir("_Project/Resources", "SOSound");
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
            var existing = Path.Combine(dataPath, "../Packages/manifest.json");
            File.WriteAllText(existing, contents);
            UnityEditor.PackageManager.Client.Resolve();
        }
    }
}
