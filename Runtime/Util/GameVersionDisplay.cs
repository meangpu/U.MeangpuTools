using UnityEngine;
using TMPro;

namespace Meangpu
{
    [RequireComponent(typeof(TMP_Text))]
    [ExecuteInEditMode]
    public class GameVersionDisplay : MonoBehaviour
    {
        [SerializeField] string _frontWord = "v.";
        [SerializeField] float _lowerTextSize = 20;

        void Awake() => MainLoop();

        void MainLoop()
        {
            TMP_Text textVersion = GetComponent<TMP_Text>();
            string[] versionValue = Application.version.Split('-', 2); // split in to 2 part
            if (versionValue.Length == 2) textVersion.text = $"{_frontWord}{versionValue[0]}\n<size={_lowerTextSize}>{versionValue[1]}";
            else textVersion.text = $"<color=red>Game version format is wrong</color>";
        }
    }
}
