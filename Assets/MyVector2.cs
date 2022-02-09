using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyVector2
{
    public float x, y;

    public MyVector2(float x, float y)
    {
        this.x = x;
        this.y = y;
       
    }

    public MyVector2 AddVectors(MyVector2 a, MyVector2 b)
    {
        MyVector2 rv = new MyVector2(0, 0);

        rv.x = a.x + b.x;
        rv.y = a.y + b.y;

        return rv;
    }

    public MyVector2 SubtractingVectors(MyVector2 a, MyVector2 b)
    {
        MyVector2 rv = new MyVector2(0, 0);

        rv.x = a.x - b.x;
        rv.y = a.y - b.y;

        return rv;
    }
}
