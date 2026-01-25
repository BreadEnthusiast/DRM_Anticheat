using System.Collections.Generic;

/// <summary>
/// Extremely simple runtime-only "database" of issued game keys.
/// Not persistent and not secure; meant only for DRM simulation.
/// </summary>
public static class DRMKeyDatabase
{
    // gameId -> issued keys
    private static readonly Dictionary<string, HashSet<int>> IssuedKeysByGame = new Dictionary<string, HashSet<int>>();

    public static void RegisterKey(string gameId, int key)
    {
        if (string.IsNullOrWhiteSpace(gameId)) return;
        if (key == 0) return;

        if (IssuedKeysByGame.TryGetValue(gameId, out HashSet<int> keys) == false)
        {
            keys = new HashSet<int>();
            IssuedKeysByGame[gameId] = keys;
        }

        keys.Add(key);
    }

    public static bool IsKeyValid(string gameId, int key)
    {
        if (string.IsNullOrWhiteSpace(gameId)) return false;
        if (key == 0) return false;

        return IssuedKeysByGame.TryGetValue(gameId, out HashSet<int> keys) && keys.Contains(key);
    }
}

