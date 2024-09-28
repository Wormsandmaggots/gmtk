using System;
using System.Collections;
using DG.Tweening;
using Grid;
using UnityEngine;

namespace DefaultNamespace
{
    public class ResetButton : MonoBehaviour
    {
        private static bool isClicking = false;
        private void OnMouseDown()
        {
            if (GridGenerator.block) return;
            if (Tutorial.IsBlocking) return;
            if (WinScreen.win) return;
            
            if(isClicking) return;

            isClicking = true;
            
            AudioManager.instance.Play("click");
            
            transform.DOLocalMove(Vector3.down * 0.1f, 0.2f).onComplete = () =>
            {
                GridGenerator.ResetCells();
                BlockSpawner.Spawn.Invoke();
                if(BlockResolver.instance != null)
                    BlockResolver.instance.ClearResolve();
                
                transform.DOLocalMove(Vector3.zero, 0.1f);
            };
        }

        private IEnumerator ClickDelay()
        {
            yield return new WaitForSeconds(0.3f);
            isClicking = false;
        }

        public static void ResetIsClicking()
        {
            isClicking = false;
        }
    }
}