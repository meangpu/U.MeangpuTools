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
    }

}
