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

    public override void Awake()
    {
        base.Awake();
        if (musicSource) musicSource.playOnAwake = false;
        if (SFXSource) SFXSource.playOnAwake = false;
        LoadAudioStatus();
        ApplyInitialAudioStatus();
    }
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

        if (_isMutedMusic)
        {
            if (musicSource && musicSource.isPlaying) musicSource.Pause();
        }
        else
        {
            if (musicSource)
            {
                if (musicSource.clip != null)
                {
                    musicSource.UnPause();
                    if (!musicSource.isPlaying) musicSource.Play();
                }
            }
        }
        SaveAudioStatus();
    }

    public void ToggleSFX()
    {
        _isMutedSFX = !_isMutedSFX;
        if (SFXSource) SFXSource.mute = _isMutedSFX;
        SaveAudioStatus();
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

    private void SaveAudioStatus()
    {
        PlayerPrefs.SetInt(GameConfig.MUSIC_STATUS, _isMutedMusic == true ? 1 : 0);
        PlayerPrefs.SetInt(GameConfig.SFX_STATUS, _isMutedSFX == true ? 1 : 0);
        PlayerPrefs.Save();
    }
    private void LoadAudioStatus()
    {
        _isMutedMusic = PlayerPrefs.GetInt(GameConfig.MUSIC_STATUS) == 1 ? true : false;
        _isMutedSFX = PlayerPrefs.GetInt(GameConfig.SFX_STATUS) == 1 ? true : false;
    }

    private void ApplyInitialAudioStatus()
    {
        if (SFXSource) SFXSource.mute = _isMutedSFX;

        if (musicSource == null) return;

        if (_isMutedMusic)
        {
            musicSource.Stop();
        }
        else
        {
            if (!musicSource.isPlaying) musicSource.Play();
        }
    }
}
