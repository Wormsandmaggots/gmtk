using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class MainMenu : MonoBehaviour
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
        }

        public void ShowButtons()
        {
            winText.transform.DOMove(winTextTarget.position, 1f);
            button.transform.DOMove(buttonTarget.position, 1f).onComplete = () => { clickable = true; };
        }
        
        public void PlayTransition()
        {
            AudioManager.instance.Play("click");

            if (!clickable) return;
            
            clickable = false;
            Transition.PlayAnim();

            es.SetSelectedGameObject(null);
        }

        public void Exit()
        {
            AudioManager.instance.Play("click");

            Application.Quit();
        }
        
    }
}