using UnityEngine;

namespace DefaultNamespace
{
    public class LevelSettings : MonoBehaviour
    {
        public static bool BlockRotation;
        
        [SerializeField] private bool levelBlockRotation;
        
        private void Awake()
        {
            BlockRotation = levelBlockRotation;
        }

        private void OnDestroy()
        {
            BlockRotation = false;
        }
    }
}