using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDrawing : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 gridPos;
    public float gridSize;
    public GameObject target;

    private Grid grid;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }
    // Update is called once per frame
    void Update()
    {
        gridPos.x = Mathf.Floor(target.gameObject.transform.position.x / gridSize) * gridSize; 
    }

    void draw()
    {
        //var finalPosition = grid.GetNea
    }
}
