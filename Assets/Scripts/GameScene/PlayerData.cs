using UnityEngine;
using UnityEngine.Networking;

public class PlayerData : NetworkBehaviour
{
    private string pName;

    private uint pScore;

    /// <summary>
    /// Setting Player Name
    /// </summary>
    /// <param name="_pName"></param>
    public void SetPlayerName(string _pName)
    {
        pName = _pName;
        transform.name = _pName;
    }

    public string GetPlayerName()
    {
        return pName;
    }

    /// <summary>
    /// Add specific Score value to Player's Score
    /// </summary>
    /// <param name="addValue"></param>
    public void AddPlayerScore(uint addValue)
    {
        pScore += addValue;
        Debug.Log(pName + " gains: " + addValue + " points. Total count: " + pScore);
    }

    public uint GetPlayerScore()
    {
        return pScore;
    }

    public void Refresh()
    {
        pName = "Player " + PlayerPrefs.GetString("PlayerName");
        pScore = 0;
    }

    public void PlayerDataDebugInfo()
    {
        Debug.Log("PlayerName is: " + pName + ". Score: " + pScore);
    }
}
