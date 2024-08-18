using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockResolver : MonoBehaviour
{
    public static BlockResolver instance;
    
    private List<BoxBase> toResolve;

    private void Awake()
    {
        instance = this;
    }

    public void AddBlockToResolve(BoxBase box)
    {
        toResolve.Add(box);
    }

    public void ResolveRound()
    {
        foreach (var box in toResolve)
        {
            box.Execute();
        }
    }
}
