using System.Collections;
using System.Collections.Generic;
using Monopoly.Lobby_v2;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class WaitingScreen : NetworkBehaviour
{
    private GameObject networkManagerGO;
    private LobbyManager lobbyManager;

    private GameManager GameManagerScript;

    private int playerCount;

    [SerializeField]
    private Text[] playerNames;

    // Use this for initialization
    private void Start()
    {
        GameManagerScript = gameObject.GetComponent<GameManager>();

        networkManagerGO = GameObject.Find("Network Manager");
        lobbyManager = networkManagerGO.GetComponent<LobbyManager>();
    }

    public void OnServerConnect(NetworkConnection _connection)
    {
        Debug.Log("Player Joined");
        playerNames[playerCount].text = PlayerPrefs.GetString("PlayerName");

        playerCount++;

        //if (_connection.hostId >= 0)
        //{
        //    Debug.Log("New Player has joined");
        //}
    }

    // ToDo: Start when all are joined
    private IEnumerator StartGM()
    {
        const float delay = 5f;
        yield return new WaitForSeconds(delay);

        // Gameplay start point
        GameManagerScript.enabled = true;
    }
}
