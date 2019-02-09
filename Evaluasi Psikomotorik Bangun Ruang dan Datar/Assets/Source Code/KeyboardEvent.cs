using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardEvent : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3[] newVertices;
    Vector2[] newUV;
    int[] newTriangles;
    private int number;
    public float height;
    public float width;
    void Start()
    {
        //Meshfilter mf = GetComponent<MeshFilter>();
        //Mesh mesh = new Mesh();
        //mf.mesh = mesh;

        //Vertices
        //Vector3[] vertices = new Vector3[4]
        //{
        //    new Vector3(0,0,0),
        //    new Vector3(widh,0,0),
        //    new Vector3(0,height,0),
        //    new Vector3(width, height,0)
        //};

        //Triangle
        //int[] tri = new int[6];

        //tri[0] = 0;
        //tri[1] = 1;
        //tri[2] = 2;

        //tri[3] = 2;
        //tri[4] = 4;
        //tri[5] = 1;

        //Vector3[] normals = new Vector3[4];



    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vKey))
            {
                number = toNumber(vKey);
            }
        }
    }

    int toNumber(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.Alpha0:
                return 0;
            case KeyCode.Alpha1:
                return 1;
            case KeyCode.Alpha2:
                return 2;
            case KeyCode.Alpha3:
                return 3;
            case KeyCode.Alpha4:
                return 4;
            case KeyCode.Alpha5:
                return 5;
            case KeyCode.Alpha6:
                return 6;
            case KeyCode.Alpha7:
                return 7;
            case KeyCode.Alpha8:
                return 8;
            default:
                return 9;
        }
    }

    public int getNumber()
    {
        return number;
    }
}
