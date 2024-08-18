using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class BoxBase : MonoBehaviour
{
    private bool isActive = false;
    protected int id;

    protected virtual void Start()
    {
        
    }

    public virtual void Execute(){}

    public virtual void TryActivate(BoxBase touching)
    {
        isActive = true;

        touching.isActive = false;
    }

    public int GetID()
    {
        return id;
    }

    private Vector3 offset;
    private float zCoord;
    private Plane dragPlane;

    void OnMouseDown()
    {
        dragPlane = new Plane(Vector3.up, transform.position);

        zCoord = Camera.main.WorldToScreenPoint(transform.position).z;

        offset = transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance;

        if (dragPlane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }

        return Vector3.zero;
    }

    void OnMouseDrag()
    {
        Vector3 newPos = GetMouseWorldPos() + offset;
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
    }

    private void OnMouseOver()
    {
        
    }
}
