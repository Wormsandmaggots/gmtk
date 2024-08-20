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
            anim = instance.GetComponent<Animator>();
            anim.SetTrigger("nextLevel");
        }

        public void NextLevel()
        {
            int level = Settings.instance.level + 1;

            if (level > 10)
            {
                SceneManager.LoadScene("MainMenu");
                Settings.instance.level = 0;
            }
            else
            {
                SceneManager.LoadScene((Settings.instance.level + 1).ToString());
            }
        }
    }
}