using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMode : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    public void UnhideMode()
    {
        gameObject.SetActive(true);

    }
    public void HideMode() 
    {
        gameObject.SetActive(false);

    }

}
