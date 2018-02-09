using System.Collections;
using System.Collections.Generic;
using Monopoly.Lobby_v2;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class InitPlayers : MonoBehaviour
{
	private GameObject networkManagerGO;
	private NetworkManager networkManager;
	private LobbyManager lobbyManager;

	[SerializeField]
	private Text playerText1;

	// Use this for initialization
	private void Start()
	{
		networkManagerGO = GameObject.Find("Network Manager");
		networkManager = networkManagerGO.GetComponent<NetworkManager>();
		lobbyManager = networkManagerGO.GetComponent<LobbyManager>();

		if (string.IsNullOrEmpty(playerText1.text))
		{
			playerText1.text = lobbyManager.playerName;
		}
		 
	}
}
