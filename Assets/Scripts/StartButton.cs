using System;
using DG.Tweening;
using Grid;
using UnityEngine;

namespace DefaultNamespace
{
    public class StartButton : MonoBehaviour
    {
        private bool isClicking = false;
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
            if (!CanClick())
                return;
            
            isClicking = true;
            AudioManager.instance.Play("click");

            transform.DOLocalMove(Vector3.down * 0.1f, 0.2f).onComplete = () =>
            {
                BlockResolver.instance.ResolveRound();

                transform.DOLocalMove(Vector3.zero, 0.1f).onComplete = () => { isClicking = false; };
            };
        }

        bool CanClick()
        {
            if (GridGenerator.block) return false;
            if (Tutorial.IsBlocking) return false;
            if (BlockResolver.isResolving) return false;
            if (BlockSpawner.isReseting) return false;
            if (!BlockResolver.canResolve) return false;
            if (!GUI.Instance.Hidden) return false;
            
            if (isClicking) return false;

            return true;
        }
        private void OnMouseOver()
        {
            
        }
    }
}