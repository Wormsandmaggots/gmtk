using System;
using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Grid
{
    public enum CellType
    {
        Normal,
        Start,
        End
    }
    
    public class Cell : MonoBehaviour
    {
        private BoxBase associatedBox;
        private bool isBlocked = false;
        private Material material;

        private void Start()
        {
            material = GetComponentInChildren<MeshRenderer>()?.materials[1];
            // if (material != null)
            // {
            //     materialId = material.shader.FindPropertyIndex("_FlashingSpeed");
            //     Debug.Log(material.HasProperty("_FlashingSpeed"));
            // }
            
            IsOver(false);
        }

        public BoxBase AssociatedBox
        {
            get => associatedBox;
            set
            {
                associatedBox = value;

                Block(value);
            }
        }

        //when block gets on grid cell
        public virtual void OnBlockOccupy(BoxBase box)
        {
            AssociatedBox = box;
        }

        public virtual void Block(bool blocked)
        {
            isBlocked = blocked;
        }

        public virtual CellType GetCellType()
        {
            return CellType.Normal;
        }

        public virtual void InvokeCellTypeRelatedMethods()
        {
            
        }

        public void IsOver(bool val)
        {
            if (material == null)
                return;

            if (!material.HasProperty("_Lerp"))
                return;
            
            if (val)
            {
                material.DOKill();
                material.DOFloat(1, "_Lerp", 0.4f);
            }
            else
            {
                material.DOKill();
                material.DOFloat(0, "_Lerp", 0.4f);
            }
        }

        public int GetFloor()
        {
            Floor f = GetComponentInParent<Floor>();
            
            return f != null ? Convert.ToInt16(GetComponentInParent<Floor>().name) : 0;
        }
    }
}