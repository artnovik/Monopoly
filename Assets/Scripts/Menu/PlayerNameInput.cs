using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputField))]
public class PlayerNameInput : MonoBehaviour
{
    private static string playerNamePrefKey = "PlayerName";

    private void Start()
    {
        string defaultName = "";
        InputField _inputField = GetComponent<InputField>();

        if (_inputField != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.playerName = defaultName;
    }

    public void SetPlayerName(string inputName)
    {
        // Force "space" string in case value is an empty string, else playerName would not be updated.
        PhotonNetwork.playerName = inputName + " ";
        PlayerPrefs.SetString(playerNamePrefKey, inputName);
    }
}
