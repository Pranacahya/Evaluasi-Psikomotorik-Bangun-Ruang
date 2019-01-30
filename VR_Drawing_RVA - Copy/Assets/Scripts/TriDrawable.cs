using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriDrawable : IDrawable
{
    public Vector3[] Draw(Vector3[] data)
    {
        Debug.Log("Drawing Triangle");
        List<Vector3> computes;
        computes = new List<Vector3>(data);
        Vector3 leftest = data[0];
        Vector3 rightest = data[0];
        Vector3 toppest = data[0];
        foreach(var compute in computes)
        {
            if(compute.x < leftest.x)
            {
                leftest = compute;
            }
            if (compute.x > rightest.x)
            {
                rightest = compute;
            }
            if (compute.y > toppest.y)
            {
                toppest = compute;
            }
        }
        return new Vector3[] {leftest, toppest, rightest, leftest};

       
    }

    
}
