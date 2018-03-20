using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Monopoly.Lobby_v2
{
    public class PhotonLauncher : Photon.PunBehaviour
    {
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

        [HideInInspector]
        public string playerName;

        private bool InternetOn;
        private bool roomsExisting;

        public Color32 colorProcess = new Color32(225, 216, 98, 255);
        public Color32 colorSuccess = new Color32(98, 225, 114, 255);
        public Color32 colorError = new Color32(225, 98, 98, 255);

        #region Photon

        private const string _gameVersion = "1.0";
        private const string roomName = "Monopoly";
        [SerializeField]
        private PhotonLogLevel logLevel = PhotonLogLevel.Informational;
        [SerializeField]
        private byte maxPlayersPerRoom = 6;

        #endregion

        private void Awake()
        {
            PhotonNetwork.autoJoinLobby = false;
            PhotonNetwork.automaticallySyncScene = true;
            PhotonNetwork.logLevel = logLevel;
        }

        public void Connect()
        {
            if (PhotonNetwork.connected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Master");
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("No random room available, so create one");
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom }, null);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("Now this client is in a room");
        }

        public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
        {
            Debug.Log("Room creation failed");
        }

        public override void OnDisconnectedFromPhoton()
        {
            Debug.Log("Disconnected from Photon");
        }

        private void SetStatusText(string newText, Color32 newColor)
        {
            status.text = newText;
            status.color = newColor;
        }

        public void ButtonStartOnClick()
        {

        }

        private void CreateRoom()
        {

        }

        // Getting all active rooms
        private void GetRooms()
        {

        }

        public void JoinRoom()
        {

        }

        private void ClearRoomList()
        {

        }
    }
}
