using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Skirym_DRM : MonoBehaviour
{
    [SerializeField] private GameObject skirymGameRoot;

    private const string ClickerUiRootName = "SkirymClickerUI";
    private int gold = 0;
    private TMP_Text goldText = null;

    public void Showbuypanel()
    {
        DRMManager.ShowBuyGamePanel();
    }

    public void CheckOwnership()
    {
        UserScriptableData currentUser = UserManager.GetCurrentUser();
        if (currentUser == null)
        {
            DRMManager.ShowUnauthorizePanel();
            return;
        }

        if (currentUser.HasGameKey(UserScriptableData.GameIdSkirym) == false)
        {
            DRMManager.ShowBuyGamePanel();
            return;
        }

        if (skirymGameRoot == null)
        {
            Debug.LogWarning($"{nameof(Skirym_DRM)}: Missing reference to skirymGameRoot; cannot launch Skirym.");
            return;
        }

        skirymGameRoot.SetActive(true);
        EnsureClickerUI();
    }

    private void EnsureClickerUI()
    {
        if (skirymGameRoot == null) return;

        Transform existing = skirymGameRoot.transform.Find(ClickerUiRootName);
        if (existing == null)
        {
            CreateClickerUI();
        }

        gold = 0;
        UpdateGoldText();
    }

    private void CreateClickerUI()
    {
        // Root
        GameObject root = new GameObject(ClickerUiRootName, typeof(RectTransform));
        root.transform.SetParent(skirymGameRoot.transform, false);

        RectTransform rootRt = root.GetComponent<RectTransform>();
        rootRt.anchorMin = new Vector2(0f, 1f);
        rootRt.anchorMax = new Vector2(0f, 1f);
        rootRt.pivot = new Vector2(0f, 1f);
        rootRt.anchoredPosition = new Vector2(40f, -40f);
        rootRt.sizeDelta = new Vector2(420f, 200f);

        // Gold text
        GameObject goldTextGo = new GameObject("GoldText", typeof(RectTransform));
        goldTextGo.transform.SetParent(root.transform, false);

        RectTransform goldRt = goldTextGo.GetComponent<RectTransform>();
        goldRt.anchorMin = new Vector2(0f, 1f);
        goldRt.anchorMax = new Vector2(0f, 1f);
        goldRt.pivot = new Vector2(0f, 1f);
        goldRt.anchoredPosition = new Vector2(0f, 0f);
        goldRt.sizeDelta = new Vector2(420f, 70f);

        goldText = goldTextGo.AddComponent<TextMeshProUGUI>();
        goldText.fontSize = 48;
        goldText.alignment = TextAlignmentOptions.Left;
        goldText.color = new Color(1f, 0.85f, 0.2f, 1f);

        // Button
        GameObject buttonGo = new GameObject("GainGoldButton", typeof(RectTransform));
        buttonGo.transform.SetParent(root.transform, false);

        RectTransform btnRt = buttonGo.GetComponent<RectTransform>();
        btnRt.anchorMin = new Vector2(0f, 1f);
        btnRt.anchorMax = new Vector2(0f, 1f);
        btnRt.pivot = new Vector2(0f, 1f);
        btnRt.anchoredPosition = new Vector2(0f, -90f);
        btnRt.sizeDelta = new Vector2(320f, 80f);

        Image btnImage = buttonGo.AddComponent<Image>();
        btnImage.color = new Color(0.15f, 0.15f, 0.15f, 0.85f);

        Button btn = buttonGo.AddComponent<Button>();
        btn.onClick.AddListener(AddGold);

        // Button label
        GameObject labelGo = new GameObject("Label", typeof(RectTransform));
        labelGo.transform.SetParent(buttonGo.transform, false);

        RectTransform labelRt = labelGo.GetComponent<RectTransform>();
        labelRt.anchorMin = new Vector2(0f, 0f);
        labelRt.anchorMax = new Vector2(1f, 1f);
        labelRt.offsetMin = Vector2.zero;
        labelRt.offsetMax = Vector2.zero;

        TMP_Text label = labelGo.AddComponent<TextMeshProUGUI>();
        label.text = "Get 1 gold";
        label.fontSize = 36;
        label.alignment = TextAlignmentOptions.Center;
        label.color = Color.white;
        label.raycastTarget = false;
    }

    private void AddGold()
    {
        gold++;
        UpdateGoldText();
    }

    private void UpdateGoldText()
    {
        if (goldText == null)
        {
            Transform existing = skirymGameRoot != null ? skirymGameRoot.transform.Find($"{ClickerUiRootName}/GoldText") : null;
            if (existing != null) goldText = existing.GetComponent<TMP_Text>();
        }

        if (goldText != null)
        {
            goldText.text = $"Gold: {gold}";
        }
    }
}
