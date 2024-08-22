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
    
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private Vector3 defaultSize;
        [SerializeField] private Line[] grid;
        [SerializeField] private static Cell[] cells;
        private List<Vector3> positions;

        private static EndCellTrigger[] endCells;
        public static bool block = true;

        private void Start()
        {
            cells = GetComponentsInChildren<Cell>();
            endCells = GetComponentsInChildren<EndCellTrigger>();

            positions = new List<Vector3>();

            foreach (var cell in cells)
            {
                positions.Add(cell.transform.position);
                cell.transform.position += Vector3.up * 10;
            }

            StartCoroutine(SpawnCells());
        }

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
        void GenerateGrid()
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

        [Button]
        void ClearGrid()
        {
            while (transform.childCount != 0)
            { 
                DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        private IEnumerator SpawnCells()
        {
            yield return new WaitForSeconds(1.5f);
            for (int i = 0; i < cells.Length; i++)
            {
                if (i == cells.Length - 1)
                {
                    cells[i].transform.DOJump(positions[i], 1f, 1, 0.6f);
                    cells[i].transform.DOShakeRotation(0.7f, positions[i] * 10, 3, 45).onComplete = () => block = false;
                    yield return null;
                    break;
                }

                cells[i].transform.DOJump(positions[i], 1f, 1, 0.6f);
                cells[i].transform.DOShakeRotation(0.7f, positions[i] * 100);
                yield return null;
            }
        }
    }
}