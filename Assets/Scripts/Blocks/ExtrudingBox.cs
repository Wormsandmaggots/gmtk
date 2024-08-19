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
        StartCoroutine(Extrude());
    }

    private IEnumerator Extrude()
    {
        while (true)
        {
            foreach (var scaler in scalers)
            {
                Vector3 scale = scaler.transform.localScale;
                Vector3 currDir = scaler.Direction;
        
                if (currDir.x < 0)
                    currDir.x = -currDir.x;

                if (currDir.z < 0)
                    currDir.z = -currDir.z;
        
                scale += Time.deltaTime * extrudeSpeed * currDir;

                scaler.Scale(scale);
            
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.TransformDirection(scaler.Direction), out hit, Mathf.Infinity,
                        Settings.instance.blockLayer))
                {
                    BoxBase box = hit.transform.GetComponent<BoxBase>();
                    box.Execute(this);
                }
            }

            yield return null;
        }
    }
}