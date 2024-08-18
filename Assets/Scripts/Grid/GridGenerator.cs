using System;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace Grid
{
    [Serializable]
    public class BoxType
    {
        //[Dropdown("Box types")]
        public BoxBase BoxObject;
    }
    
    [Serializable]
    public class Line
    {
        [ReorderableList]
        public List<BoxType> lineContent;
        
        public BoxType this[int key]
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
        
        [Button]
        void GenerateGrid()
        {
            ClearGrid();
            
            for (int i = 0; i < grid.Length; i++)
            {
                var line = grid[i];
                for (int j = 0; j < line.Length(); j++)
                {
                    if (line[j] == null || line[j].BoxObject == null)
                        continue;
                    
                    BoxBase cell = Instantiate(line[j].BoxObject, transform);
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
    }
}