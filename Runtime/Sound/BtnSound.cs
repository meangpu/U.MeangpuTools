using UnityEngine;
using UnityEngine.UI;

public class BtnSound : MonoBehaviour
{
    Button[] _allBtnList;
    [SerializeField] string[] _audioNameForRandom;
    int index;

    private void Start()
    {
        _allBtnList = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button _b in _allBtnList) _b.onClick.AddListener(playRandomSound);
    }

    void playRandomSound()
    {
        index = Random.Range(0, _audioNameForRandom.Length);
        string _audioName = _audioNameForRandom[index];
        AudioManager.instance.Play(_audioName);
    }

}
