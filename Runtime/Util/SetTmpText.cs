using UnityEngine;
using TMPro;

public class SetTmpText : MonoBehaviour
{
    [SerializeField] TMP_Text _targetText;
    public void SetText(string word)
    {
        _targetText.SetText(word);
    }
}
