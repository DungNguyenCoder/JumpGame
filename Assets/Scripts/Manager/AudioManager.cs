using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    private bool _isMuted = false;

    public AudioClip jump;
    public AudioClip crash;
    public AudioClip gameOver;

    public void PlaySFX(AudioClip audioClip)
    {
        if (!_isMuted)
            SFXSource.PlayOneShot(audioClip);
    }
    public void PlayMusic()
    {
        if (!_isMuted) musicSource.Play();
    }
    public void PauseMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }
    public void ContinueMusic()
    {
        if (musicSource != null)
        {
            musicSource.UnPause();
        }
    }
    public void MuteAll()
    {
        _isMuted = true;

        if (musicSource) musicSource.mute = true;
        if (SFXSource) SFXSource.mute = true;
        Debug.Log("Mute");
    }
    public void UnMuteAll()
    {
        _isMuted = false;

        if (musicSource) musicSource.mute = false;
        if (SFXSource) SFXSource.mute = false;
        Debug.Log("UnMute");
    }

    public void PlayMusicFromStart()
    {
        if (!_isMuted && musicSource != null)
        {
            musicSource.Stop();
            musicSource.time = 0f;
            musicSource.Play();
        }
    }
}
