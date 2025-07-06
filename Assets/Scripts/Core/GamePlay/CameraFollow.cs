using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : SingleTon<CameraFollow>
{
    private Vector3 initialPosition;
    public Transform target;
    private Vector3 offset;
    private Vector3 targetPosition;
    void Awake()
    {
        initialPosition = transform.position;
        offset = transform.position - target.position;
    }
    void LateUpdate()
    {
        if (target != null)
        {
            targetPosition = target.position + offset;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 13 * Time.deltaTime);
        }
    }
    public void ResetCamera()
    {
        transform.position = initialPosition;
    }
}
