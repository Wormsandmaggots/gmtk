using System;
using NaughtyAttributes;
using UnityEngine;

public class ExtrudingBox : BoxBase
{
    private static DropdownList<Vector3> Directions()
    {
        return new DropdownList<Vector3>()
        {
            { "Right",   Vector3.right },
            { "Left",    Vector3.left },
            { "Forward", Vector3.forward },
            { "Back",    Vector3.back }
        };
    }
    
    [Dropdown("Directions")]
    [SerializeField] private Vector3 direction;
    
    protected override void Start()
    {
        id = 2;
    }

    public override void TryActivate(BoxBase touching)
    {
        base.TryActivate(touching);
    }

    [Button]
    public override void Execute()
    {
        Vector3 scale = transform.localScale;
        Vector3 currDir = direction;
        
        if (direction.x < 0)
            currDir.x = -direction.x;

        if (direction.z < 0)
            currDir.z = -direction.z;
        
        scale += currDir;

        transform.localScale = scale;
    }

    [Button]
    void Reset()
    {
        transform.localScale = Vector3.one;
        transform.GetChild(0).transform.localPosition = Vector3.zero;
    }

    [Button]
    public void ResetPivot()
    {
        Vector3 scale = transform.localScale;
        transform.GetChild(0).transform.localPosition = new Vector3(direction.x / 2, 0, direction.z / 2);
    }
}