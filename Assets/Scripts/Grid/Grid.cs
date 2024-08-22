using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Grid
{
    public abstract class Grid : MonoBehaviour
    {
        [SerializeField] protected Vector3 defaultSize;
        
        protected static Cell[] cells;
        protected List<Vector3> positions;

        protected static EndCellTrigger[] endCells;
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
        
        [Button]
        public void ClearGrid()
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
        
        public abstract void GenerateGrid();
    }
}