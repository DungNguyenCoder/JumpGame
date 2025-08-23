using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : Panel
{
    public void OnClickCreditButton()
    {
        GameManager.Instance.Credit();
    }
    public void OnClickMuteButton()
    {
        AudioManager.Instance.MuteAll();
    }
    public void OnClickUnMuteButton()
    {
        AudioManager.Instance.UnMuteAll();
    }
    public void OnClickCloseButton()
    {
        PanelManager.Instance.ClosePanel(this.name);
    }
}
