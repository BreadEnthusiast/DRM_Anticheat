using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject buyGamePanel;
    [SerializeField] private GameObject storePanel;
    [SerializeField] private GameObject skirymBuyButton;

    public void OpenStore()
    {
        storePanel.SetActive(true);
        buyGamePanel.SetActive(false);
    }

    public void CloseStore()
    {
        storePanel.SetActive(false);
    }

    public void BuySkirym()
    {

    }
}
