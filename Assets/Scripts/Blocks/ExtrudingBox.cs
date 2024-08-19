using System;
using System.Collections;
using System.Collections.Generic;
using Blocks.Helpers;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;

public class ExtrudingBox : BoxBase
{
    [SerializeField] private float extrudeSpeed = 100;
    private Scaler[] scalers;
    private bool isExecuting = false;
    
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
        StartCoroutine(Extrude());
    }

    private IEnumerator Extrude()
    {
        while (true)
        {
            int i = 0;
            foreach (var scaler in scalers)
            {
                if(!scaler.ShouldExtrude) continue;
                
                RaycastHit hit = scaler.CheckBlockCollision();

                if (hit.collider != null)
                {
                    scaler.ShouldExtrude = false;

                    BoxBase box = hit.transform.GetComponent<BoxBase>();
                    
                    if(box != null)
                        box.Execute(this);
                    else
                    {
                        hit.transform.parent.parent.parent.GetComponent<BoxBase>().Execute(this);
                    }

                    i++;
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
}