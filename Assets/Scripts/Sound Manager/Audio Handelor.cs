using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandelor: MonoBehaviour
{
    public static AudioHandelor instance;

    public Audios[] musicSound, sfxSound;
    public AudioSource musicSource, sfxSource;

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
    }

    private void Start()
    {
        //PlayMusic("");
    }

    public void PlayMusic(string audioName)
    {
        Audios sound = Array.Find(musicSound, x => x.audioName == audioName);

        if (sound == null)
        {
            Debug.Log("Sound NOt found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string audioName)
    {
        Audios sound = Array.Find(sfxSound, x => x.audioName == audioName);

        if (sound == null)
        {
            Debug.Log("SFX NOt found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
