using DG.Tweening;
using UnityEngine;
using UnityEngine.VFX;

public class Tumbleweed : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _animTime = 5.0f;
    [SerializeField] private float _jumpFrequency = 1.0f;
    [SerializeField] private float _rotationSpeed = 1.0f;
    [SerializeField] private float _bumpRotationMul = 2f;
    [SerializeField] private Vector3 _rotationAxis = Vector3.right;
    [SerializeField] private Transform _pointDirection;
    [SerializeField] private bool _useScaling = false;
    [SerializeField] private VisualEffect _visualEffect;
    private Vector3 _deltaPos;
    private Vector3 _startPos;
    private Vector3 _dir;
    private float _timer;
    private float _epsilon = 0.1f;
    private Vector3 _startScale;

    private void Start()
    {
        _startPos = transform.position;

        Vector3 d = _pointDirection.position - transform.position;
        _dir.x = d.x;
        _dir.z = d.z;
        _dir = _dir.normalized;
        
        _startScale = transform.localScale;
    }

    private void Update()
    {
        if (_timer <= 0)
        {
            Reset();
        }
        
        _timer -= Time.deltaTime;
        
        _deltaPos.y = Mathf.Abs(Mathf.Sin(Time.time * _jumpFrequency) * _jumpForce);
        _deltaPos += Time.deltaTime * _moveSpeed * _dir;

        transform.position = _startPos + _deltaPos;
        
        transform.Rotate(_rotationAxis, _rotationSpeed);

        if (transform.position.y <= _startPos.y + _epsilon)
        {
            _visualEffect.Stop();
            
            _visualEffect.transform.position = new Vector3(transform.position.x, _visualEffect.transform.position.y,
                transform.position.z);

            if (_useScaling)
            {
                Vector3 scaleVector = Vector3.forward;
                scaleVector += Vector3.right;
                
                transform.DOScale(transform.localScale + scaleVector * -0.05f, 0.1f).onComplete = () =>
                {
                    transform.DOScale(_startScale, 0.1f);
                };
                transform.Rotate(_rotationAxis, _rotationSpeed * _bumpRotationMul);
            }
            
            _visualEffect.Play();
        }
    }

    private void Reset()
    {
        _timer = _animTime;
        
        transform.position = _startPos;

        _deltaPos.x = 0;
        _deltaPos.z = 0;
    }
}
