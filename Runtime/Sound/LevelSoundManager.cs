using UnityEngine;

public class LevelSoundManager : MonoBehaviour
{
    // play ambient sound
    [SerializeField] string _playName;
    [SerializeField] bool _stopOtherMusic;

    void Start()
    {
        if (_stopOtherMusic) AudioManager.instance.StopAllSound();
        AudioManager.instance.Play(_playName);
    }

}
