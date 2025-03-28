using System;
using System.Collections.Generic;
using Blocks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.VFX;

namespace Grid
{
    public class EndCell : Cell
    {
        [SerializeField] private Transform FlagPoint;
        [SerializeField] private GameObject FlagObject;
        [SerializeField] private VisualEffect effect;
        [SerializeField] private VisualEffect dissapearEffect;
        private EndCellTrigger Trigger;
        private Vector3 FlagStartPosition;
        private Vector3 FlagStartRotation;
        private Vector3 FlagStartScale;

        protected override void Start()
        {
            base.Start();
            
            Trigger = GetComponentInChildren<EndCellTrigger>();
            
            SetFlagActive(false);
            FlagStartPosition = FlagObject.transform.position;
            FlagStartRotation = FlagObject.transform.eulerAngles;
            FlagStartScale = FlagObject.transform.localScale;
        }

        public override void OnBlockOccupy(BoxBase box)
        {
            Debug.Log("Win");
        }

        public override CellType GetCellType()
        {
            return CellType.End;
        }

        public void SetParticleColor(Color color)
        {
            effect.SetVector4("Color", color);
        }

        public void MoveFlag(Vector3 pos)
        {
            FlagObject.transform.DOMove(pos, 0.2f).onComplete = () =>
            {
                if(effect != null)
                    effect.Play();
            };
            FlagObject.transform.DOShakeRotation(0.7f, 5, 30);
        }

        public void SetFlagActive(bool active)
        {
            FlagObject.SetActive(active);
        }

        public override void PlayEffect()
        {
            
        }

        public override void Reset()
        {
            if(FlagObject.activeSelf)
                dissapearEffect.Play();
            
            FlagObject.transform.DOJump(FlagObject.transform.position, 0.5f, 1, 0.4f);
            
            FlagObject.transform.DOScale(Vector3.zero, 0.4f).onComplete = () =>
            {
                FlagObject.transform.position = FlagStartPosition;
                FlagObject.transform.eulerAngles = FlagStartRotation;
                FlagObject.transform.localScale = FlagStartScale;
                
                SetFlagActive(false);
            };

            FlagObject.transform.DOShakeRotation(0.7f, 3, 20);

            //FlagObject.transform.DORotate(FlagObject.transform.eulerAngles + Vector3.up * 600, 0.2f);
            //FlagObject.transform.DOMove(FlagStartPosition, 0.3f).onComplete = () => SetFlagActive(false);
        }
    }
}