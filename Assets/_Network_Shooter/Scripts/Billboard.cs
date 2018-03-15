using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NetworkShooter
{
    public class Billboard : MonoBehaviour
    {
        private void Update()
        {
            transform.LookAt(Camera.main.transform);
        }
    }
}
