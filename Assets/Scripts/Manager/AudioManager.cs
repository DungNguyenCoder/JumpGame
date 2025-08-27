using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;

    public bool _isMutedMusic = false;
    public bool _isMutedSFX = false;

    public AudioClip jump;
    public AudioClip crash;
    public AudioClip gameOver;
    public AudioClip click;

    public void PlaySFX(AudioClip audioClip)
    {
        if (!_isMutedSFX)
            SFXSource.PlayOneShot(audioClip);
    }
    public void PlayMusic()
    {
        if (!_isMutedMusic) musicSource.Play();
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
    public void ToggleMusic()
    {
        _isMutedMusic = !_isMutedMusic;
        if (musicSource) musicSource.mute = _isMutedMusic;
    }
    public void ToggleSFX()
    {
        _isMutedSFX = !_isMutedSFX;
        if (SFXSource) SFXSource.mute = _isMutedSFX;
    }

    public void PlayMusicFromStart()
    {
        if (!_isMutedMusic && musicSource != null)
        {
            musicSource.Stop();
            musicSource.time = 0f;
            musicSource.Play();
        }
    }
}
