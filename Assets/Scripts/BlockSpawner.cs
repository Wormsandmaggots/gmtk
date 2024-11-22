using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class BlockSpawner : MonoBehaviour
    {
        public static bool isReseting = false;
        [SerializeField] private List<BoxBase> boxesToSpawn;
        [SerializeField] private Vector3 offset = Vector3.right;
        [SerializeField] private float spawnDelay = 0.2f;

        public static UnityEvent Spawn = new UnityEvent();

        private static List<BoxBase> spawnedBlocks = new List<BoxBase>();

        private void OnDisable()
        {
            Spawn.RemoveListener(ResetBlocks);
        }

        private void OnEnable()
        {
            Spawn.AddListener(ResetBlocks);
        }

        private void Start()
        {
            StartCoroutine(SpawnCoroutine(3f));
        }

        private void ResetBlocks()
        {
            if (spawnedBlocks == null || spawnedBlocks.Count < 1)
                return;

            if (Settings.instance.resetMethod == Reset.Spawn)
            {
                foreach (var block in spawnedBlocks)
                {
                    Destroy(block.gameObject);
                }
            
                spawnedBlocks.Clear();
            
                StartCoroutine(SpawnCoroutine(0));
            }

            if (Settings.instance.resetMethod == Reset.Unextrude)
            {
                foreach (var block in spawnedBlocks)
                {
                    block.Reset();
                }
            }
        }

        public static void ResetToBaseBoxValues()
        {
            foreach (var block in spawnedBlocks)
            {
                block.ResetBase();
            }

            BoxBase.ResetCounter = spawnedBlocks.Count;
            ResetButton.ResetIsClicking();
        }

        private IEnumerator SpawnCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);
            
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

            spawnedBlocks = new List<BoxBase>(GetComponentsInChildren<BoxBase>());

            BoxBase.ResetCounter = spawnedBlocks.Count;

            isReseting = false;
        }
    }
}