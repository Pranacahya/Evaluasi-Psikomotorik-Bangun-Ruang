using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeshBuilder : MonoBehaviour
{   
    KeyboardEvent keyboardEvent = new KeyboardEvent();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int number = keyboardEvent.getNumber();
        makeShape();
    }

    void makeShape()
    {
        if(Input.GetKey(KeyCode.Return))
        {
            int number = keyboardEvent.getNumber();
            Debug.Log(number);
        }
    }
}
