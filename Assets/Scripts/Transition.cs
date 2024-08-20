using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Transition : MonoBehaviour
    {
        private static Animator anim;
        private void Awake()
        {
            anim = GetComponent<Animator>();
            DontDestroyOnLoad(gameObject);
        }

        public static void PlayAnim()
        {
            anim.SetTrigger("nextLevel");
        }
    }
}