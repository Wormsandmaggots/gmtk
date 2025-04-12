using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RotateOverTime : MonoBehaviour
{
    [SerializeField] private float time = 5f;
    private float timer;
    
    void Start()
    {
        timer = time;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            transform.DOPunchRotation(Vector3.right * 10, 1f);
            timer = time;
        }
    }
}
