using System;
using UnityEngine;

public class DRMManager : MonoBehaviour
{
    private static DRMManager instance = null;

    [SerializeField] private GameObject buyGamePanel = null;
    [SerializeField] private GameObject unauthorizePanel = null;
    [SerializeField] private GameObject saveModifiedPanel = null;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void showBuyGamePanel()
    {
        unauthorizePanel.SetActive(false);
        buyGamePanel.SetActive(true);
        saveModifiedPanel.SetActive(false);
    }

    private void showUnauthorizePanel()
    {
        unauthorizePanel.SetActive(true);
        buyGamePanel.SetActive(false);
        saveModifiedPanel.SetActive(false);
    }
    
    private void showSaveModifiedPanel()
    {
        unauthorizePanel.SetActive(false);
        buyGamePanel.SetActive(false);
        saveModifiedPanel.SetActive(true);
    }

    public static void ShowBuyGamePanel()
    {
        if (instance != null)
        {
            instance.showBuyGamePanel();
        }
    }

    public static void ShowUnauthorizePanel()
    {
        if (instance != null)
        {
            instance.showUnauthorizePanel();
        }
    }
    
    public static void ShowSaveModifiedPanel()
    {
        if (instance != null)
        {
            instance.showSaveModifiedPanel();
        }
    }
}