using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Ground : MonoBehaviour, IGetColor
    {
        public static Ground instance; 
        private Material material;

        private void Start()
        {
            instance = this;
            material = GetComponent<Renderer>().material;
        }

        public Color GetColor()
        {
            return material.color;
        }
    }
}