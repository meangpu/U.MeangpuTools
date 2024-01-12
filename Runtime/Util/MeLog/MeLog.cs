using UnityEngine;

namespace Meangpu
{
    public static class MeLog
    {
        public static void Log(string word) => Debug.Log($"<color=#37D2E1>{word}</color>");
        public static void Log(string word, Color color) => Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{word}</color>");
        public static void Log(string word, string hexColor) => Debug.Log($"<color=#{hexColor}>{word}</color>");
        public static void LogError(string word) => Debug.Log($"<color=#FF4936>{word}</color>");
    }
}
