using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : Panel
{
    [SerializeField] private GameObject muteMusic;
    [SerializeField] private GameObject unMuteMusic;
    [SerializeField] private GameObject muteSFX;
    [SerializeField] private GameObject unMuteSFX;

    private void OnEnable()
    {
        muteMusic.SetActive(AudioManager.Instance._isMutedMusic);
        unMuteMusic.SetActive(!AudioManager.Instance._isMutedMusic);
        muteSFX.SetActive(AudioManager.Instance._isMutedSFX);
        unMuteSFX.SetActive(!AudioManager.Instance._isMutedSFX);
    }
    public void OnClickContinueButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        GameManager.Instance.ContinueGame();
    }
    public void OnClickToggleMusicButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        AudioManager.Instance.ToggleMusic();
        muteMusic.SetActive(AudioManager.Instance._isMutedMusic);
        unMuteMusic.SetActive(!AudioManager.Instance._isMutedMusic);
    }
    public void OnClickToggleSFXButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        AudioManager.Instance.ToggleSFX();
        muteSFX.SetActive(AudioManager.Instance._isMutedSFX);
        unMuteSFX.SetActive(!AudioManager.Instance._isMutedSFX);
    }

    public void OnClickMainMenuButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        AudioManager.Instance.PlayMusicFromStart();
        SceneManager.LoadScene("MainMenu");
    }
}
