using System.Collections;
using System.Collections.Generic;
using Monopoly.Lobby_v2;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WaitingScreen : NetworkBehaviour
{
    private GameObject networkManagerGO;

    [SerializeField]
    private GameObject GameManagerGo;

    private int playerCount;

    [SerializeField]
    private Text[] playerNames;

    // Use this for initialization
    private void Start()
    {
        GameManagerGo.SetActive(false);
        networkManagerGO = GameObject.Find("Network Manager");

        //if (!isLocalPlayer)
        //{
        //    CmdInitPlayerNames();
        //}
    }

    public void OnServerConnect(NetworkConnection _connection)
    {
        Debug.Log("Player J!");
    }

    private void Update()
    {
        if (NetworkServer.active)
        {
            RpcInitPlayerNames();
        }
    }

    // ToDo: Start when all are joined
    private IEnumerator StartGM()
    {
        const float delay = 5f;

        yield return new WaitForSeconds(delay);

        // Gameplay start point, and disable itself
        GameManagerGo.SetActive(true);
        gameObject.SetActive(false);
    }

    [ClientRpc]
    private void RpcInitPlayerNames()
    {
        Debug.Log(Network.connections.Length);
        playerNames[Network.connections.Length].text = PlayerPrefs.GetString("PlayerName");
    }
}
