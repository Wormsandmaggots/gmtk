using System;
using System.Collections;
using System.Collections.Generic;
using Blocks;
using Blocks.Helpers;
using DefaultNamespace;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

public class ExtrudingBox : BoxBase
{
    [SerializeField] private float extrudeSpeed = 100;
    private Scaler[] scalers;
    private bool isExecuting = false;
    private bool forceStop = false;
    
    protected override void Start()
    {
        id = 2;

        scalers = GetComponentsInChildren<Scaler>();
    }

    public override void TryActivate(BoxBase touching)
    {
        base.TryActivate(touching);
    }

    public override void Execute(BoxBase previous)
    {
        if(isExecuting) return;
        
        isExecuting = true;
        transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
        transform.DOJump(transform.position, 0.2f, 1, 0.2f).onComplete = () => StartCoroutine(Extrude());
    }

    private IEnumerator Extrude()
    {
        while (true)
        {
            if(forceStop)
                yield break;
            
            int i = 0;
            foreach (var scaler in scalers)
            {
                if (!scaler.ShouldExtrude)
                {
                    i++;
                    continue;
                }
                
                Vector3 scale = scaler.transform.localScale;
                Vector3 currDir = scaler.Direction;
        
                if (currDir.x < 0)
                    currDir.x = -currDir.x;

                if (currDir.z < 0)
                    currDir.z = -currDir.z;
        
                scale += Time.deltaTime * extrudeSpeed * currDir;

                scaler.Scale(scale);
                
            }
            
            if(i == scalers.Length)
                yield break;

            yield return null;
        }
    }

    private IEnumerator DelayTurnOffScaling(Scaler scaler, float delay)
    {
        yield return new WaitForSeconds(delay);

        scaler.ShouldExtrude = false;
    }
    
    private IEnumerator DelayTurnOffScaling(Scaler scaler)
    {
        yield return null;

        scaler.ShouldExtrude = false;
    }

    public void ForceStop()
    {
        forceStop = true;
    }

    public override void Reset()
    {
        StopAllCoroutines();
        StartCoroutine(ResetScalers());
    }

    private IEnumerator ResetScalers()
    {
        CanBeDragged = false;
        
        foreach (var scaler in scalers)
        {
            scaler.ShouldExtrude = true;
        }
        
        while (true)
        {
            int counter = 0;
            foreach (var scaler in scalers)
            {                
                Vector3 scale = scaler.transform.localScale;

                if (scale.sqrMagnitude == 3)
                {
                    counter++;
                    continue;
                }
                
                Vector3 currDir = scaler.Direction;
        
                if (currDir.x > 0)
                    currDir.x = -currDir.x;

                if (currDir.z > 0)
                    currDir.z = -currDir.z;
        
                scale += Time.deltaTime * extrudeSpeed * currDir;

                if (scale.x < 1)
                    scale.x = 1;

                if (scale.z < 1)
                    scale.z = 1;

                scaler.Scale(scale);
            }

            if (counter == scalers.Length)
                break;
                
            yield return null;
        }
        
        transform.DOJump(StartPos1, 0.5f, 1, 0.5f).onComplete = () =>
        {
            BoxBase.ResetCounter--;

            if (BoxBase.ResetCounter <= 0)
            {
                BlockSpawner.ResetToBaseBoxValues();
            }
            
            forceStop = false;
            isExecuting = false;
        };
    }

    public bool ForceStop1 => forceStop;
}