using DG.Tweening;
using Grid;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class GUI : MonoBehaviour
    {
        public static GUI Instance;

        [SerializeField] private GameObject goToMainMenuButton;
        [SerializeField] private Transform goToMainMenuTarget;
        private Vector3 _goToMainMenuStartPosition;

        [SerializeField] private GameObject closeButton;
        [SerializeField] private Transform closeTarget;
        private Vector3 _closeStartPosition;

        private bool _hidden = true;
        private bool _clickable = true;
        private bool _block;
        
        private void Awake()
        {
            Instance = this;
            
            _goToMainMenuStartPosition = goToMainMenuButton.transform.position;
            _closeStartPosition = closeButton.transform.position;
        }

        public void ShowHome()
        {
            AudioManager.instance.Play("click");

            _clickable = false;
            
            goToMainMenuButton.transform.DOMove(goToMainMenuTarget.position, 1.0f).onComplete = () => _clickable = true;
            closeButton.transform.DOMove(closeTarget.position, 1.0f);
            
            BlurManager.SetBlur(true);
            
            _hidden = false;
        }

        public void ToggleHome()
        {
            if (_block) return;
            if (!_clickable) return;
            if (WinScreen.win) return; 
            
            if (_hidden)
                ShowHome();
            else
            {
                HideHomeWithSound();
            }
        }

        public void HideHome()
        {
            if (!_clickable && !WinScreen.win) return;
            
            _clickable = false;
            
            goToMainMenuButton.transform.DOMove(_goToMainMenuStartPosition, 1.0f).onComplete = () => _clickable = true;
            closeButton.transform.DOMove(_closeStartPosition, 1.0f);
            
            BlurManager.SetBlur(false);

            _hidden = true;
        }

        public void HideHomeWithSound()
        {
            if (!_clickable && !WinScreen.win) return;
            
            AudioManager.instance.Play("click");
            
            _clickable = false;
            
            goToMainMenuButton.transform.DOMove(_goToMainMenuStartPosition, 1.0f).onComplete = () => _clickable = true;
            closeButton.transform.DOMove(_closeStartPosition, 1.0f);
            
            BlurManager.SetBlur(false);

            _hidden = true;
        }

        public void GoToMainMenu()
        {
            if (!_clickable) return;
            
            AudioManager.instance.Play("click");

            _clickable = false;

            Transition.loadMainMenu = true;
            Transition.PlayAnim();
            
            EndCellTrigger.EndCells.Clear();
        }

        public bool Hidden => _hidden;

        public bool Block
        {
            get => _block;
            set => _block = value;
        }
    }
}