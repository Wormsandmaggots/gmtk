using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class EnvShow : MonoBehaviour
    {
        private static float delay = 0.5f;
        private Vector3 targetScale;
        [SerializeField] private float animDuration = 0.5f;
        [SerializeField] private float jumpPower = 1.2f;
        [SerializeField] private int numJumps = 1;
        [SerializeField] private Vector3 rotation = Vector3.forward;
        [SerializeField] private float rotationPower = 50f;
        [SerializeField] private bool doRotation = true;
        [SerializeField] private bool doScale = true;
        [SerializeField] private bool doJump = true;
        
        private void Start()
        { 
            targetScale = transform.localScale;
            transform.localScale = Vector3.zero;

            StartCoroutine(Show());
        }

        private IEnumerator Show()
        {
            yield return new WaitForSeconds(Random.Range(1, 1 + delay));

            if(doScale) transform.DOScale(targetScale, animDuration);
            if(doRotation) transform.DOPunchRotation(rotation * rotationPower, animDuration);
            if(doJump) transform.DOJump(transform.position, jumpPower, numJumps, animDuration);
        }
    }
}