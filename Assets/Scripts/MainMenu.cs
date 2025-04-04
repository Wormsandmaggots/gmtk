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

        public Image tutorial;
        public Transform tutorialTarget;

        public EventSystem es;

        private bool clickable = false;
        
        private void Awake()
        {
            es.firstSelectedGameObject = null;
            Game.GameState = GameState.MainMenu;
        }

        public void ShowButtons()
        {
            winText.transform.DOMove(winTextTarget.position, 1f).onComplete = () =>
            {
                tutorial.transform.DOMove(tutorialTarget.position, 1f);
            };
            button.transform.DOMove(buttonTarget.position, 1f).onComplete = () => { clickable = true; };
        }
        
        public void PlayTransition()
        {
            if (!clickable) return;
            AudioManager.instance.Play("click");
            
            clickable = false;
            Transition.PlayAnim();

            es.SetSelectedGameObject(null);

            Game.GameState = GameState.Game;
        }

        public void OnlyTutorial()
        {
            if (!clickable) return;
            AudioManager.instance.Play("click");
            
            clickable = false;
            Transition.PlayAnim();

            es.SetSelectedGameObject(null);

            Game.GameState = GameState.Tutorial;
        }

        public void Exit()
        {
            if (!clickable) return;
            AudioManager.instance.Play("click");

            Application.Quit();
        }
        
    }
}