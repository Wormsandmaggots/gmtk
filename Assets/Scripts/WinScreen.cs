using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class WinScreen : MonoBehaviour
    {
        public static WinScreen instance;
        public Image winText;
        public Image button;

        private void Awake()
        {
            instance = this;
        }

        public void ShowWinScreen()
        {
            winText.transform.DOMove(Vector3.zero, 1f);
            button.transform.DOMove(Vector3.zero, 1f);
        }
    }
}