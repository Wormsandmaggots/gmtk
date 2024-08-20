using System;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Grid
{
    public class BackButton : MonoBehaviour
    {
        private bool isClicking = false;
        private void OnMouseDown()
        {
            if (Tutorial.IsBlocking) return;
            if (WinScreen.win) return;
            
            if(isClicking) return;

            isClicking = true;
            
            AudioManager.instance.Play("click");
            
            transform.DOLocalMove(Vector3.down * 0.1f, 0.2f).onComplete = () =>
            {
                transform.DOLocalMove(Vector3.zero, 0.1f).onComplete = () =>
                {
                    Transition.PlayAnim();
                };
            };
        }
    }
}