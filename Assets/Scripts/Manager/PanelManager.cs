using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : Singleton<PanelManager>
{
    public Dictionary<string, Panel> panels = new();

    private void Start()
    {
        var list = GetComponentsInChildren<Panel>();
        foreach (var panel in list)
        {
            panels[panel.name] = panel;
        }
    }
    public Panel GetPanel(string panelName)
    {
        if (IsExisted(panelName))
        {
            return panels[panelName];
        }
        Panel panel = Resources.Load<Panel>("Panel/" + panelName);
        // Debug.Log(panel.name);
        Panel newPanel = Instantiate(panel, this.transform);
        newPanel.name = panelName;
        newPanel.gameObject.SetActive(false);

        panels[panelName] = newPanel;
        return newPanel;
    }

    public void RemovePanel(string name)
    {
        if (IsExisted(name))
        {
            panels.Remove(name);
        }
    }

    public void OpenPanel(string name)
    {
        Panel panel = GetPanel(name);
        panel.Open();
    }

    public void ClosePanel(string name)
    {
        Panel panel = GetPanel(name);
        panel.Close();
    }

    public void CloseAllPanels()
    {
        foreach (var panel in panels.Values)
        {
            panel.Close();
        }
    }

    private bool IsExisted(string name)
    {
        return panels.ContainsKey(name);
    }

    public void Unregister(string name)
    {
        panels.Remove(name);
    }
}