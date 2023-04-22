using UnityEngine;
using UnityEditor;
using TMPro;
using System;

[RequireComponent(typeof(TMP_Text))]
[ExecuteInEditMode]
public class GameVersionDisplay : MonoBehaviour
{
    [SerializeField] string _frontWord = "v.";

    void Awake() => MainLoop();

    void MainLoop()
    {
        if (!IsProjectFormatCorrect()) SetFirstTimeFormat();

        TMP_Text textVersion = GetComponent<TMP_Text>();
        string[] versionValue = Application.version.Split('-', 2); // split in to 2 part

        textVersion.text = $"{_frontWord}{versionValue[0]}\n<size=20>{versionValue[1]}";
    }

    bool IsProjectFormatCorrect()
    {
        string[] currentVersionNumber = PlayerSettings.bundleVersion.Split('-');
        return currentVersionNumber.Length >= 1 ? true : false;
    }

    public static void SetFirstTimeFormat()
    {
        Debug.Log("Perform first time version set");
        string date = DateTime.Now.ToString("d-MMM-yyy");
        string time = DateTime.Now.ToString("t");
        string finalDateTime = $"{date} {time}";
        PlayerSettings.bundleVersion = $"0.0-{finalDateTime}";
        Debug.Log(PlayerSettings.bundleVersion);
        UnityEditor.AssetDatabase.Refresh();
    }
}
