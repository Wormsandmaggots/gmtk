using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private String tutorialTextPC;
        [SerializeField] private String tutorialTextMobile;
        [SerializeField] private float tutorialTextSize = 144;
        private Vector3 startPos;
        private bool clickable = false;
        private int i = 2;
        private float duration;
        public static bool IsBlocking = false;
        
        private void Start()
        {
#if UNITY_IOS || UNITY_ANDROID
            if(tutorialTextMobile.Length != 0)
                GetComponentInChildren<TextMeshProUGUI>().text = tutorialTextMobile;
#elif UNITY_STANDALONE_WIN
            if(tutorialTextPC.Length != 0)
                GetComponentInChildren<TextMeshProUGUI>().text = tutorialTextPC;
#elif UNITY_WEBGL
            if (Application.isMobilePlatform)
            {
                if(tutorialTextMobile.Length != 0)
                    GetComponentInChildren<TextMeshProUGUI>().text = tutorialTextMobile;
            }
            else
            {
                if(tutorialTextPC.Length != 0)
                    GetComponentInChildren<TextMeshProUGUI>().text = tutorialTextPC;
            }
#endif
            
            GetComponentInChildren<TextMeshProUGUI>().fontSize = tutorialTextSize;
            
            startPos = transform.position;
            BlurManager.SetBlur(true);
            IsBlocking = true;

            GUI.Instance.Block = true;
            
            Sequence s = DOTween.Sequence();
            s.AppendInterval(2f);
            s.Append(transform.DOMove(target.position, 1f));
            s.onComplete = () => clickable = true;
            s.Play();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && clickable)
            {
                i--;

                if (i == 0)
                {
                    BlurManager.SetBlur(false);

                    var tween = transform.DOMove(startPos, 1f);
                    tween.onComplete = () =>
                    {
                        GUI.Instance.Block = false;
                        IsBlocking = false;
                    };
                    tween.onUpdate = () =>
                    {
                        duration += Time.deltaTime;

                        if (duration >= 0.3f)
                            IsBlocking = false;
                    };
                }
            }
        }
    }
}