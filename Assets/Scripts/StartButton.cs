using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class StartButton : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Debug.Log("START");
            transform.DOLocalMove(Vector3.down * 0.1f, 0.5f).onComplete = () =>
            {
                BlockResolver.instance.ResolveRound();

                transform.DOLocalMove(Vector3.zero, 0.3f);
            };
        }

        private void OnMouseOver()
        {
            
        }
    }
}