using System;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "UserScriptableData", menuName = "ScriptableObjects/UserScriptableData")]
public class UserScriptableData : ScriptableObject
{
    [SerializeField] private string userName = String.Empty;
    [SerializeField] private string passwordName = String.Empty;
    [SerializeField] private Sprite userIcon = null;

    public string game;
    public int key;
    public string GetUserName() => userName;
    public string GetUserPassword() => passwordName;
    public Sprite GetUserIcon() => userIcon;

    public Dictionary<string, int> OwnedGames = new Dictionary<string, int>();

    public bool AreCredentialValid(string _userName, string _passwordName) => _userName.Equals(userName) && _passwordName.Equals(passwordName);

    public void AddGame()
    {

    }

    public void GenerateKey()
    {

    }
}
