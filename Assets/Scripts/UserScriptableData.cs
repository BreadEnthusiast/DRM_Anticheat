using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UserScriptableData", menuName = "ScriptableObjects/UserScriptableData")]
public class UserScriptableData : ScriptableObject
{
    [SerializeField] private string userName = String.Empty;
    [SerializeField] private string passwordName = String.Empty;
    [SerializeField] private Sprite userIcon = null;

    // Game IDs (keep consistent across UI/scripts).
    public const string GameIdSkirym = "Skirym";

    public string GetUserName() => userName;
    public string GetUserPassword() => passwordName;
    public Sprite GetUserIcon() => userIcon;

    // Runtime-only ownership (Dictionary isn't Unity-serializable; that's fine for this simulation).
    public Dictionary<string, int> OwnedGames = new Dictionary<string, int>();

    private void OnEnable()
    {
        OwnedGames ??= new Dictionary<string, int>();
    }

    public bool HasGameKey(string gameId)
    {
        if (string.IsNullOrWhiteSpace(gameId) || OwnedGames == null) return false;
        return OwnedGames.ContainsKey(gameId);
    }

    public int GetGameKey(string gameId)
    {
        if (string.IsNullOrWhiteSpace(gameId) || OwnedGames == null) return 0;
        return OwnedGames.TryGetValue(gameId, out int key) ? key : 0;
    }

    public void AddOrReplaceGameKey(string gameId, int key)
    {
        if (string.IsNullOrWhiteSpace(gameId)) return;
        OwnedGames ??= new Dictionary<string, int>();
        OwnedGames[gameId] = key;
    }

    public bool AreCredentialValid(string _userName, string _passwordName) => _userName.Equals(userName) && _passwordName.Equals(passwordName);

}
