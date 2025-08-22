using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public bool destroyOnClose = true;
    public string PanelName => name;
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }

    public virtual void Close()
    {
        gameObject.SetActive(false);
        if (destroyOnClose)
        {
            PanelManager.Instance.Unregister(PanelName);
            Destroy(gameObject);
        }
    }
}
