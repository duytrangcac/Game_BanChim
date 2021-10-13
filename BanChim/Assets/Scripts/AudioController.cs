using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    [Header("Main Settings:")]
    [Range(0, 1)]
    public float musicVolume;
    [Range(0, 1)]
    public float sfxVolume;

    public AudioSource musicAus;
    public AudioSource sfxAus;

    [Header("Game Sounds And Musics:")]
    public AudioClip shooting;
    public AudioClip win;
    public AudioClip lose;
    public AudioClip[] bgMusics;

    public override void Start()
    {
        PlayMusic(bgMusics);
    }
    public void playSound(AudioClip sound,AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;  
        }
        if (aus)
        {
            aus.PlayOneShot(sound, sfxVolume);

        }

        
    }
    public void PlaySound(AudioClip [] sounds,AudioSource aus = null)
    {
        if (!aus)
        {
            aus = sfxAus;
        }
        if (aus)
        {
            int randIdx = Random.Range(0, sounds.Length);
            if (sounds[randIdx] != null)
            {
                aus.PlayOneShot(sounds[randIdx], sfxVolume);
            }
        }
    }
    public void PlayMusic(AudioClip music,bool loop=true)
    {
        if (musicAus)
        {
            musicAus.clip = music;
            musicAus.loop = loop;
            musicAus.volume = musicVolume;
            musicAus.Play();
        }
    }
    public void PlayMusic(AudioClip[] musics, bool loop = true)
    {
        if (musicAus)
        {
            int ranIndx = Random.Range(0, musics.Length);
            if (musics[ranIndx] != null)
            {
                musicAus.clip = musics[ranIndx];
                musicAus.loop = loop;
                musicAus.volume = musicVolume;
                musicAus.Play();
            }
           
        }
    }
}
