using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
[ExecuteInEditMode]
public class GameVersionDisplay : MonoBehaviour
{
    [SerializeField] string _frontWord = "v.";

    void Awake() => MainLoop();

    void MainLoop()
    {
        TMP_Text textVersion = GetComponent<TMP_Text>();
        string[] versionValue = Application.version.Split('-', 2); // split in to 2 part
        if (versionValue.Length == 2)
        {
            textVersion.text = $"{_frontWord}{versionValue[0]}\n<size=20>{versionValue[1]}";
        }
        else textVersion.text = $"<color=red>Game version format is wrong</color>";

    }
}