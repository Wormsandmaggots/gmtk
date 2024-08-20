using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class BlurCamera : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Camera>().fieldOfView = Camera.main.fieldOfView;
        }
    }
}