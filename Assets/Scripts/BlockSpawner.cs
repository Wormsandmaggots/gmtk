using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

                newBox.transform.DOJump(newBox.StartPos, 1f, 1, 0.6f);
                newBox.transform.DOShakeRotation(0.7f, newBox.StartPos * 100);
                //newBox.transform.DOPunchPosition(newBox.StartPos, 0.6f, 1, 0.2f);
                
                yield return new WaitForSeconds(spawnDelay);
                i++;
            }
            
            yield return null;
        }
    }
}