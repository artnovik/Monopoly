using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

namespace Monopoly.Lobby_v2
{
    public class Connect : MonoBehaviour
    {
        public delegate void JoinRoomDelegate(MatchInfoSnapshot _match);
        private JoinRoomDelegate joinRoomCallback;

        private PhotonLauncher _photonLauncher;

        public MatchInfoSnapshot match;

        public void Setup(MatchInfoSnapshot _match, JoinRoomDelegate _joinRoomCallback)
        {
            match = _match;
            joinRoomCallback = _joinRoomCallback;
        }

        public void JoinRoom()
        {
            _photonLauncher = GameObject.Find("LobbyManager").GetComponent<PhotonLauncher>();

            _photonLauncher.playerName = GameObject.Find("InputFieldName").GetComponent<InputField>().text;

            // Checking PlayerName Input
            if (string.IsNullOrEmpty(_photonLauncher.playerName))
            {
                _photonLauncher.status.color = _photonLauncher.colorError;
                _photonLauncher.status.text = "Name can't be empty!";
                return;
            }
            else
            {
                // If connected
                _photonLauncher.status.color = _photonLauncher.colorSuccess;
                gameObject.GetComponent<Button>().interactable = false;
                _photonLauncher.status.text = "Connected! Wait a bit...";
                PlayerPrefs.SetString("PlayerName", _photonLauncher.playerName);

                joinRoomCallback.Invoke(match);
            }
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
