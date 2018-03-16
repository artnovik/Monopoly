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

    private const uint roomSizeTotal = 3; //6
    private const uint roomSizeClients = roomSizeTotal - 1; //5

    // Use this for initialization
    private void Start()
    {
        //Show FPS only on Android, if Dev Build
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
        while (NetworkServer.connections.Count < roomSizeTotal+1)
        {
            RpcShowMessageOnAll(NetworkServer.connections.Count-1);

            if (NetworkServer.connections.Count - 1 > 0)
            {
                ShowStartBoard();
            }

            yield return new WaitForSeconds(1f);
            Debug.Log("Connected:" + NetworkServer.connections.Count + ". Max: " + roomSizeClients);
        }

        Debug.Log("1 Server + " + (NetworkServer.connections.Count - 1) + " players!");
    }

    [ClientRpc]
    private void RpcShowMessageOnAll(int conCount)
    {
        readyText_GO.SetActive(true);
        readyTextComponent.text = conCount + "/" + roomSizeClients;
    }

    [ClientRpc]
    private void RpcClearMessageOnAll()
    {
        readyText_GO.SetActive(false);
        readyTextComponent.text = string.Empty;
    }

    [Server]
    private void ShowStartBoard()
    {
        if (!startBoardUI_GO.activeSelf)
        {
            startBoardUI_GO.SetActive(true);
        }
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
}
