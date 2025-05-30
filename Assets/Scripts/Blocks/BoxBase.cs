using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using Grid;
using UnityEngine;
using GUI = DefaultNamespace.GUI;

public class BoxBase : MonoBehaviour
{
    [SerializeField] private float yDownOffset = 0.05f;
    private Tweener floatSequence;
    private bool isActive = false;
    protected int id;
    private bool isOverCell = false;
    private Cell overCell;

    private bool canBeDragged = true;
    private bool isDragged = false;

    private Vector3 startPos;

    public static int ResetCounter = 0;

    private Sequence s;
    private Sequence grabSequence;
    private Sequence putSequence;

    private bool isPuttingDown = false;

    private float r = 0;

    protected virtual void Start()
    {
        r = Random.Range(0.0f, 2 * Mathf.PI);
    }
    
    Vector3 v;

    private void Update()
    {
        if (overCell) return;
        if (isDragged) return;
        
        v = Mathf.Sin(Time.time + r) * Time.deltaTime * 0.1f * Vector3.up;

        transform.position += v;
    }

    public virtual void Execute(BoxBase previous)
    {
        
    }

    protected virtual void TryActivate(BoxBase touching)
    {
        isActive = true;

        touching.isActive = false;
        
        BlockResolver.instance.AddBlockToResolve(this);
    }

    public int GetID()
    {
        return id;
    }

    protected virtual void PlayDrop()
    {
        
    }

    protected virtual void RotateDrop(Vector3 rotation)
    {
        
    }

    public virtual void OnBumpEffect()
    {
        
    }

    public IEnumerator PlayDropDelayed(float val)
    {
        yield return new WaitForSeconds(val);
        PlayDrop();
    }

    private Vector3 offset;
    private float zCoord;
    private Plane dragPlane;

    private void OnMouseEnter()
    {

    }

    private void OnMouseExit()
    {
        // if(!isDragged)
        //     floatSequence.Kill(true);
    }

    private static float dragTimeMoveLimit = 0.2f;
    
    private bool isMouseDown = false;
    private float dragTime;

    protected virtual void OnMouseDown()
    {
        if (GridGenerator.block) return;
        if (Tutorial.IsBlocking) return;
        if (BlockResolver.isResolving) return;
        if (!GUI.Instance.Hidden) return;

        dragTime = 0;
        
        isMouseDown = true;
        
        if(s.IsActive())
            s.Kill();
        
        if(grabSequence.IsActive())
            grabSequence.Kill();
        
        if(putSequence.IsActive())
            putSequence.Kill();
#if !UNITY_ANDROID && !UNITY_IOS && !UNITY_WEBGL
        //isOverCell = false;

        isDragged = true;

        if (overCell != null && overCell.AssociatedBox ==  this)
        {
            if(overCell.AssociatedBox == this)
                overCell.AssociatedBox = null;
            
            if(overCell.GetCellType() == CellType.Start)
                BlockResolver.instance.RemoveFromToResolve(this);
            
            grabSequence = DOTween.Sequence();
            grabSequence.Append(transform.DOMoveY(transform.position.y + Settings.instance.cellBlockOffset, 0.1f));
            grabSequence.Play();
        }

        //overCell?.IsOver(false);
        //overCell = null;
#elif UNITY_WEBGL
    if(!Application.isMobilePlatform)
    {
        //overCell?.IsOver(false);

        isOverCell = false;

        isDragged = true;

        if (overCell != null && overCell.AssociatedBox ==  this)
        {
            if(overCell.AssociatedBox == this)
                overCell.AssociatedBox = null;
            
            if(overCell.GetCellType() == CellType.Start)
                BlockResolver.instance.RemoveFromToResolve(this);

            grabSequence = DOTween.Sequence();
            grabSequence.Append(transform.DOMoveY(transform.position.y + Settings.instance.cellBlockOffset, 0.1f));
            grabSequence.Play();
        }

        //overCell = null;
    }
#endif
        
        Vector3 pos = transform.position;
        //pos.y += Settings.instance.cellBlockDragOffset;
        
        dragPlane = new Plane(Vector3.up, pos);

        zCoord = Camera.main.WorldToScreenPoint(pos).z;

        offset = pos - GetMouseWorldPos();
        
#if !UNITY_IOS && !UNITY_ANDROID && !UNITY_WEBGL
        AudioManager.instance.Play("grab");
#elif UNITY_WEBGL
        if (!Application.isMobilePlatform)
        {
            AudioManager.instance.Play("grab");
        }
#endif
        playsoundForFirstTime = true;
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

    private bool playsoundForFirstTime = true;
    void OnMouseDrag()
    {
        if (GridGenerator.block) return;
        if (Tutorial.IsBlocking) return;
        if (BlockResolver.isResolving) return;
        if (!GUI.Instance.Hidden) return;
        if (!isMouseDown) return;
        if (isPuttingDown) return;
        if (!canBeDragged) return;
        
        dragTime += Time.deltaTime;

#if UNITY_ANDROID || UNITY_IOS
        if (dragTime < dragTimeMoveLimit)
        {
            return;
        }
        
        isOverCell = false;

        isDragged = true;

        if (overCell != null && overCell.AssociatedBox ==  this)
        {
            if(overCell.AssociatedBox == this)
                overCell.AssociatedBox = null;
            
            if(overCell.GetCellType() == CellType.Start)
                BlockResolver.instance.RemoveFromToResolve(this);

            grabSequence = DOTween.Sequence();
            grabSequence.Append(transform.DOMoveY(transform.position.y + Settings.instance.cellBlockOffset, 0.1f));
            grabSequence.Play();
        }

        overCell?.IsOver(false);
        overCell = null;
        
        if (playsoundForFirstTime)
        {
            playsoundForFirstTime = false;
            AudioManager.instance.Play("grab");
        }
#elif UNITY_WEBGL
        if (Application.isMobilePlatform)
        {
            if (dragTime < dragTimeMoveLimit)
            {
                return;
            }

            overCell?.IsOver(false);
            isOverCell = false;

            isDragged = true;
            isMouseDown = true;

            if (overCell != null && overCell.AssociatedBox ==  this)
            {
                if(overCell.AssociatedBox == this)
                    overCell.AssociatedBox = null;
            
                if(overCell.GetCellType() == CellType.Start)
                    BlockResolver.instance.RemoveFromToResolve(this);
                
                grabSequence = DOTween.Sequence();
                grabSequence.Append(transform.DOMoveY(transform.position.y + Settings.instance.cellBlockOffset, 0.1f));
                grabSequence.Play();
            }

            overCell = null;
            
            if (playsoundForFirstTime)
            {
                playsoundForFirstTime = false;
                AudioManager.instance.Play("grab");
            }
        }
#endif
        
        Vector3 newPos = GetMouseWorldPos() + offset;
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity,
                Settings.instance.cellLayer))
        {
            if(overCell != null)
                overCell.IsOver(false);
            
            isOverCell = true;
            overCell = hit.collider.GetComponent<Cell>();
            // if (overCell.AssociatedBox == this)
            // {
            //     if(overCell.GetCellType() == CellType.Start)
            //         BlockResolver.instance.RemoveFromToResolve(this);
            //     
            //     overCell.AssociatedBox = null;
            // }
            
            overCell.IsOver(overCell.AssociatedBox == null);

            return;
        }

