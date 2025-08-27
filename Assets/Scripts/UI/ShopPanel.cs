using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : Panel
{
    [SerializeField] private GameObject _characterPanel;
    [SerializeField] private GameObject _mapPanel;
    [SerializeField] private Image _characterIcon;
    [SerializeField] private Image _mapIcon;

    private void Start()
    {
        _characterPanel.SetActive(true);
        _mapPanel.SetActive(false);
    }
    private void OnEnable()
    {
        _characterPanel.SetActive(true);
        _mapPanel.SetActive(false);
    }
    public void OnClickCharacterButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        _characterIcon.color = new Color(200f/255f, 200f/255f, 200f/255f);
        _mapIcon.color = new Color(1, 1, 1);
        _characterPanel.SetActive(true);
        _mapPanel.SetActive(false);
    }

    public void OnClickMapButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        _mapIcon.color = new Color(200f/255f, 200f/255f, 200f/255f);
        _characterIcon.color = new Color(1, 1, 1);
        _characterPanel.SetActive(false);
        _mapPanel.SetActive(true);
    }

    public void OnClickCloseButton()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
        PanelManager.Instance.ClosePanel(this.name);
    }
}