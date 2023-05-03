using UnityEditor;
using UnityEngine;

public class ChangeGameSpeed : EditorWindow
{

    public float GameSpeed;

    [MenuItem("MeangpuTools/GameSpeed")]
    public static void ShowWindow()
    {
        GetWindow<ChangeGameSpeed>("Set Game Speed");
    }

    private void OnGUI()
    {
        // obj example
        // _targetPref = EditorGUILayout.ObjectField("TargetPrefab", _targetPref, typeof(Transform), true) as Transform;

        GameSpeed = EditorGUILayout.FloatField("layer1", GameSpeed);

        EditorGUILayout.LabelField("TIME SETTING", EditorStyles.boldLabel);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("x0.1")) Time.timeScale = 0.1f;
        if (GUILayout.Button("x0.5")) Time.timeScale = 0.5f;
        if (GUILayout.Button("x1")) Time.timeScale = 1;
        if (GUILayout.Button("x2")) Time.timeScale = 2;
        if (GUILayout.Button("x5")) Time.timeScale = 5;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("x10")) Time.timeScale = 10;
        if (GUILayout.Button("x20")) Time.timeScale = 20;
        if (GUILayout.Button("InputFloatBoxSpeed")) Time.timeScale = GameSpeed;
        EditorGUILayout.EndHorizontal();

    }

}