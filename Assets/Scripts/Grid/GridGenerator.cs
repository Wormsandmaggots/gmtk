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
                endCell.Reset();
            }
        }

        [Button]
        public override void GenerateGrid()
        {
            ClearGrid();
            
            GameObject floor = new GameObject("0");
            floor.transform.parent = transform;
            floor.transform.position = transform.position;
            floor.AddComponent(typeof(Floor));
            
            for (int i = 0; i < grid.Length; i++)
            {
                var line = grid[i];
                
                for (int j = 0; j < line.Length(); j++)
                {
                    if (line[j] == null)
                        continue;
                    
                    Cell cell = Instantiate(line[j], floor.transform);
                    cell.transform.position = new Vector3(defaultSize.x * i, defaultSize.y, defaultSize.z * j);
                    cell.transform.position += transform.position;
                }
            }
        }
    }
}