#if UNITY_EDITOR
using UnityEngine;
using System.Reflection;
using VInspector;

namespace Meangpu.Util
{
    public class ClearEditorConsoleLog : MonoBehaviour
    {
        [Button]
        public static void ClearLog()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
        }

        [Button] public static void TestLog() => Debug.Log("Test Log!");
    }
}
#endif