using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace DefaultNamespace
{
    public class BlockSpawner : MonoBehaviour
    {
        [SerializeField] private List<BoxBase> boxesToSpawn;
        [SerializeField] private Vector3 offset = Vector3.right;
        [SerializeField] private float spawnDelay = 0.2f;
        private void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }

        private IEnumerator SpawnCoroutine()
        {
            Vector3 startPos = transform.position;
            
            int i = 0;
            foreach (var box in boxesToSpawn)
            {
                Vector3 pos = startPos + offset * (i + 1);
                pos.y += Settings.instance.cellBlockDragOffset;
                
                var newBox = Instantiate(box, pos, quaternion.identity, transform);
                newBox.StartPos = pos;
                
                yield return new WaitForSeconds(spawnDelay);
                i++;
            }
            
            yield return null;
        }
    }
}