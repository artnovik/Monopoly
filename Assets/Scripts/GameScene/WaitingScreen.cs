using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WaitingScreen : NetworkBehaviour
{
    [SerializeField]
    private GameObject GameManagerGo;

    [SerializeField] private GameObject fpsTextGO;

    [SerializeField] private GameObject waitingScreenUI_GO;
    [SerializeField] private GameObject startBoardUI_GO;
    [SerializeField] private GameObject countdownText_GO;

    // Use this for initialization
    private void Start()
    {
        //Show FPS only on Android
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    fpsTextGO.SetActive(true);
        //}

        waitingScreenUI_GO.SetActive(true);
        startBoardUI_GO.SetActive(false);

        // ToDo Move this to "When all joined"
        ShowStartBoard();

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

    private void ShowStartBoard()
    {
        startBoardUI_GO.SetActive(true);
    }

    public void StartGame()
    {
        // ToDo: When all clicked
        startBoardUI_GO.SetActive(false);
        countdownText_GO.SetActive(true);

        StartCoroutine(StartGM());
    }

    // ToDo: Start when all are joined
    private IEnumerator StartGM()
    {
        int timer = 5;

        while (timer > 0)
        {
            countdownText_GO.GetComponent<Text>().text = timer.ToString();
            yield return new WaitForSeconds(1);
            --timer;
        }

        // Gameplay start point. Disabling itself - don't need anymore
        waitingScreenUI_GO.SetActive(false);
        GameManagerGo.SetActive(true);
        GameManagerGo.GetComponent<GameManager>().StartGame();
        gameObject.SetActive(false);
    }
}
