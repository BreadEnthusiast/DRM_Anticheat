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

        if (allUsers.Length == 0)
        {
            Debug.LogWarning($"{nameof(UserManager)}: No users found in Resources/Users. " +
                             $"If you intend to use Resources.LoadAll, place user assets under Assets/Resources/Users/. " +
                             $"(Inspector-assigned default user will still be logged in.)");
        }

        if (userToLogInOnStart == null) return;
        LogInUser(userToLogInOnStart);
    }

    public static void LogInUser(UserScriptableData user)
    {
        if (user == null) return;
        currentUser = user;
        OnLogIn?.Invoke(user);
    }

    public static bool TryToLogInUser(string _userName, string _passwordName)
    {
        for (int i = 0; i < allUsers.Length; i++)
        {
            if (allUsers[i].AreCredentialValid(_userName, _passwordName) == false)
            {
                continue;
            }

            LogInUser(allUsers[i]);
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