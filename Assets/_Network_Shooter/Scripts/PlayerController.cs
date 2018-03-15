using UnityEngine;
using UnityEngine.Networking;

namespace NetworkShooter
{
    public class PlayerController : NetworkBehaviour
    {
        #region Public Variables

        public GameObject bulletPrefab;
        public Transform bulletSpawn;

        #endregion

        #region Callbacks

        private void Update()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                CmdFire();
            }
        }

        public override void OnStartLocalPlayer()
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }


        #endregion

        #region Methods

        [Command]
        private void CmdFire()
        {
            // Create the Bullet from the Bullet Prefab
            var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

            // Add velocity to the bullet
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

            // Spawn the bullet on the Clients
            NetworkServer.Spawn(bullet);

            // Destroy the bullet after X seconds
            Destroy(bullet, 5.0f);
        }

        #endregion

    }
}
