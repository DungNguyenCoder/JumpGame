using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    public AudioClip jump;
    public AudioClip crash;
    public AudioClip gameOver;

    public void PlaySFX(AudioClip audioClip)
    {
        SFXSource.PlayOneShot(audioClip);
    }
    public void StopMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }
    public void ResumeMusic()
    {
        if (musicSource != null)
        {
            musicSource.UnPause();
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
