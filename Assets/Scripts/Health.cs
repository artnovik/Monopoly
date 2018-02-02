using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NetworkShooter
{
    public class Health : NetworkBehaviour
    {
        private NetworkStartPosition[] spawnPoints;
        public RectTransform healthBar;

        public const int MaxHealth = 100;

        public bool destroyOnDeath;

        [SyncVar(hook = "OnChangeHealth")]
        public int currentHealth = MaxHealth;

        private void Start()
        {
            if (isLocalPlayer)
            {
                spawnPoints = FindObjectsOfType<NetworkStartPosition>();
            }
        }

        public void TakeDamage(int amount)
        {
            if (!isServer)
            {
                return;
            }

            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                if (destroyOnDeath)
                {
                    Destroy(gameObject);
                }
                else
                {
                    currentHealth = MaxHealth;

                    // Called on the Server, but invoked on the Clients
                    RpcRespawn();
                    Debug.Log(gameObject.name + " is dead, but respawned");
                }
            }
        }

        private void OnChangeHealth(int currentHealth)
        {
            healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);

        }

        [ClientRpc]
        private void RpcRespawn()
        {
            if (isLocalPlayer)
            {
                // Move back to zero location
                //transform.position = Vector3.zero;

                // Set the spawn point to origin as a default value
                Vector3 spawnPoint = Vector3.zero;

                // If there is a spawn point array and the array is not empty, pick a spawn point at random
                if (spawnPoints != null && spawnPoints.Length > 0)
                {
                    spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
                }

                // Set the player's position to the chosen spawn point
                transform.position = spawnPoint;
            }
        }
    }
}
