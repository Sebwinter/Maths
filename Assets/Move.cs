using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Move : MonoBehaviour
{
    MyVector3 eulerRotation = new MyVector3(0,0,0);
    MyVector3 direction = new MyVector3(0,0,0);
    MyVector3 scale = new MyVector3(1, 1, 1);
    MyVector3 targetDirection = new MyVector3(0,0,0);

    public float smoothSpeed = 1.0f;
    
    GameObject player;
    MyVector3[] ModelSpaceVertices;

    public float moveSpeed = 10f;
    public float turnSpeed = 10f;

    public MyVector3 ThisObjectVector;
    public GameObject target;

    MyVertex myTransform;
    void Start()
    {
        myTransform = GetComponent<MyVertex>();
        player = GameObject.Find("Player");
        ThisObjectVector = MyVector3.ToMyVector(myTransform.Position);

        MeshFilter MF = GetComponentInChildren<MeshFilter>();

        ModelSpaceVertices = MyVector3.Array(MF.mesh.vertices);
            

       
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {

            MyVector3 MyPos = MyVector3.ToMyVector(myTransform.Position);
            MyVector3 MyRot = MyVector3.ToMyVector(myTransform.Rotation);
            MyVector3 MyScale = MyVector3.ToMyVector(myTransform.Scale);


            MyVector3 restartPosition = new MyVector3(0, 0, 0);
            MyVector3 lerpedPos = MathsLib.VecLerp(MyPos, restartPosition, smoothSpeed);
            Vector3 RestartPosition = MyVector3.ToUnityVector(lerpedPos);
            myTransform.Position = RestartPosition;

            MyVector3 restartRotation = new MyVector3(0, 0, 0);
            MyVector3 lerpedRot = MathsLib.VecLerp(MyRot, restartRotation, smoothSpeed);
            Vector3 RestartRotation = MyVector3.ToUnityVector(lerpedRot);
            myTransform.Rotation = RestartRotation;

            MyVector3 restartScale = new MyVector3(1, 1, 1);
            MyVector3 lerpedScale = MathsLib.VecLerp(MyScale, restartScale, smoothSpeed);
            Vector3 RestartScale = MyVector3.ToUnityVector(lerpedScale);
            myTransform.Scale = RestartScale;

        }

        //Calculate character Euler angles
        //transform.eulerAngles = new Vector3(transform.position.x, transform.position.y, transform.eulerAngles.z);

        //Vector3 rotate = transform.eulerAngles;

        //rotate = rotate / (180 / Mathf.PI);

        //MyVector3 eulerRotation = MyVector3.ToMyVector(rotate);

        //Calculate a forward direction from the rotation
        direction = MathsLib.EulerAnglesToDirection(eulerRotation);

       //Rotation on the Yaw (Left)
        if (Input.GetKey(KeyCode.Q))
        {
            MyVector3 yawLeftRotation = new MyVector3(0, -1, 0);
            targetDirection = MathsLib.VectorCrossProduct(yawLeftRotation, direction);

            MyVector3 appliedDirection = targetDirection * moveSpeed * Time.deltaTime;

            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Rotation -= vec3Direction;
        }

        //Rotation on the Yaw (Right)
        if (Input.GetKey(KeyCode.E))
        {
            MyVector3 yawRightRotation = new MyVector3(0, 1, 0);
            targetDirection = MathsLib.VectorCrossProduct(yawRightRotation, direction);

            MyVector3 appliedDirection = targetDirection * moveSpeed * Time.deltaTime;

            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Rotation -= vec3Direction;
        }


        //Rotation on the Pitch (Left)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            MyVector3 pitchLeft = new MyVector3(1, 0, 0);
            targetDirection = MathsLib.VectorCrossProduct(pitchLeft, direction);

            MyVector3 appliedDirection = targetDirection * moveSpeed * Time.deltaTime;

            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Rotation -= vec3Direction;
        }

        //Rotation on the Pitch (Right)
        if (Input.GetKey(KeyCode.DownArrow))
        {
            MyVector3 pitchRight = new MyVector3(-1, 0, 0);
            targetDirection = MathsLib.VectorCrossProduct(pitchRight, direction);

            MyVector3 appliedDirection = targetDirection * moveSpeed * Time.deltaTime;
            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Rotation -= vec3Direction;
        }


        //rotation on the Roll (Right)
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MyVector3 rollRight = new MyVector3(0, 0, 1);
            targetDirection = MathsLib.VectorCrossProduct(rollRight, direction);

            MyVector3 appliedDirection = direction * moveSpeed * Time.deltaTime;
            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Rotation += vec3Direction;
        }

        //rotation on the Roll (Left)
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MyVector3 rollLeft = new MyVector3(0, 0, -1);
            targetDirection = MathsLib.VectorCrossProduct(rollLeft, direction);

            MyVector3 appliedDirection = direction * moveSpeed * Time.deltaTime;
            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Rotation -= vec3Direction;
        }

        //movement along X axis
        if (Input.GetKey(KeyCode.D))
        {
            MyVector3 RightVector3 = new MyVector3(0, 1, 0);
            targetDirection = MathsLib.VectorCrossProduct(RightVector3, direction);

            MyVector3 appliedDirection = targetDirection * moveSpeed * Time.deltaTime;

            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Position += vec3Direction;
        }

        if (Input.GetKey(KeyCode.A))
        {
            MyVector3 LeftVector3 = new MyVector3(0, -1, 0);
            targetDirection = MathsLib.VectorCrossProduct(LeftVector3, direction);

            MyVector3 appliedDirection = targetDirection * moveSpeed * Time.deltaTime;

            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Position += vec3Direction;

        }


        //movement along Y axis
        if (Input.GetKey(KeyCode.LeftShift))
        {
            MyVector3 upVector3 = new MyVector3(-1, 0, 0);
            targetDirection = MathsLib.VectorCrossProduct(upVector3, direction);

            MyVector3 appliedDirection = targetDirection * moveSpeed * Time.deltaTime;

            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Position += vec3Direction;

        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            MyVector3 downVector3 = new MyVector3(1, 0, 0);
            targetDirection = MathsLib.VectorCrossProduct(downVector3, direction);

            MyVector3 appliedDirection = targetDirection * moveSpeed * Time.deltaTime;

            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Position += vec3Direction;

        }


        //movement along Z axis
        if (Input.GetKey(KeyCode.W))
        {
            MyVector3 forward = new MyVector3(0, 0, 1);
            targetDirection = MathsLib.VectorCrossProduct(forward, direction);

            MyVector3 appliedDirection = direction * moveSpeed * Time.deltaTime;

            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Position += vec3Direction;

        }

        if (Input.GetKey(KeyCode.S))
        {
            MyVector3 backward = new MyVector3(0, 0, -1);
            targetDirection = MathsLib.VectorCrossProduct(backward, direction);

            MyVector3 appliedDirection = direction * moveSpeed * Time.deltaTime;

            Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

            myTransform.Position -= vec3Direction;

        }


 

        //Scale Increase
        if(Input.GetKey(KeyCode.C))
        {
            if ((myTransform.Scale.x > 0) && (myTransform.Scale.y > 0) && (myTransform.Scale.z > 0))
            {
                MyVector3 scaleIncrease = new MyVector3(1, 1, 1);
                targetDirection = MathsLib.VectorCrossProduct(scaleIncrease, scale);

                MyVector3 appliedDirection = scale * moveSpeed * Time.deltaTime;
                Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

                myTransform.Scale += vec3Direction;

            }

        }

        //Scale Decrease
        if (Input.GetKey(KeyCode.Z))
        {
            if((myTransform.Scale.x > 0) && (myTransform.Scale.y > 0) && (myTransform.Scale.z > 0))
            {
                MyVector3 scaleIncrease = new MyVector3(1, 1, 1);
                targetDirection = MathsLib.VectorCrossProduct(scaleIncrease, scale);

                MyVector3 appliedDirection = scale * moveSpeed * Time.deltaTime;
                Vector3 vec3Direction = MyVector3.ToUnityVector(appliedDirection);

                myTransform.Scale -= vec3Direction;

            }



        }

    }


}
