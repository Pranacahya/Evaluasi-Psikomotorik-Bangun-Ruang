using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;


public class PinchCheck : MonoBehaviour
{
    
    [SerializeField]
    private Leap.Unity.PinchDetector pinch;
    private Leap.Unity.HandModel hands;
    public GameObject handGO;
    public GameObject pallete;
    //private Finger myFinger;

    bool pinchDetector;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    { 
       
        pinchDetector = pinch.IsPinching;
        if (pinchDetector)
        {
            Debug.Log("oekf");
        }
    }
}
