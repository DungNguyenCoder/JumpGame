using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : Panel
{
    [SerializeField] private GameObject _characterPanel;
    [SerializeField] private GameObject _mapPanel;

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
        Debug.Log("Open character panel");
        _characterPanel.SetActive(true);
        _mapPanel.SetActive(false);
    }

    public void OnClickMapButton()
    {
        _characterPanel.SetActive(false);
        _mapPanel.SetActive(true);
    }

    public void OnClickCloseButton()
    {
        PanelManager.Instance.ClosePanel(this.name);
    }
}