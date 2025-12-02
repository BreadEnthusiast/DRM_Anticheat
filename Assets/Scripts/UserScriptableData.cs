using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UserScriptableData", menuName = "ScriptableObjects/UserScriptableData")]
public class UserScriptableData : ScriptableObject
{
    [SerializeField] private string userName = String.Empty;
    [SerializeField] private string passwordName = String.Empty;
    [SerializeField] private Sprite userIcon = null;
    
    public string GetUserName() => userName;
    public string GetUserPassword() => passwordName;
    public Sprite GetUserIcon() => userIcon;
   
    public bool AreCredentialValid(string _userName, string _passwordName) => _userName.Equals(userName) && _passwordName.Equals(passwordName);

}
