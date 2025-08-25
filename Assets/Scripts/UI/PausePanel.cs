using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanel : Panel
{
    public void OnClickContinueButton()
    {
        GameManager.Instance.ContinueGame();
    }
    public void OnClickMuteButton()
    {
        AudioManager.Instance.MuteAll();
    }
    public void OnClickUnMuteButton()
    {
        AudioManager.Instance.UnMuteAll();
    }

    public void OnClickMainMenuButton()
    {
        AudioManager.Instance.PlayMusicFromStart();
        SceneManager.LoadScene("MainMenu");
    }
}
