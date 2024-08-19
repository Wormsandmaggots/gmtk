using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BlockResolver : MonoBehaviour
{
    public static BlockResolver instance;
    
    private List<BoxBase> toResolve = new List<BoxBase>();
    private bool isResolving = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            ResolveRound();
    }

    public void AddBlockToResolve(BoxBase box)
    {
        toResolve.Add(box);
    }

    public void RemoveFromToResolve(BoxBase box)
    {
        toResolve.Remove(box);
    }

    [Button]
    public void ResolveRound()
    {
        if (isResolving) return;

        if (toResolve.Count < 1)
            return;
        
        isResolving = true;
        
        foreach (var box in toResolve)
        {
            box.Execute(null);
        }
    }
}
