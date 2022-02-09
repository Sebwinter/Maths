using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEditorInternal;
using UnityEngine;

public class MathsLib
{

    public static float VectorToRadians(MyVector2 V)
    {
        float rv = 0.0f;

        rv = Mathf.Atan(V.y / V.x);

        return rv;
    }

    public static MyVector2 RadiansToVector(float angle)
    {
        MyVector2 rv = new MyVector2(Mathf.Cos(angle), Mathf.Sin(angle));

        rv.x = Mathf.Cos(angle);
        rv.y = Mathf.Sin(angle);

        return rv;
    }

    public static MyVector3 EulerAnglesToDirection (MyVector3 EulerAngles)
    {
        MyVector3 rv = new MyVector3(0,0,0);

        rv.x = Mathf.Cos(EulerAngles.x) * Mathf.Sin(EulerAngles.y);
        rv.y = Mathf.Sin(EulerAngles.x);
        rv.z = Mathf.Cos(EulerAngles.y) * Mathf.Cos(EulerAngles.x);

        return rv;

    }

    public static MyVector3 VectorCrossProduct(MyVector3 A, MyVector3 B)
    {
        MyVector3 C = new MyVector3(0,0,0);

        C.x = A.y * B.z - A.z * B.y;
        C.y = A.z * B.x - A.x * B.z;
        C.z = A.x * B.y - A.y * B.x;

        return C;
    }

    public static MyVector3 VecLerp(MyVector3 A, MyVector3 B, float t)
    {
        MyVector3 C = new MyVector3(0, 0, 0);

        //t = new 1.0f - t;
        //C = A * (1.0f - t) + B * t;

        A = MyVector3.ScaleVector(A, (1.0f-t));
        B = MyVector3.ScaleVector(B, t);

        C = MyVector3.AddVector(A, B);

        return C;

    }

}

public class MyMatrix4by4
{
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
    public MyMatrix4by4(MyVector4 column1, MyVector4 column2, MyVector4 column3, MyVector4 column4)
    {
        values = new float[4, 4];

        //column1
        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = column1.w;

        //column2
        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = column2.w;

        //column3
        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = column3.w;

        //column4
        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = column4.w;
    }

    public MyMatrix4by4(MyVector3 column1, MyVector3 column2, MyVector3 column3, MyVector3 column4)
    {
        values = new float[4, 4];

        //column1
        values[0, 0] = column1.x;
        values[1, 0] = column1.y;
        values[2, 0] = column1.z;
        values[3, 0] = 0;

        //column2
        values[0, 1] = column2.x;
        values[1, 1] = column2.y;
        values[2, 1] = column2.z;
        values[3, 1] = 0;

        //column3
        values[0, 2] = column3.x;
        values[1, 2] = column3.y;
        values[2, 2] = column3.z;
        values[3, 2] = 0;

        //column4
        values[0, 3] = column4.x;
        values[1, 3] = column4.y;
        values[2, 3] = column4.z;
        values[3, 3] = 1; //must be one or it will not translate.
    }

    
    public static MyVector4 operator * (MyMatrix4by4 lhs, MyVector4 rhs)
    {
        MyVector4 rv = new MyVector4(0,0,0,0);

        rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 2] * rhs.z + lhs.values[0, 3] * 1;
        rv.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z + lhs.values[1, 3] * 1;
        rv.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z + lhs.values[2, 3] * 1;
        rv.w = lhs.values[3, 0] * rhs.x + lhs.values[3, 1] * rhs.y + lhs.values[3, 2] * rhs.z + lhs.values[3, 3] * 1;

        return rv;
    }

    public static MyVector3 operator *(MyMatrix4by4 lhs, MyVector3 rhs)
    {
        MyVector3 rv = new MyVector3(0, 0, 0);

        rv.x = lhs.values[0, 0] * rhs.x + lhs.values[0, 1] * rhs.y + lhs.values[0, 2] * rhs.z + lhs.values[0, 3] * 1;
        rv.y = lhs.values[1, 0] * rhs.x + lhs.values[1, 1] * rhs.y + lhs.values[1, 2] * rhs.z + lhs.values[1, 3] * 1;
        rv.z = lhs.values[2, 0] * rhs.x + lhs.values[2, 1] * rhs.y + lhs.values[2, 2] * rhs.z + lhs.values[2, 3] * 1;
        

        return rv;
    }
    public static MyMatrix4by4 operator *(MyMatrix4by4 lhs, MyMatrix4by4 rhs)
    {
        //MyMatrix4by4 rv = new MyMatrix4by4(Vector4.zero, Vector4.zero, Vector4.zero, Vector4.zero);
        MyMatrix4by4 rv = Identity;

        for (int i = 0; i < 4; i++)
        {
            for(int s = 0; s < 4; s++)
            {
                rv.values[i,s] = lhs.values[i, 0] * rhs.values[0, s] + 
                                 lhs.values[i, 1] * rhs.values[1, s] +
                                 lhs.values[i, 2] * rhs.values[2, s] + 
                                 lhs.values[i, 3] * rhs.values[3, s];
            }
        }
        
        return rv;
    }



}
