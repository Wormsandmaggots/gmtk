using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class Transition : MonoBehaviour
    {
        private static Animator anim;
        private static Transition instance;

        public static bool loadMainMenu = false;
        
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

            //hard coded, good enough for now
            if (loadMainMenu || 
                level > 10 ||
                level > 4 && Game.GameState == GameState.Tutorial)
            {
                SceneManager.LoadScene("MainMenu");
                Settings.instance.level = 0;

                loadMainMenu = false;
            }
            else
            {
                SceneManager.LoadScene((Settings.instance.level + 1).ToString());
            }
        }
    }
}