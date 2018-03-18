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
    [SerializeField] private GameObject countdownText_GO;
    private Text readyTextComponent;
    private Text countdownTextComponent;

    private bool started;

    private const uint roomSizeTotal = 3; //6
    private const uint roomSizeClients = roomSizeTotal - 1; //5

    // Use this for initialization
    private void Start()
    {
        //Show FPS only on Android, if Dev Build
        //if (Application.platform == RuntimePlatform.Android && Debug.isDebugBuild)
        //{
        //    fpsTextGO.SetActive(true);
        //}

        waitingScreenUI_GO.SetActive(true);
        startBoardUI_GO.SetActive(false);
        readyTextComponent = readyText_GO.GetComponent<Text>();
        countdownTextComponent = countdownText_GO.GetComponent<Text>();

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
            if (started)
            {
                yield break;
            }

            RpcShowMessageOnAll(NetworkServer.connections.Count-1);

            if (NetworkServer.connections.Count - 1 > 0)
            {
                ShowStartBoard();
            }

            yield return new WaitForSeconds(0.5f);
        }
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
        RpcClearMessageOnAll();

        if (NetworkServer.active)
        {
            StartCoroutine(StartGM());
        }
    }

    [Server]
    private IEnumerator StartGM()
    {
        started = true;
        StopCoroutine(CheckIfAllJoined());

        uint timer = 5;
        while (timer > 0)
        {
            RpcCountdown(timer, true);
            yield return new WaitForSeconds(1);
            --timer;
        }

        // Gameplay start point.
        RpcCountdown(timer, false);
        GameManagerGo.GetComponent<GameManager>().StartGame();
        RpcDisableWs();
    }

    [ClientRpc]
    private void RpcCountdown(uint timer, bool value)
    {
        countdownText_GO.SetActive(value);
        countdownTextComponent.text = timer.ToString();
    }

    [ClientRpc]
    private void RpcDisableWs()
    {
        waitingScreenUI_GO.SetActive(false);
        gameObject.SetActive(false);
    }
}
