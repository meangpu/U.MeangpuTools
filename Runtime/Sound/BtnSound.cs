using UnityEngine;
using UnityEngine.UI;

public class BtnSound : MonoBehaviour
{
    Button[] allBtnList;
    [SerializeField] string[] _audioNameForRandom;
    AudioManager audScpt;
    int index;

    private void Start()
    {
        getAndSetAllBtn();
    }

    void playRandomSound()
    {
        // don't try optimize by move it to start it will make thing worse // don't destroy on load will make it get error
        audScpt = FindObjectOfType<AudioManager>();
        index = Random.Range(0, _audioNameForRandom.Length);
        string _audioName = _audioNameForRandom[index];
        audScpt.Play(_audioName);
    }

    void getAndSetAllBtn()
    {
        allBtnList = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button _b in allBtnList)
        {
            _b.onClick.AddListener(playRandomSound);
        }
    }
}
