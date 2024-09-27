using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbySceneLoadCount : MonoBehaviour
{
    public static LobbySceneLoadCount instance;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    int lobbySceneCount = 0;

    public void LobbySceneIsLoaded()
    {
        lobbySceneCount++;
    }

    public bool IsLobbySceneLoaded()
    {
        if (lobbySceneCount > 1)
            return true;
        else
            return false;
    }
}
