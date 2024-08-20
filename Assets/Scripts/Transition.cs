using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Transition : MonoBehaviour
    {
        private static Animator anim;
        private static Transition instance;
        
        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
            
            anim = GetComponent<Animator>();
            DontDestroyOnLoad(gameObject);
        }

        public static void PlayAnim()
        {
            anim.SetTrigger("nextLevel");
        }

        public void NextLevel()
        {
            SceneManager.LoadScene((Settings.instance.level + 1).ToString());
        }
    }
}