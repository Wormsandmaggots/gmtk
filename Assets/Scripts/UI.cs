using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private string firstLevel;
        
        public void StartGame()
        {
            SceneManager.LoadScene(firstLevel);
        }
    }
}