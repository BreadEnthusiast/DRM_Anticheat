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
        RefreshUI();
    }

    public void CloseStore()
    {
        storePanel.SetActive(false);
    }

    public void BuySkirym()
    {
        UserScriptableData currentUser = UserManager.GetCurrentUser();
        if (currentUser == null)
        {
            DRMManager.ShowUnauthorizePanel();
            return;
        }

        int generatedKey = Random.Range(100000, 1000000); // 6-digit key
        currentUser.AddOrReplaceGameKey(UserScriptableData.GameIdSkirym, generatedKey);

        RefreshUI();
        CloseStore();
    }

    private void RefreshUI()
    {
        if (skirymBuyButton == null) return;

        UserScriptableData currentUser = UserManager.GetCurrentUser();
        bool hasKey = currentUser != null && currentUser.HasGameKey(UserScriptableData.GameIdSkirym);
        skirymBuyButton.SetActive(hasKey == false);
    }
}
