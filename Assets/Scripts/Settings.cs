using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public enum Reset
    {
        Spawn,
        Unextrude
    }
    
    public class Settings : MonoBehaviour
    {
        public static Settings instance;

        public LayerMask cellLayer;
        public LayerMask blockLayer;
        public LayerMask lineLayer;
        public LayerMask knobLayer;
        public LayerMask wallLayer;

        public Reset resetMethod = Reset.Unextrude;

        public int level = 0;

        public LayerMask mouseInputLayer;

        public float cellBlockOffset = 0.5f;
        public float cellBlockDragOffset = 1f;

        private void Awake()
        {
            if(Char.IsNumber(SceneManager.GetActiveScene().name[0]))
                level = Convert.ToInt16(SceneManager.GetActiveScene().name);
            
            if(instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
            }

            Camera.main.eventMask = mouseInputLayer;
        }
    }
}