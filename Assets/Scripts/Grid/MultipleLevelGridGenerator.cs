using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Grid
{
    [Serializable]
    public class Level
    {
        [ReorderableList]
        public List<Line> levelContent;
        
        public Line this[int key]
        {
            get => levelContent[key];
        }

        public int Length()
        {
            return levelContent.Count;
        }
    }
    
    public class MultipleLevelGridGenerator : Grid
    {
        [SerializeField] private Level[] levels;
        [SerializeField] private float yScaleMultiplier;
        
        [Button]
        public override void GenerateGrid()
        {
            ClearGrid();
            
            for (int k = 0; k < levels.Length; k++)
            {
                Level level = levels[k];
                
                for (int i = 0; i < level.Length(); i++)
                {
                    Line line = level[i];

                    for (int j = 0; j < line.Length(); j++)
                    {
                        if (line[j] == null)
                            continue;
                    
                        Cell cell = Instantiate(line[j], transform);
                        cell.transform.position = new Vector3(defaultSize.x * i, defaultSize.y * k, defaultSize.z * j);
                        cell.transform.localScale = new Vector3(1, k > 0 ? cell.transform.localScale.y * k * yScaleMultiplier : 1, 1);
                    }
                }
            }
        }
    }
}