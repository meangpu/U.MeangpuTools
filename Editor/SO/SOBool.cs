using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New SOBool", menuName = "Meangpu/SOBool")]
public class SOBool : ScriptableObject
{
    [HideInInspector]
    public Texture2D _true;
    [HideInInspector]
    public Texture2D _false;
    public Texture2D PreviewIcon
    {
        get => BoolState ? _true : _false;
    }

    public bool BoolState;
}

[CustomEditor(typeof(SOBool))]
public class SOBoolEditor : Editor
{
    // learn more from here https://docs.unity3d.com/ScriptReference/EditorGUIUtility.SetIconForObject.html
    public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
    {
        SOBool SOBool = (SOBool)target;
        if (SOBool == null || SOBool.PreviewIcon == null) return null;

        Texture2D tex = new(width, height);
        EditorUtility.CopySerialized(SOBool.PreviewIcon, tex);
        return tex;
    }
}
