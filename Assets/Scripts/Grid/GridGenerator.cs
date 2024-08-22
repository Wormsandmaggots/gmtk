using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Grid
{
    [Serializable]
    public class Line
    {
        [ReorderableList]
        public List<Cell> lineContent;
        
        public Cell this[int key]
        {
            get => lineContent[key];
        }

        public int Length()
        {
            return lineContent.Count;
        }
    }
    
    public class GridGenerator : Grid
    {
        [SerializeField] private Line[] grid;

        public static void ResetCells()
        {
            if (cells == null) return;
            
            foreach (var cell in cells)
            {
                cell.AssociatedBox = null;
            }

            if (endCells == null)
                return;

            EndCellTrigger.EndCells.Clear();
            
            foreach (var endCell in endCells)
            {
                EndCellTrigger.EndCells.Add(endCell);    
            }
        }

        [Button]
        public override void GenerateGrid()
        {
            ClearGrid();
            
            for (int i = 0; i < grid.Length; i++)
            {
                var line = grid[i];
                
                for (int j = 0; j < line.Length(); j++)
                {
                    if (line[j] == null)
                        continue;
                    
                    Cell cell = Instantiate(line[j], transform);
                    cell.transform.position = new Vector3(defaultSize.x * i, defaultSize.y, defaultSize.z * j);
                }
            }
        }
    }
}