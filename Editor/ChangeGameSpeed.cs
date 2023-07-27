using UnityEditor;
using UnityEngine;

namespace Meangpu
{
    public class ChangeGameSpeed : EditorWindow
    {
        public float GameSpeed;
        public int GameFramerate = 1;

        [MenuItem("MeangpuTools/GameSpeed-Framerate")]
        public static void ShowWindow()
        {
            GetWindow<ChangeGameSpeed>("Set GameSpeed-Framerate");
        }

        private void OnGUI()
        {
            // obj example
            // _targetPref = EditorGUILayout.ObjectField("TargetPrefab", _targetPref, typeof(Transform), true) as Transform;

            GameSpeed = EditorGUILayout.FloatField("GameSpeed", GameSpeed);

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

            EditorGUILayout.LabelField("Framerate", EditorStyles.boldLabel);
            GameFramerate = EditorGUILayout.IntField("GameFramerate", GameFramerate);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("5")) Application.targetFrameRate = 5;
            if (GUILayout.Button("10")) Application.targetFrameRate = 10;
            if (GUILayout.Button("30")) Application.targetFrameRate = 30;
            if (GUILayout.Button("InputBox")) Application.targetFrameRate = GameFramerate;
            EditorGUILayout.EndHorizontal();
        }
    }
}