        overCell?.IsOver(false);
        isOverCell = false;
    }

    private bool isRotating = false;

    private void OnMouseOver()
    {
        if (GridGenerator.block) return;
        if (Tutorial.IsBlocking) return;
        if (BlockResolver.isResolving) return;
        if (!canBeDragged) return;
        if (!GUI.Instance.Hidden) return;
        
#if !UNITY_IOS && !UNITY_ANDROID && !UNITY_WEBGL
        if (Input.GetMouseButtonDown(1))
        {
            RotateBlock();
        }
#elif UNITY_WEBGL
        if (!Application.isMobilePlatform && Input.GetMouseButtonDown(1))
        {
            RotateBlock();
        }
#endif
    }

    private void OnMouseUp()
    {
        if (GridGenerator.block) return;
        if (Tutorial.IsBlocking) return;
        if (BlockResolver.isResolving) return;
        if (!canBeDragged) return;
        if (isPuttingDown) return;
        if (!GUI.Instance.Hidden) return;

#if UNITY_IOS || UNITY_ANDROID
        if (dragTime < dragTimeMoveLimit)
        {
            RotateBlock();
            return;
        }
#elif UNITY_WEBGL
        if (Application.isMobilePlatform)
        {
            if (dragTime < dragTimeMoveLimit)
            {
                RotateBlock();
                return;
            }
        }
#endif

        if (Input.GetMouseButtonUp(0))
        {
            isDragged = false;
            canBeDragged = false;
            
            if (!isOverCell || overCell.AssociatedBox != null || overCell.GetCellType() == CellType.End)
            {
                s = DOTween.Sequence();
                s.Append(transform.DOMove(startPos, 0.5f));
                s.onComplete = () =>
                {
                    canBeDragged = true;
                };
                s.onKill = () => { canBeDragged = true; };
                s.Play();

                overCell = null;
                
                return;
            }
            
            overCell.OnBlockOccupy(this);
            overCell.IsOver(false);
            overCell.InvokeCellTypeRelatedMethods();
                
            PutDown();
        }
    }

    private void RotateBlock()
    {
        if (isRotating) return;
        if (LevelSettings.BlockRotation) return;
        
        isRotating = true;
        
        AudioManager.instance.Play("grab");
        Vector3 currentRotation = transform.eulerAngles;

        currentRotation.y += 90;
        
        transform.DORotate(transform.rotation.eulerAngles + Vector3.down * 10, 0.1f).onComplete = () =>
        {
            if (overCell != null && overCell.AssociatedBox == this)
            {
                RotateDrop(new Vector3(0,90,0));
                PlayDrop();
            }

            transform.DORotate(currentRotation, 0.2f).onComplete = () =>
            {
                RotateDrop(new Vector3(0, -90, 0));
                isRotating = false;
            };
        };
    }

    public virtual void Reset()
    {
        ResetBase();
    }

    public void ResetBase()
    {
        if(overCell)
            overCell.AssociatedBox = null;
        
        overCell = null;
        canBeDragged = true;
        isOverCell = false;
    }

    private void PutDown()
    {
        isPuttingDown = true;
        
        if(grabSequence.IsActive())
            grabSequence.Kill();
        
        AudioManager.instance.Play("put");
        
        Vector3 targetPosition = overCell.transform.position;
        
        targetPosition.y += Settings.instance.cellBlockOffset + yDownOffset * (overCell.GetFloor() + 1);
        
        if(putSequence.IsActive())
            putSequence.Kill();
            
        putSequence = DOTween.Sequence();
        
        putSequence.Append(transform.DOMove(targetPosition, 0.2f)).onComplete = () =>
        {
            canBeDragged = true;
            isPuttingDown = false;
            PlayDrop();
        };
        
        putSequence.onKill = () =>
        {
            canBeDragged = true;
            isPuttingDown = false;
        };

        putSequence.Play();
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

    public virtual Color GetColor()
    {
        return Color.white;
    }

    protected Vector3 StartPos1 => startPos;

    public Cell OverCell => overCell;
}
