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
            int i = 0;
            foreach (var box in boxesToSpawn)
            {
                var newBox = Instantiate(box, offset * (i + 1), quaternion.identity, transform);
                newBox.StartPos = offset * (i + 1);
                
                yield return new WaitForSeconds(spawnDelay);
                i++;
            }
            
            yield return null;
        }
    }
}