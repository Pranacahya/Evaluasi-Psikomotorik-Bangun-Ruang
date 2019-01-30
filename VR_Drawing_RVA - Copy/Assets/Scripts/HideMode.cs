using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMode : MonoBehaviour {

    private bool State = true;

    public void Toggle()
    {

        if (State)
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);

            Debug.Log("Toggled");
        }
    }
}
