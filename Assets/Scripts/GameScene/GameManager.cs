using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    private uint answeredQuestionsCount;

    private void Start()
    {
        Debug.Log("GameManager started.");
        
        // ToDo Question Mechanics
    }

    private void Update()
    {
        // Host computer will only spectate
        //if (NetworkServer.active)
        //{
        //    return;
        //}


    }
}
