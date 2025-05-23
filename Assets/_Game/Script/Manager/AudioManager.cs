using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("----- Audio Source -----")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("----- Audio Clip -----")]
    public AudioClip backgroundClip;
    public AudioClip click;
    public AudioClip win;
    public AudioClip loose;
    public AudioClip collect;
    public AudioClip eatBubble;
    public AudioClip clickMoveBtn;

    private bool isMuted;

    public bool IsMuted => isMuted;

    /*private void Start()
    {
        musicSource.clip = backgroundClip;
        musicSource.Play();
    }*/

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void TurnOff()
    {
        isMuted = true;

        //this.enabled = false;
        sfxSource.volume = 0f;
        musicSource.volume = 0f;
    }

    public void TurnOn()
    {
        isMuted = false;

        //this.enabled = true;
        sfxSource.volume = 0.7f;
        musicSource.volume = 0.5f;
    }
}