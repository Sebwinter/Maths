using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyOrbit : MonoBehaviour
{
    public MyVector3 ThisObjectVector;
    //public Transform target;
    public MyVertex target;

    public float moveSpeed = 10f;
    public float orbitDistance = 10f;
    public float orbitDegreesPerSec = 180.0f;

    MyVertex myTransform;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<MyVertex>();
        ThisObjectVector = MyVector3.ToMyVector(myTransform.Position);

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetObject = new Vector3(target.transform.position.x,target.transform.position.y, target.transform.position.z);

        //ThisObjectVector = targetObject + (ThisObjectVector - targetObject).NormalizeVector() * orbitDistance;
        MyVector3 ThisObjectVector = MyVector3.ToMyVector(myTransform.Position);

        //Vector3 vec3TargetObject = MyVector3.ToUnityVector(targetObject);

        //as i did not make my own quartion, so this is unitys one.
        transform.RotateAround(targetObject, Vector3.up, orbitDegreesPerSec * Time.deltaTime);

        MyVector3 cubeDirection = new MyVector3(0, 0, 0);
        //cubeDirection = MyVector3.SubtractVector(targetObject, ThisObjectVector);
        cubeDirection = cubeDirection.NormalizeVector();

        MyVector3 moveDirection = new MyVector3(0, 0, 0);
        MyVector3 upwardVector = new MyVector3(0, 1, 0);
        moveDirection = MathsLib.VectorCrossProduct(upwardVector, cubeDirection);

        MyVector3 appliedDirection = moveDirection * moveSpeed * Time.deltaTime;

        //Vector3 destination = appliedDirection.ToUnityVector();

        MyVector3 currentDirection = appliedDirection + ThisObjectVector;

        Vector3 vec3CurrentPos = MyVector3.ToUnityVector(currentDirection);

        //myTransform.Position += vec3CurrentPos; breaks the AABB.
        ThisObjectVector = currentDirection;

        ThisObjectVector = MyVector3.ToMyVector(myTransform.Position);

    }

    //For testing the planets and moons orbit.

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;

    //    Gizmos.DrawWireSphere(target.transform.position, orbitDistance);

    //}
}
