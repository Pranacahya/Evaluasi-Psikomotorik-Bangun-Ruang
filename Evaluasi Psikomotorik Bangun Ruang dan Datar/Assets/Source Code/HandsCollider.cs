using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsCollider : MonoBehaviour
{
    private bool status;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Fingerbone" || col.gameObject.name == "Palm Bone")
        {
            SetStatus(true);
        }
        else
        {
            SetStatus(false);
        }
    }

    public void SetStatus(bool status)
    {
        this.status = status;
    }

    public bool GetStatus()
    {
        return this.status;
    }

}
