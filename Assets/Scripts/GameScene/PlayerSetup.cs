using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] componentsToDisable;

    [SerializeField]
    private PlayerInfo playerInfo;

    private string _ID;

    private void Start()
    {
        if (!isLocalPlayer /*|| NetworkServer.active*/)
        {
            // To make sure every Player is unique
            DisableComponents();
        }
        else
        {

        }

        RegisterPlayer();
    }

    /// <summary>
    /// Setting up Player Name
    /// </summary>
    private void RegisterPlayer()
    {
        _ID = "Player " + PlayerPrefs.GetString("PlayerName");
        if (playerInfo != null)
        {
            playerInfo.SetPlayerName(_ID);
        }

    }

    /// <summary>
    /// Disabling Components attached to Player Instance (Networking)
    /// </summary>
    private void DisableComponents()
    {
        for (uint i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }
}
