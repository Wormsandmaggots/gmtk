using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public class BlockResolver : MonoBehaviour
{
    public static BlockResolver instance;
    
    private List<BoxBase> toResolve = new List<BoxBase>();
    public static bool isResolving = false;
    public static bool canResolve = false;

    private void Awake()
    {
        isResolving = false;
        instance = this;
        canResolve = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            ResolveRound();
    }

    public void AddBlockToResolve(BoxBase box)
    {
        toResolve.Add(box);
        
        canResolve = true;
    }

    public void RemoveFromToResolve(BoxBase box)
    {
        toResolve.Remove(box);
        
        if(toResolve.Count <= 0)
            canResolve = false;
    }

    public void ClearResolve()
    {
        if (toResolve == null || toResolve.Count < 1)
            return;
        
        toResolve.Clear();

        isResolving = false;
        canResolve = false;
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
