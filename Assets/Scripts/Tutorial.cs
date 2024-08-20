using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Vector3 startPos;
        private bool clickable = false;
        private int i = 2;
        public static bool IsBlocking = false;
        
        private void Start()
        {
            startPos = transform.position;
            BlurManager.SetBlur(true);
            IsBlocking = true;
            transform.DOMove(target.position, 1f).onComplete = () => clickable = true;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && clickable)
            {
                i--;

                if (i == 0)
                {
                    BlurManager.SetBlur(false);
                    transform.DOMove(startPos, 1f).onComplete = () => IsBlocking = false;
                }
            }
        }
    }
}