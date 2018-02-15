using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    private Behaviour[] componentsToDisable;

    private string _ID;

    private void Start()
    {
        if (!isLocalPlayer || NetworkServer.active)
        {
            DisableComponents();
        }
        else
        {

        }
        RegisterPlayer();
    }

    private void RegisterPlayer()
    {
        _ID = "Player " + PlayerPrefs.GetString("PlayerName");
        transform.name = _ID;
    }

    private void DisableComponents()
    {
        for (int i = 0; i < componentsToDisable.Length; i++)
        {
            componentsToDisable[i].enabled = false;
        }
    }
}
