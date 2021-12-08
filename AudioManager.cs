using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    public Sound[] sounds;
    
    [HideInInspector]
    public Sound CurrentSound;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixer;
        }
    }

    public void Play (string name)
    {
        
        Sound s = Array.Find(sounds, sound => sound.name == name);
        CurrentSound = s;
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found");
            return;
        }
        Debug.Log(name);
        if (s.source.isPlaying == false)
            s.source.Play();
    }

    public void Play(int index)
    {
        Sound s = sounds[index];
        CurrentSound = s;
        if (s == null)
        {
            Debug.LogWarning("Sound : " + name + " not found");
            return;
        }
        s.source.Play();
    }
}
