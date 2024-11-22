using System;
using DG.Tweening;
using Grid;
using UnityEngine;

namespace DefaultNamespace
{
    public class StartButton : MonoBehaviour
    {
        private bool isClicking = false;
        private void OnMouseDown()
        {
            if (GridGenerator.block) return;
            if (Tutorial.IsBlocking) return;
            if (BlockResolver.isResolving) return;
            if (BlockSpawner.isReseting) return;
            if (!BlockResolver.canResolve) return;
            
            if (isClicking) return;
            
            isClicking = true;
            AudioManager.instance.Play("click");

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