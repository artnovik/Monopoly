using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

namespace Monopoly.Lobby_v2
{
	public class Connect : MonoBehaviour
	{
		public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
		private JoinRoomDelegate joinRoomCallback;

		public MatchInfoSnapshot match;

		public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback)
		{
			match = _match;
			joinRoomCallback = _joinRoomCallback;
		}

		public void JoinRoom()
		{
			LobbyManager _lobbyManager = GameObject.Find("Network Manager").GetComponent<LobbyManager>();
			_lobbyManager.playerName = GameObject.Find("InputFieldName").GetComponent<InputField>().text;

			// Checking PlayerName Input
			if (string.IsNullOrEmpty(_lobbyManager.playerName))
			{
				_lobbyManager.status.color = _lobbyManager.colorError;
				_lobbyManager.status.text = "Name can't be empty!";

				return;
			}

			// If connected
			_lobbyManager.status.color = _lobbyManager.colorSuccess;
			_lobbyManager.status.text = "Connected! Wait a bit...";

			joinRoomCallback.Invoke(match);
		}

		//ToDo Move this Method into actual Gameplay
		// Disconnect
		/*public void LeaveRoom()
		{
			MatchInfo matchInfo = networkManager.matchInfo;
			networkManager.matchMaker.DropConnection(networkManager.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
			networkManager.StopHost();
		}*/
	}
}
