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

        public static bool win = false;
        
        private void Awake()
        {
            es.firstSelectedGameObject = null;
            instance = this;
            win = false;
        }

        public void ShowWinScreen()
        {
            win = true;
            
            GUI.Instance.HideHome();
            
            AudioManager.instance.Play("win");
            button.GetComponentInChildren<Button>().interactable = false;

            winText.transform.DOMove(winTextTarget.position, 1f);
            button.transform.DOMove(buttonTarget.position, 1f).onComplete = () =>
            {
                clickable = true;
                button.GetComponentInChildren<Button>().interactable = true;
            };
            
            BlurManager.SetBlur(true);
        }

        public void PlayTransition()
        {
            if (!clickable) return;
            AudioManager.instance.Play("click");
            
            clickable = false;
            Transition.PlayAnim();

            es.SetSelectedGameObject(null);
        }
    }
}