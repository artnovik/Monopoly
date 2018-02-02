using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace NetworkShooter
{
    public class Health : NetworkBehaviour
    {
        public RectTransform healthBar;

        public const int MaxHealth = 100;

        [SyncVar(hook = "OnChangeHealth")]
        public int currentHealth = MaxHealth;

        public void TakeDamage(int amount)
        {
            if (!isServer)
            {
                return;
            }

            currentHealth -= amount;

            if (currentHealth <= 0)
            {
                currentHealth = MaxHealth;

                // Called on the Server, but invoked on the Clients
                RpcRespawn();
                Debug.Log(gameObject.name + " is dead, but respawned");
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
                transform.position = Vector3.zero;
            }
        }
    }
}
