// [Skip The Menu In Unity - YouTube] (https://www.youtube.com/watch?v=d7vsQ8AkpMY)
// this not work on two column project view = need to change to one column for it to work

// using UnityEngine;
// using UnityEditor;
// using UnityEditor.Callbacks;

// namespace Meangpu
// {
//     public static class OpenFolderTool
//     {
//         [OnOpenAsset]
//         public static bool OnOpenAsset(int instanceId)
//         {
//             Event e = Event.current;

//             if (e?.shift != true)
//                 return false;

//             Object obj = EditorUtility.InstanceIDToObject(instanceId);
//             string path = AssetDatabase.GetAssetPath(obj);
//             if (AssetDatabase.IsValidFolder(path))
//             {
//                 EditorUtility.RevealInFinder(path);
//             }
//             return true;
//         }
//     }
// }
