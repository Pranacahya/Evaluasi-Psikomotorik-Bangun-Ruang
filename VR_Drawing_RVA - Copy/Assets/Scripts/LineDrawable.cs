using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LineDrawable : IDrawable

{
    public Vector3[] Draw(Vector3[] data)
    {
        Debug.Log("Drawing Line");
        List<Vector3> lines = new List<Vector3>(2);
        lines.Add(data[0]);
        lines.Add(data[data.Length - 1]);
        return lines.ToArray();
    }
}
