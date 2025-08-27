using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class SettingPanel : Panel
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
    public void OnClickCreditButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        GameManager.Instance.Credit();
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
    public void OnClickCloseButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        PanelManager.Instance.ClosePanel(this.name);
    }
}
