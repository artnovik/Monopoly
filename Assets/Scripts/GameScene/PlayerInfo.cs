using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerInfo : NetworkBehaviour
{
    [SerializeField]
    private string playerName;

    [SerializeField]
    private uint playerScore;

    private void Start()
    {

    }

    private void Update()
    {

    }

    /// <summary>
    /// Setting Player Name
    /// </summary>
    /// <param name="_playerName"></param>
    public void SetPlayerName(string _playerName)
    {
        playerName = _playerName;
        transform.name = _playerName;
    }

    /// <summary>
    /// Add specific Score value to Player's Score
    /// </summary>
    /// <param name="_scoreValue"></param>
    public void AddPlayerScore(uint gainedPointsValue)
    {
        playerScore += gainedPointsValue;
        Debug.Log(playerName + " gains: " + gainedPointsValue + " points. Total count: " + playerScore);
    }
}
