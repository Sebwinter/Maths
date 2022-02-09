using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public MyVertex target;
    //public Transform target;
    public Vector3 offset;
    public float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.Position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }


}
