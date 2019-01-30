using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePalette : MonoBehaviour {

    //[SerializeField]
    //private GameObject Palette;
    private bool State = true;
   // private bool Toggle;

    public void Toggle()
    {
        if (State)
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
            
            Debug.Log("Toggled");
        }
    }
}
