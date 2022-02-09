using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MyVertex : MonoBehaviour
{
    public Vector3 Position = new Vector3(0,0,0);
    public Vector3 Rotation = new Vector3(0,0,0);
    public Vector3 Scale = new Vector3(1, 1, 1);

    MyVector3[] ModelSpaceVertices;



    void Start()
    {
        MeshFilter MF = GetComponent<MeshFilter>();

        ModelSpaceVertices = MyVector3.Array(MF.mesh.vertices);
        

    }

    // Update is called once per frame
    void Update()
    {
        
        //Rotation
        MyMatrix4by4 rollMatrix = new MyMatrix4by4(
            new MyVector3(Mathf.Cos(Rotation.x), Mathf.Sin(Rotation.x), 0),
            new MyVector3(-Mathf.Sin(Rotation.x), Mathf.Cos(Rotation.x), 0),
            new MyVector3(0, 0, 1),
            new MyVector3(0,0,0));

        MyMatrix4by4 pitchMatrix = new MyMatrix4by4(
            new MyVector3(1, 0, 0),
            new MyVector3(0, Mathf.Cos(Rotation.y), Mathf.Sin(Rotation.y)),
            new MyVector3(0, -Mathf.Sin(Rotation.y), Mathf.Cos(Rotation.y)),
            new MyVector3(0, 0, 0));

        MyMatrix4by4 yawMatrix = new MyMatrix4by4(
            new MyVector3(Mathf.Cos(Rotation.z), 0, -Mathf.Sin(Rotation.z)),
            new MyVector3(0, 1, 0),
            new MyVector3(Mathf.Sin(Rotation.z), 0, Mathf.Cos(Rotation.z)),
            new MyVector3(0, 0, 0));

        //Translation
        MyMatrix4by4 translationMatrix = new MyMatrix4by4(
            new MyVector3(1, 0, 0),
            new MyVector3(0, 1, 0),
            new MyVector3(0, 0, 1),
            new MyVector3(Position.x, Position.y, Position.z));

        //scale
        MyMatrix4by4 scaleMatrix = new MyMatrix4by4(
            new MyVector3(1, 0, 0) * Scale.x, 
            new MyVector3(0, 1, 0) * Scale.y, 
            new MyVector3(0, 0, 1) * Scale.z,
            new MyVector3(0, 0, 0));

        MyVector3[] TransformedVertices = new MyVector3[ModelSpaceVertices.Length];

        MyMatrix4by4 rotMatrix = yawMatrix * (pitchMatrix * rollMatrix);

        MyMatrix4by4 matrix = translationMatrix * rotMatrix * scaleMatrix;


        for (int i = 0; i < TransformedVertices.Length; i++)
        {
            
            TransformedVertices[i] = matrix * ModelSpaceVertices[i];

        }

        MeshFilter MF = GetComponent<MeshFilter>();

        MF.mesh.vertices = MyVector3.ToUnityArray(TransformedVertices);

        MF.mesh.RecalculateNormals();
        MF.mesh.RecalculateBounds();

    }

    public float[,] values;
    public static MyMatrix4by4 Identity
    {
        get
        {
            return new MyMatrix4by4
                (new MyVector4(1, 0, 0, 0),
                 new MyVector4(0, 1, 0, 0),
                 new MyVector4(0, 0, 1, 0),
                 new MyVector4(0, 0, 0, 1));
        }
    }
    public MyMatrix4by4 TranslationInverse()
    {

        MyMatrix4by4 rv = Identity;

        rv.values[0, 3] = -values[0, 3];
        rv.values[1, 3] = -values[1, 3];
        rv.values[2, 3] = -values[2, 3];


        return rv;
    }
    public MyMatrix4by4 ScaleInverse()
    {

        MyMatrix4by4 rv = Identity;

        rv.values[0, 0] = 1.0f / values[0, 0];
        rv.values[1, 1] = 1.0f / values[1, 1];
        rv.values[2, 2] = 1.0f / values[2, 2];


        return rv;
    }
    public MyMatrix4by4 RotationInverse()
    {
        MyMatrix4by4 rv = Identity;

        MyMatrix4by4 inverseRollMatrix = new MyMatrix4by4(
            new MyVector3(Mathf.Cos(Rotation.x), -Mathf.Sin(Rotation.x), 0),
            new MyVector3(Mathf.Sin(Rotation.x), Mathf.Cos(Rotation.x), 0),
            new MyVector3(0, 0, 1),
            new MyVector3(0, 0, 0));

        MyMatrix4by4 inversePitchMatrix = new MyMatrix4by4(
            new MyVector3(1, 0, 0),
            new MyVector3(0, Mathf.Cos(Rotation.y), -Mathf.Sin(Rotation.y)),
            new MyVector3(0, Mathf.Sin(Rotation.y), Mathf.Cos(Rotation.y)),
            new MyVector3(0, 0, 0));

        MyMatrix4by4 inverseYawMatrix = new MyMatrix4by4(
            new MyVector3(Mathf.Cos(Rotation.z), 0, Mathf.Sin(Rotation.z)),
            new MyVector3(0, 1, 0),
            new MyVector3(-Mathf.Sin(Rotation.z), 0, Mathf.Cos(Rotation.z)),
            new MyVector3(0, 0, 0));

        MyMatrix4by4 inverseRotMatrix = inverseYawMatrix * (inversePitchMatrix * inverseRollMatrix);
        return rv;

    }
}
