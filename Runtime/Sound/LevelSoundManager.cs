using UnityEngine;

public class LevelSoundManager : MonoBehaviour
{
    // play ambient sound
    [SerializeField] string playName;
    [SerializeField] bool StopOtherMusic;

    void Start()
    {
        if (StopOtherMusic)
        {
            AudioManager.instance.StopAllSound();
        }
        AudioManager.instance.Play(playName);
    }

}
