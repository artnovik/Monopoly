using System.Collections;
using System.Collections.Generic;
using Monopoly.Lobby_v2;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WaitingScreen : NetworkBehaviour
{
    [SerializeField]
    private GameObject GameManagerGo;

    [SerializeField] private GameObject waitingScreenUI_GO;
    [SerializeField] private GameObject readyText_GO;

    [SerializeField]
    private Text[] playerNames;

    // Use this for initialization
    private void Start()
    {
        waitingScreenUI_GO.SetActive(true);
        GameManagerGo.SetActive(false);

        StartCoroutine(StartGM());

        //if (!isLocalPlayer)
        //{
        //    InitPlayerNames();
        //}
    }

    // ToDo Networking functionality [1]

    /*public void OnServerConnect(NetworkConnection _connection)
    {
        Debug.Log("Player J!");
    }*/

    // ToDo Networking functionality [2]

    /*private void Update()
    {
        if (NetworkServer.active)
        {
            InitPlayerNames();
        }
    }*/

    // ToDo: Start when all are joined
    private IEnumerator StartGM()
    {
        const float delay = 5f;

        readyText_GO.SetActive(true);

        yield return new WaitForSeconds(delay);

        // Gameplay start point. Disabling itself - don't need anymore
        waitingScreenUI_GO.SetActive(false);
        GameManagerGo.SetActive(true);
        gameObject.SetActive(false);
    }


    // ToDo Networking functionality [3]. Initialize Player Names into UI

    //[ClientRpc]
    private void InitPlayerNames()
    {
        //Debug.Log(Network.connections.Length);
        playerNames[Network.connections.Length].text = PlayerPrefs.GetString("PlayerName");
    }
}
