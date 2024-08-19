using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using DefaultNamespace;
using DG.Tweening;
using Grid;
using UnityEditor.Rendering;
using UnityEngine;

public class BoxBase : MonoBehaviour
{
    private bool isActive = false;
    protected int id;
    private bool isOverCell = false;
    private Cell overCell;

    private bool canBeDragged = true;

    private Vector3 startPos;

    protected virtual void Start()
    {
    }

    public virtual void Execute(BoxBase previous)
    {
        
    }

    public virtual void TryActivate(BoxBase touching)
    {
        isActive = true;

        touching.isActive = false;
        
        BlockResolver.instance.AddBlockToResolve(this);
    }

    public int GetID()
    {
        return id;
    }

    private Vector3 offset;
    private float zCoord;
    private Plane dragPlane;

    protected virtual void OnMouseDown()
    {
        isOverCell = false;

        if (overCell != null && overCell.AssociatedBox == this)
        {
            if(overCell.AssociatedBox == this)
                overCell.AssociatedBox = null;
            
            if(overCell.GetCellType() == CellType.Start)
                BlockResolver.instance.RemoveFromToResolve(this);
        }

        overCell = null;
        
        Vector3 pos = transform.position;
        pos.y += Settings.instance.cellBlockDragOffset;
        
        dragPlane = new Plane(Vector3.up, pos);

        zCoord = Camera.main.WorldToScreenPoint(pos).z;

        offset = pos - GetMouseWorldPos();
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
        if (!canBeDragged) return;
        
        Vector3 newPos = GetMouseWorldPos() + offset;
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity,
                Settings.instance.cellLayer))
        {
            isOverCell = true;
            overCell = hit.collider.GetComponent<Cell>();
            return;
        }

        isOverCell = false;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 currentRotation = transform.eulerAngles;

            currentRotation.y += 90;
            
            transform.rotation = Quaternion.Euler(currentRotation);
        }
    }

    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!isOverCell || overCell.AssociatedBox != null || overCell.GetCellType() == CellType.End)
            {
                canBeDragged = false;
                transform.DOMove(startPos, 0.5f).onComplete = () => { canBeDragged = true; };
                return;
            }

            Vector3 targetPosition = overCell.transform.position;

            targetPosition.y += Settings.instance.cellBlockOffset;
            
            transform.position = targetPosition;
            
            overCell.OnBlockOccupy(this);
            
            overCell.InvokeCellTypeRelatedMethods();
        }
    }

    public bool CanBeDragged
    {
        get => canBeDragged;
        set => canBeDragged = value;
    }

    public Vector3 StartPos
    {
        get => startPos;
        set => startPos = value;
    }
}
