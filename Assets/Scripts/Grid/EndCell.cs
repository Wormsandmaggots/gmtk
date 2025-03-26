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
        private Vector3 FlagStartPosition;

        protected override void Start()
        {
            base.Start();
            
            SetFlagActive(false);
            FlagStartPosition = FlagObject.transform.position;
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
            FlagObject.transform.DOMove(FlagStartPosition, 0.3f).onComplete = () => SetFlagActive(false);
        }
    }
}