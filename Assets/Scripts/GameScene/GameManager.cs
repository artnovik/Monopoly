using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Starting");
    }

    private void Update()
    {
        // Host computer will only spectate
        if (NetworkServer.active)
        {
            return;
        }
    }
}
