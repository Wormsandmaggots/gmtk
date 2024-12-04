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
        private MeshRenderer buttonMesh;
        private bool cachedCanClick = true;
            
        private void Start()
        {
            buttonMesh = GetComponentInChildren<MeshRenderer>();
        }

        private void Update()
        {
            if (cachedCanClick != CanClick())
            {
                if (CanClick())
                {
                    buttonMesh.materials[0].DOFloat(1, "_influence", 0.5f);
                }
                else
                {
                    buttonMesh.materials[0].DOFloat(0, "_influence", 0.5f);
                }
                
                cachedCanClick = CanClick();
            }
        }
        
        private void OnMouseDown()
        {
            if(!CanClick())
                return;

            isClicking = true;
            
            AudioManager.instance.Play("click");

            BlockSpawner.isReseting = true;
            
            transform.DOLocalMove(Vector3.down * 0.1f, 0.2f).onComplete = () =>
            {
                GridGenerator.ResetCells();
                BlockSpawner.Spawn.Invoke();
                if(BlockResolver.instance != null)
                    BlockResolver.instance.ClearResolve();
                
                transform.DOLocalMove(Vector3.zero, 0.1f);
            };
        }

        bool CanClick()
        {
            if (GridGenerator.block) return false;
            if (Tutorial.IsBlocking) return false;
            if (WinScreen.win) return false;
            if (!BlockResolver.isResolving) return false;
            if (BlockSpawner.isReseting) return false;
            
            if(isClicking) return false;

            return true;
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