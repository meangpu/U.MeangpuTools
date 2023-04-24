using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    // AudioManager.instance?.Play(playName);

    [SerializeField] Sound[] _sounds;
    public static AudioManager instance;
    private AudioSource[] _allAudioSources;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (Sound s in _sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.spatialBlend = s.blend;
            s.source.loop = s.loop;
        }
    }

    Sound FindSound(string soundName)
    {
        Sound foundSound = Array.Find(_sounds, sound => sound.audioName == soundName);
        if (foundSound == null)
        {
            Debug.Log("No Sound Found");
            return null;
        }
        return foundSound;
    }

    public void Play(string name)
    {
        FindSound(name).source.Play();
    }

    public void PlayOneShot(string name)
    {
        Sound soundObj = FindSound(name);
        soundObj.source.PlayOneShot(soundObj.clip);
    }

    public void PlayOneChangePitch(string name, float newPitchVal)
    {
        Sound soundObj = FindSound(name);
        soundObj.source.pitch = newPitchVal;
        soundObj.source.PlayOneShot(soundObj.clip);
    }

    public void PlayRandomPitch(string name, float min = 0.7f, float max = 1.5f, float volume = 0)
    {
        Sound soundObj = FindSound(name);
        if (volume != 0) soundObj.source.volume = volume;
        soundObj.source.pitch = UnityEngine.Random.Range(min, max);
        soundObj.source.Play();
    }

    public void PlayRandomPitchMoreSwing(string name, float min = 0.5f, float max = 2.5f, float volume = 0)
    {
        Sound soundObj = FindSound(name);
        if (volume != 0) soundObj.source.volume = volume;
        soundObj.source.pitch = UnityEngine.Random.Range(min, max);
        soundObj.source.Play();
    }

    public void PlayOneRandomPitch(string name, float min = 0.7f, float max = 1.5f, float volume = 0)
    {
        Sound soundObj = FindSound(name);
        if (volume != 0) soundObj.source.volume = volume;
        soundObj.source.pitch = UnityEngine.Random.Range(min, max);
        soundObj.source.PlayOneShot(soundObj.clip);
    }

    public void StopAllSound()
    {
        _allAudioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audio in _allAudioSources)
        {
            audio.Stop();
        }
    }

    public AudioSource FindAudioSource(string soundName)
    {
        Sound foundSound = Array.Find(_sounds, sound => sound.audioName == soundName);
        if (foundSound == null)
        {
            Debug.Log("No Sound Found");
            return null;
        }
        AudioSource audioSourceComponent = foundSound.source;
        return audioSourceComponent;
    }

    public void MuteSourceByName(string soundName)
    {
        Sound foundSound = Array.Find(_sounds, sound => sound.audioName == soundName);
        if (foundSound == null)
        {
            Debug.Log("No Sound Found");
            return;
        }
        AudioSource audioSourceComponent = foundSound.source;
        audioSourceComponent.volume = 0;
    }

    public void SetSourceVolumeByName(string soundName, int newVol)
    {
        Sound foundSound = Array.Find(_sounds, sound => sound.audioName == soundName);
        if (foundSound == null)
        {
            Debug.Log("No Sound Found");
            return;
        }
        foundSound.source.volume = newVol;
    }

    public void ResetVolumeByName(string soundName)
    {
        Sound foundSound = Array.Find(_sounds, sound => sound.audioName == soundName);
        if (foundSound == null)
        {
            Debug.Log("No Sound Found");
            return;
        }
        foundSound.source.volume = foundSound.volume;
    }

}
