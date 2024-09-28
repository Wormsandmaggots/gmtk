using System;
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

        public int GetFloor()
        {
            Floor f = GetComponentInParent<Floor>();
            
            return f != null ? Convert.ToInt16(GetComponentInParent<Floor>().name) : 0;
        }
    }
}