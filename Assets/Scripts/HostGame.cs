using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Monopoly
{
	public class HostGame : MonoBehaviour
	{
		[SerializeField]
		private uint roomSize = 5;

		private string roomName;

		private NetworkManager networkManager;

		private void Start()
		{
			networkManager = NetworkManager.singleton;

			if (networkManager.matchMaker == null)
			{
				networkManager.StartMatchMaker();
			}
		}

		public void SetRoomName(InputField inputFieldRoomNameText)
		{
			roomName = inputFieldRoomNameText.GetComponent<InputField>().text;
		}

		public void CreateRoom()
		{
			if (!string.IsNullOrEmpty(roomName))
			{
				Debug.Log("Creating Room: " + roomName + " for " + roomSize + " players");

				networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
			}
		}
	}
}
