using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class StartButton : MonoBehaviour
    {
        private bool isClicking = false;
        private void OnMouseDown()
        {
            if (isClicking) return;
            
            isClicking = true;
            Debug.Log("START");
            transform.DOLocalMove(Vector3.down * 0.1f, 0.2f).onComplete = () =>
            {
                BlockResolver.instance.ResolveRound();

                transform.DOLocalMove(Vector3.zero, 0.1f).onComplete = () => { isClicking = false; };
            };
        }

        private void OnMouseOver()
        {
            
        }
    }
}