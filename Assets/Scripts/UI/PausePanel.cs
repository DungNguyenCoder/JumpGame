using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
