using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Settings : MonoBehaviour
    {
        public static Settings instance;

        public LayerMask cellLayer;
        public LayerMask blockLayer;

        public float cellBlockOffset = 0.5f;
        public float cellBlockDragOffset = 1f;

        private void Awake()
        {
            if(instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
            }
        }
    }
}