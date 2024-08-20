using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class WinScreen : MonoBehaviour
    {
        public static WinScreen instance;
        
        public Image winText;
        public Transform winTextTarget;
        
        public Image button;
        public Transform buttonTarget;

        public EventSystem es;

        private bool clickable = false;
        
        private void Awake()
        {
            es.firstSelectedGameObject = null;
            instance = this;
        }

        public void ShowWinScreen()
        {
            winText.transform.DOMove(winTextTarget.position, 1f);
            button.transform.DOMove(buttonTarget.position, 1f).onComplete = () => { clickable = true; };
            BlurManager.SetBlur(true);
        }

        public void NextLevel()
        {
            if (!clickable) return;
            
            SceneManager.LoadScene((Settings.instance.level + 1).ToString());
        }
    }
}