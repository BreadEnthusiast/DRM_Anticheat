using System;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class UserManager : MonoBehaviour
{
    public static UnityAction<UserScriptableData> OnLogIn;
    public static UnityAction<UserScriptableData> OnLogOut;

    private static UserScriptableData currentUser = null;
    private static UserScriptableData[] allUsers = Array.Empty<UserScriptableData>();

    [SerializeField] private UserScriptableData userToLogInOnStart = null;

    private void Start()
    {
        allUsers = Resources.LoadAll("Users", typeof(UserScriptableData)).Cast<UserScriptableData>().ToArray();

        if (userToLogInOnStart == null) return;
        TryToLogInUser(userToLogInOnStart.GetUserName(), userToLogInOnStart.GetUserPassword());
    }

    public static bool TryToLogInUser(string _userName, string _passwordName)
    {
        for (int i = 0; i < allUsers.Length; i++)
        {
            if (allUsers[i].AreCredentialValid(_userName, _passwordName) == false)
            {
                continue;
            }

            if (currentUser == null)
            {
                currentUser = allUsers[i];
            }

            OnLogIn?.Invoke(allUsers[i]);
            return true;
        }

        return false;
    }

    public static void TryToLogOutCurrentUser()
    {
        if (currentUser == null) return;
        
        UserScriptableData _cachedUser = currentUser;
        currentUser = null;
        OnLogOut?.Invoke(_cachedUser);
    }

    public static UserScriptableData GetCurrentUser() => currentUser;
    public static Sprite GetCurrentUserIcon() => currentUser == null ? null : currentUser.GetUserIcon();
}