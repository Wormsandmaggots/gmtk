using System;
using System.Collections.Generic;
using Blocks;
using UnityEngine;
using UnityEngine.VFX;

namespace Grid
{
    public class EndCell : Cell
    {
        public override void OnBlockOccupy(BoxBase box)
        {
            Debug.Log("Win");
        }

        public override CellType GetCellType()
        {
            return CellType.End;
        }
    }
}