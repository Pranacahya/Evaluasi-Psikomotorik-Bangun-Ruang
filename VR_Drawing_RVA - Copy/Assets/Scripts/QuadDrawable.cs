using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuadDrawable : IDrawable
{
    public Vector3[] Draw(Vector3[] data)
    {
        Debug.Log("Drawing Square");
        List<Vector3> computes;
        computes = new List<Vector3>(data);
        Vector3 leftest = data[0];
        Vector3 rightest = data[0];
        Vector3 toppest = data[0];
        Vector3 bottomest = data[0];
        Vector3 sisiA = Vector3.zero;
        Vector3 sisiB = Vector3.zero;
        Vector3 sisiC = Vector3.zero;
        Vector3 sisiD = Vector3.zero;
        Vector3 sisiZ = Vector3.zero;
        foreach (var compute in computes)
        {
            if (compute.x < leftest.x)
            {
                leftest.x = compute.x;
            }
            if (compute.x > rightest.x)
            {
                rightest.x = compute.x;
            }
            if (compute.y > toppest.y)
            {
                toppest.y = compute.y;
            }
            if (compute.y < bottomest.y)
            {
                bottomest.y = compute.y;
            }
        }

        sisiZ.z = (leftest.z + rightest.z + toppest.z + bottomest.z) / 4;
        sisiA.x = sisiB.x = leftest.x;
        sisiA.y = sisiD.y = bottomest.y;
        sisiB.y = sisiC.y = toppest.y;
        sisiC.x = sisiD.x = rightest.x;
        sisiA.z = sisiB.z = sisiC.z = sisiD.z = sisiZ.z;
        return new Vector3[] { sisiA, sisiB, sisiC, sisiD, sisiA };
    }
}
