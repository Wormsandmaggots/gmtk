using System;
using DG.Tweening;
using Grid;
using UnityEngine;

namespace DefaultNamespace
{
    public class ResetButton : MonoBehaviour
    {
        private bool isClicking = false;
        private void OnMouseDown()
        {
            if (BlockResolver.isResolving) return;
            
            if(isClicking) return;

            isClicking = true;
            
            transform.DOLocalMove(Vector3.down * 0.1f, 0.2f).onComplete = () =>
            {
                GridGenerator.ResetCells();
                BlockSpawner.Spawn.Invoke();
                BlockResolver.instance.ClearResolve();
                
                transform.DOLocalMove(Vector3.zero, 0.1f).onComplete = () => { isClicking = false; };
            };
        }
    }
}