using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector3
{
    public float x, y, z;

    public MyVector3(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;

    }

    public static MyVector3 AddFloat(MyVector3 a, float b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = a.x + b;
        rv.y = a.y + b;
        rv.z = a.z + b;

        return rv;
    }
    public static MyVector3 SubtractFloat(MyVector3 a, float b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = a.x - b;
        rv.y = a.y - b;
        rv.z = a.z - b;

        return rv;
    }
    public static MyVector3 AddVector(MyVector3 a, MyVector3 b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = a.x + b.x;
        rv.y = a.y + b.y;
        rv.z = a.z + b.z;

        return rv;
    }
    public static MyVector3 operator +(MyVector3 a, MyVector3 b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = a.x + b.x;
        rv.y = a.y + b.y;
        rv.z = a.z + b.z;

        return rv;
    }

    public static MyVector3 operator *(MyVector3 a, float b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = a.x * b;
        rv.y = a.y * b;
        rv.z = a.z * b;

        return rv;
    }

    public static MyVector3 SubtractVector(MyVector3 a, MyVector3 b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = a.x - b.x;
        rv.y = a.y - b.y;
        rv.z = a.z - b.z;

        return rv;
    }

    public static MyVector3 operator -(MyVector3 a, MyVector3 b)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = a.x - b.x;
        rv.y = a.y - b.y;
        rv.z = a.z - b.z;

        return rv;
    }

    public float Length()
    {
        float rv = 0.0f;

        rv = Mathf.Sqrt(x * x + y * y + z * z);


        return rv;
    }

    public static Vector3 ToUnityVector(MyVector3 a)
    {
        Vector3 rv = new Vector3();

        rv.x = a.x;
        rv.y = a.y;
        rv.z = a.z;

        return rv;
    }
    public static Vector3[] ToUnityArray(MyVector3[] rv)
    {
        Vector3[] array = new Vector3[rv.Length];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = ToUnityVector(rv[i]);
        }

        return array;
    }

    public static MyVector3 ToMyVector(Vector3 a)
    {
        MyVector3 rv = new MyVector3(0,0,0);

        rv.x = a.x;
        rv.y = a.y;
        rv.z = a.z;

        return rv;
    }

    public static MyVector3[] Array(Vector3[] rv)
    {
        MyVector3[] array = new MyVector3[rv.Length];

        for(int i = 0; i < array.Length; i++)
        {
            array[i] = ToMyVector(rv[i]);
        }

        return array;
    }

    public float LengthSq()
    {
        float rv = 0.0f;

        rv = x * x + y * y + z * z;


        return rv;
    }

    public static MyVector3 ScaleVector(MyVector3 v, float scalar)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = v.x * scalar;
        rv.y = v.y * scalar;
        rv.z = v.z * scalar;

        return rv;
    }

    static MyVector3 DivideVector(MyVector3 v, float divisor)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = v.x / divisor;
        rv.y = v.y / divisor;
        rv.z = v.z / divisor;

        return rv;
    }

    public MyVector3 NormalizeVector()
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv = DivideVector(this, Length());

        return rv;
    }

    static float VectorDot(MyVector3 a, MyVector3 b, bool ShouldNormalize = true)
    {
        float rv = 0.0f;

        if(ShouldNormalize)
        {
            MyVector3 normA = a.NormalizeVector();
            MyVector3 normB = b.NormalizeVector();


            rv = normA.x * normB.x + normA.y * normB.y + normA.z * normB.z;
        }
        else
        {
            rv = a.x * b.x + a.y * b.y + a.z * b.z;
        }
        

        return rv;

    }

    public static MyVector3 Zero()
    {
        return new MyVector3(0f, 0f, 0f);
    }

}
