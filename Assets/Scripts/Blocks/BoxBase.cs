using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class BoxBase : MonoBehaviour
{
    private bool isActive = false;
    protected int id;
    
    public virtual void Execute(){}

    public virtual void TryActivate(BoxBase touching)
    {
        isActive = true;
    }

    public int GetID()
    {
        return id;
    }
}
