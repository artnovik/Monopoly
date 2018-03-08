using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

namespace Monopoly.Lobby_v1
{
	public class RoomListItem : MonoBehaviour
	{
		public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
		private JoinRoomDelegate joinRoomCallback;

		[SerializeField]
		private Text roomNameText;

		private MatchInfoSnapshot match;

		public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback)
		{
			match = _match;
			joinRoomCallback = _joinRoomCallback;

			roomNameText.text = match.name + " (" + match.currentSize + "/" + match.maxSize + ")";
		}

		public void JoinRoom()
		{
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
