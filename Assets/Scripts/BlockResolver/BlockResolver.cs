using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BlockResolver : MonoBehaviour
{
    public static BlockResolver instance;
    
    private List<BoxBase> toResolve = new List<BoxBase>();
    public static bool isResolving = false;

    private void Awake()
    {
        isResolving = false;
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

    public void ClearResolve()
    {
        toResolve.Clear();

        isResolving = false;
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

    public bool IsResolving => isResolving;
}
