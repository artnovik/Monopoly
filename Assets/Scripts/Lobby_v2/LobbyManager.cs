using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

namespace Monopoly.Lobby_v2
{
    public class LobbyManager : MonoBehaviour
    {
        private List<GameObject> roomList = new List<GameObject>();

        [SerializeField]
        private InputField inputFieldName;

        [SerializeField]
        private GameObject buttonConnect;

        [SerializeField]
        private GameObject buttonCreate;

        [SerializeField]
        private Transform buttonConnectParent;

        [HideInInspector]
        public Text status;

        [SerializeField]
        private uint roomSize = 6;

        [SerializeField]
        private string roomName = "Monopoly";

        [HideInInspector]
        public string playerName;

        private bool InternetOn;
        private bool roomsExisting;

        private NetworkManager networkManager;
        private static MatchInfoSnapshot match;

        public Color32 colorProcess = new Color32(225, 216, 98, 255);
        public Color32 colorSuccess = new Color32(98, 225, 114, 255);
        public Color32 colorError = new Color32(225, 98, 98, 255);

        private void Start()
        {
            networkManager = NetworkManager.singleton;

            // Matchmaking enabling
            if (networkManager.matchMaker == null)
            {
                networkManager.StartMatchMaker();
            }

            InvokeRepeating("GetRooms", 0f, 0.5f);
        }

        // "Enter" Input Callback
        //private void Update()
        //{
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        if ((GameObject.FindGameObjectWithTag("Connect") == null) && buttonCreate.GetComponent<Button>().interactable)
        //        {
        //            // If no rooms found - create one
        //            ButtonStartOnClick();
        //        }
        //        else
        //        {
        //            // If room exists, connect
        //            buttonConnect.GetComponent<Connect>().JoinRoom();
        //        }
        //    }
        //}

        public void ButtonStartOnClick()
        {
            string inputFieldNameText = inputFieldName.GetComponent<InputField>().text;

            // Checking PlayerName Input
            if (string.IsNullOrEmpty(inputFieldNameText))
            {
                status.color = colorError;
                status.text = "Name can't be empty!";

                return;
            }

            playerName = inputFieldNameText;
            PlayerPrefs.SetString("PlayerName", playerName);

            if (InternetOn && !roomsExisting)
            {
                CreateRoom();
                CancelInvoke("GetRooms");
            }
            else
            {
                status.color = colorError;
                status.text = "Check your Internet connection";
            }
        }

        private void CreateRoom()
        {
            Debug.Log("Creating Room: " + roomName + " for " + (roomSize - roomSize + 1) + " Server + " + (roomSize - 1) + " players");

            status.color = colorSuccess;
            status.text = "Game created! Wait a bit...";

            networkManager.matchMaker.CreateMatch(roomName + Random.Range(1, 9999999), roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
            buttonCreate.GetComponent<Button>().interactable = false;
        }

        // Getting all active rooms
        private void GetRooms()
        {
            networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        }

        public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
        {
            if (!success || matchList == null)
            {
                InternetOn = false;

                return;
            }

            foreach (MatchInfoSnapshot match in matchList)
            {
                //  To make sure we have only one Connect button
                if (GameObject.FindGameObjectWithTag("Connect") == null)
                {
                    GameObject _buttonConnectGO = Instantiate(buttonConnect);
                    _buttonConnectGO.transform.SetParent(buttonConnectParent);
                    _buttonConnectGO.transform.localPosition = new Vector3(buttonConnect.transform.position.x, buttonConnect.transform.position.y, buttonConnect.transform.position.z);
                    _buttonConnectGO.transform.localScale = Vector3.one;
                    Connect _buttonConnect = _buttonConnectGO.GetComponent<Connect>();
                    if (_buttonConnect != null)
                    {
                        _buttonConnect.Setup(match, JoinRoom);
                    }

                    roomList.Add(_buttonConnectGO);
                }
            }

            roomsExisting = roomList.Count != 0;

            InternetOn = true;
        }

        public void JoinRoom(MatchInfoSnapshot _match)
        {
            CancelInvoke("GetRooms");
            buttonCreate.GetComponent<Button>().interactable = false;
            networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
            ClearRoomList();
        }

        private void ClearRoomList()
        {
            foreach (GameObject room in roomList)
            {
                Destroy(room);
            }

            roomList.Clear();
        }
    }
}
