#if UNITY_EDITOR
using EasyButtons;
using UnityEngine;

namespace Meangpu
{
    public abstract class SOCreateSOTemplate : ScriptableObject
    {
        // example: $"Assets/Resources/SO/OperationData/{i + 1}.asset";
        public string path = "Assets/Resources/";
        public const string extension = ".asset";

        public string GetFullFilePath(string fileName) => $"{path}{fileName}{extension}";

        [Button] public abstract void CreateSO_ObjectList();
    }
}
#endif