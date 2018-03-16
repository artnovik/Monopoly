using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingScreen : NetworkBehaviour
{
    [SerializeField]
    private GameObject GameManagerGo;

    [SerializeField] private GameObject fpsTextGO;

    [SerializeField] private GameObject waitingScreenUI_GO;
    [SerializeField] private GameObject startBoardUI_GO;
    [SerializeField] private GameObject readyText_GO;
    private Text readyTextComponent;

    private const uint roomSize = 3; //6

    [SerializeField]
    private Text[] playerNames;

    // Use this for initialization
    private void Start()
    {
        //Show FPS only on Android
        if (Application.platform == RuntimePlatform.Android && Debug.isDebugBuild)
        {
            fpsTextGO.SetActive(true);
        }

        waitingScreenUI_GO.SetActive(true);
        GameManagerGo.SetActive(false);
        startBoardUI_GO.SetActive(false);
        readyTextComponent = readyText_GO.GetComponent<Text>();

        if (NetworkServer.active)
        {
            StartCoroutine(CheckIfAllJoined());
        }
    }

    [Server]
    private IEnumerator CheckIfAllJoined()
    {
        readyText_GO.SetActive(true);

        while (NetworkServer.connections.Count < roomSize)
        {
            RpcShowMessageOnClients();
            var playersDifference = roomSize - NetworkServer.connections.Count;
            if (playersDifference == 1)
            {
                readyTextComponent.text = "Waiting for " + playersDifference + " Player...";
            }
            else
            {
                readyTextComponent.text = "Waiting for " + playersDifference + " Players...";
            }

            yield return new WaitForSeconds(1f);
            Debug.Log("Connected:" + NetworkServer.connections.Count + ". Max: " + roomSize);
        }

        // When all joined
        RpcClearMessageOnAll();

        ShowStartBoard();
        Debug.Log("1 Server + " + (roomSize - 1) + " players!");
    }

    [ClientRpc]
    private void RpcShowMessageOnClients()
    {
        if (!NetworkServer.active)
        {
            readyText_GO.SetActive(true);
            readyTextComponent.text = "We are client";
        }
    }

    [ClientRpc]
    private void RpcClearMessageOnAll()
    {
        readyText_GO.SetActive(false);
        readyTextComponent.text = string.Empty;
    }

    private void ShowStartBoard()
    {
        startBoardUI_GO.SetActive(true);
    }

    public void StartGame()
    {
        startBoardUI_GO.SetActive(false);
        readyText_GO.SetActive(true);

        StartCoroutine(StartGM());
    }

    private IEnumerator StartGM()
    {
        int timer = 5;

        while (timer > 0)
        {
            readyText_GO.GetComponent<Text>().text = /*"Game starts in: " +*/ timer.ToString();
            yield return new WaitForSeconds(1);
            --timer;
        }

        // Gameplay start point. Disabling itself - don't need anymore
        waitingScreenUI_GO.SetActive(false);
        GameManagerGo.SetActive(true);
        gameObject.SetActive(false);
    }


    // ToDo Networking functionality [3]. Put Player Names into UI
    //[ClientRpc]
    private void InitPlayerNames()
    {
        //Debug.Log(Network.connections.Length);
        playerNames[Network.connections.Length].text = PlayerPrefs.GetString("PlayerName");
    }
}
